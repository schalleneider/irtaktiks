/*  reacTIVision fiducial tracking framework
    FidtrackFinder.cpp
    Copyright (C) 2006 Martin Kaltenbrunner <mkalten@iua.upf.es>

    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation; either version 2 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
*/

#include "FidtrackFinder.h"

bool FidtrackFinder::init(int w, int h, int sb, int db) {
	
	FiducialFinder::init(w,h,sb,db);
	
	if (db!=1) {
		printf("target buffer must be grayscale\n");
		return false;
	}
	
	if (strcmp(tree_config,"none")!=0) {
		initialize_treeidmap_from_file( &treeidmap, tree_config );
	} else initialize_treeidmap( &treeidmap );

	initialize_fidtrackerX( &fidtrackerx, &treeidmap, dmap);
	initialize_segmenter( &segmenter, width, height, treeidmap.max_adjacencies );
	return true;
}

void FidtrackFinder::process(unsigned char *src, unsigned char *dest) {

	// segmentation
	step_segmenter( &segmenter, dest, width, height );
	// fiducial recognition
	int count = find_fiducialsX( fiducials, MAX_FIDUCIAL_COUNT,  &fidtrackerx , &segmenter, width, height);	

	// process found symbols
	for(int i=0;i< count;i++) {
	
		// tracking the objects first
		if(fiducials[i].id!=FINGER_ID) {	
			FiducialObject *existing_fiducial = NULL;
	
			// update objects we had in the last frame
			// also check if we have an ID/position conflict
			// or correct an INVALID_FIDUCIAL_ID if we had an ID in the last frame
			for (fiducial = fiducialList.begin(); fiducial!=fiducialList.end(); fiducial++) {
	
				float distance = fiducial->distance(fiducials[i].x,fiducials[i].y);
	
				if (fiducials[i].id==fiducial->fiducial_id)  {
					// find and match a fiducial we had last frame already ...
					if(!existing_fiducial) {
						existing_fiducial = &(*fiducial);
						for (int j=0;j<count;j++) {
							if ((fiducials[j].id!=FINGER_ID) && (i!=j) && (fiducial->distance(fiducials[j].x,fiducials[j].y)<distance)) {
								existing_fiducial = NULL;
								break;
							}
						}	
					} else if (distance<existing_fiducial->distance(fiducials[i].x,fiducials[i].y)) {
							existing_fiducial = &(*fiducial);
							// is this still necessary?
					}

				} else if (distance<height/15) {
					// do we have a different ID at the same place?
	
					// this should correct wrong or invalid fiducial IDs
					// assuming that between two frames
					// there can't be a rapid exchange of two symbols
					// at the same place
	

					if (fiducials[i].id==INVALID_FIDUCIAL_ID) {
                                                //printf("angle before %f after %f\n",fiducials[i].angle,fiducial->getAngle());
                                                fiducials[i].angle=fiducial->getAngle();
                                                //printf("corrected invalid ID to %d at %d %d\n", fiducial->fiducial_id,(int)fiducial$
                                                fiducials[i].id=fiducial->fiducial_id;
                                        } else if (fiducials[i].id!=fiducial->fiducial_id) {

                                                if (!fiducial->checkIdConflict(session_id,fiducials[i].id)) {
                                                        //printf("corrected wrong ID from %d to %d at %d %d\n", fiducials[i].id,fiduc$
                                                        fiducials[i].id=fiducial->fiducial_id;
                                                } else session_id++;
                                        }
                                        existing_fiducial = &(*fiducial);
                                        break;
				}
			}
		
			if  (existing_fiducial!=NULL) {
				// just update the fiducial from last frame ...
				existing_fiducial->update(fiducials[i].x,fiducials[i].y,fiducials[i].angle);
			} else if  (fiducials[i].id!=INVALID_FIDUCIAL_ID) {
				// add the newly found object
				FiducialObject addFiducial(session_id, fiducials[i].id, width, height);
				addFiducial.update(fiducials[i].x,fiducials[i].y,fiducials[i].angle);
				fiducialList.push_back(addFiducial);
				if (mserver!=NULL) mserver->sendAddMessage(fiducials[i].id);
				if (msg_listener) {
					char add_message[16];
					sprintf(add_message,"add obj %ld %d",session_id,fiducials[i].id);
					msg_listener->setMessage(std::string(add_message));
				}
				session_id++;
			}
	
			if(fiducials[i].id!=INVALID_FIDUCIAL_ID && msg_listener)
				msg_listener->setObject(fiducials[i].id,(int)(fiducials[i].x),(int)(fiducials[i].y),fiducials[i].angle );
	
		// now the finger fiducials
		} else {
			FingerObject *existing_finger = NULL;
			for (std::list<FingerObject>::iterator finger = fingerList.begin(); finger!=fingerList.end(); finger++) {
		
				float distance = finger->distance(fiducials[i].x,fiducials[i].y);
				if (distance<24.0f) {
					existing_finger = &(*finger);
					for (int j=0;j<count;j++) {
						if(i==j) continue;
						if ((fiducials[j].id==FINGER_ID) && (finger->distance(fiducials[j].x,fiducials[j].y)<distance)) {
							existing_finger = NULL;
							break;
						} else if ((fiducials[j].id!=FINGER_ID) && (finger->distance(fiducials[j].x,fiducials[j].y)<24.0f)) {
							existing_finger->reset();
							existing_finger = NULL;
							//printf("discarded existing finger\n");
							break;
						}
					}	
				}
				if (existing_finger) break;
			}

			bool add_finger = true;
			for (fiducial = fiducialList.begin(); fiducial!=fiducialList.end(); fiducial++) {
				if (fiducial->distance(fiducials[i].x,fiducials[i].y)<24) { add_finger = false; break; }
			}

			
			if  (existing_finger!=NULL) {
				// just update the finger from last frame ...
				if (add_finger) existing_finger->update(fiducials[i].x,fiducials[i].y);
				else existing_finger->reset();
			} else {
				// add the newly found cursor
				if (add_finger) {
					FingerObject addFinger(width, height);
					addFinger.update(fiducials[i].x,fiducials[i].y);
					fingerList.push_back(addFinger);
				}
			}
	
			if(msg_listener) msg_listener->setObject(FINGER_ID,(int)(fiducials[i].x),(int)(fiducials[i].y),0);	
		}

	}

	if (tserver!=NULL) {
		sendTuioMessages();
		sendCursorMessages();
	} else if (mserver!=NULL) sendMidiMessages();
	
	drawGrid(src,dest);
}

void FidtrackFinder::sendCursorMessages() {

	// get the current set of objects on the table
	// we do not only use the raw objects retrieved from the frame
	// but also the remaining (filtered) objects from our fiducialObject list
	bool sendAliveMessage = false;
	int aliveSize = 0;
	long *aliveList = new long[fingerList.size()];
		
	for(std::list<FingerObject>::iterator finger = fingerList.begin(); finger!=fingerList.end(); ) {

		char message[16];
		int finger_status = finger->checkStatus(session_id);
		switch (finger_status) {
			case FINGER_ADDED: {
				aliveList[aliveSize]=finger->session_id;
				aliveSize++;
				session_id++;
				if(msg_listener) {
					sprintf(message,"add cur %ld",finger->session_id);
					msg_listener->setMessage(std::string(message));
				}
				finger++;
				break;
			}
			case FINGER_REMOVED: {
				if(msg_listener) {
					sprintf(message,"del cur %ld",finger->session_id);
					msg_listener->setMessage(std::string(message));
				}
				finger = fingerList.erase(finger);
				sendAliveMessage = true;
				break;
			}
			case FINGER_ALIVE: {
				aliveList[aliveSize]=finger->session_id;
				aliveSize++;
				finger++;
				break;
			}
			case FINGER_EXPIRED: {
				finger = fingerList.erase(finger);
				break;
			}
			case FINGER_CANDIDATE: {
				finger++;
				break;
			}
		}
				
	}
		
	// after a certain number of frames we should send the redundant alive message	
	if ((aliveSize>0) || (totalframes%30==0)) sendAliveMessage = true;
		
	// add the alive message
	if (sendAliveMessage) {
		tserver->addCurAlive(aliveList, aliveSize);
		
		char unsent = 0;
		// send a set message (in case anything changed compared to the last frame)
		for(std::list<FingerObject>::iterator finger = fingerList.begin(); finger!=fingerList.end(); finger++) {
			if (!tserver->freeCurSpace()) {
				tserver->addCurSeq(totalframes);
				tserver->sendCurMessages();
			}
				
			char *set_message = finger->addSetMessage(tserver);
			if(set_message && msg_listener) msg_listener->setMessage(std::string(set_message));
		
			if (finger->unsent>unsent) unsent = finger->unsent;
		}
	
		// fill the rest of the packet with unchanged objects
		// sending the least sent ones first
		while (unsent>0) {
			for(std::list<FingerObject>::iterator finger = fingerList.begin(); finger!=fingerList.end(); finger++) {
				if ((finger->alive) && (finger->unsent==unsent)) {
					if (tserver->freeCurSpace()) { 
						finger->redundantSetMessage(tserver);
					} else goto send;
				}
			}
			unsent--;
		}
	
		send:
		// finally send the packet
		tserver->addCurSeq(totalframes);
		tserver->sendCurMessages();
	}
	delete[] aliveList;
}
