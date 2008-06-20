
 /*  reacTIVision fiducial tracking framework
    FiducialFinder.cpp
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

#include "FiducialFinder.h"
#include "CalibrationGrid.h"

bool FiducialFinder::init(int w, int h, int sb ,int db) {
	FrameProcessor::init(w,h,sb,db);

	dmap = 0;
	if (strcmp(grid_config,"none")!=0) {
		distortion = true;
		show_grid=false;

		dmap = new FloatPoint[height*width];
		computeGrid();
	} else distortion  = false;

	return true;

}

void FiducialFinder::computeGrid() {
	if(!distortion) return;

	// load the distortion grid
	grid_size_x = 9;
	grid_size_y = 7;
	cell_width = width/(grid_size_x-1);

	CalibrationGrid grid(grid_size_x,grid_size_y);
	grid.Load(grid_config);

	// calculate the distortion matrix
	for (float y=0;y<height;y++) {
		for (float x=0;x<width;x++) {
			FloatPoint dist_point;
			
			// get the postion in the grid
			float xpos = x/cell_width;
			float ypos = y/cell_width;

			// get the displacement
			GridPoint new_point =  grid.GetInterpolated(xpos,ypos);

			// apply the displacement
			dist_point.x = x-new_point.x*cell_width;
			dist_point.y = y-new_point.y*cell_width;

			// set the point
			dmap[(int)(y*width+x)] = dist_point;
		}
	}

	distortion = true;
}

void FiducialFinder::toggleFlag(int flag) {

	if (!distortion) return;

	if (flag=='g') {
		if (!show_grid) {
			show_grid = true;
			if(msg_listener) {
				prevMode = msg_listener->getDisplayMode();
				msg_listener->setDisplayMode(msg_listener->DEST_DISPLAY);
			}
		} else {
			show_grid = false;
			if(msg_listener) msg_listener->setDisplayMode(prevMode);
		}
	}

	if (flag=='c') {
		if(!calibration) calibration=true;
		else {
			calibration = false;
			computeGrid();
		}
	}

}

void FiducialFinder::sendTuioMessages() {

		// get the current set of objects on the table
		// we do not only use the raw objects retrieved from the frame
		// but also the remaining (filtered) objects from our fiducialObject list
		bool aliveMessage = false;
		int aliveSize = 0;
		long *aliveList = new long[fiducialList.size()];
		
		for(fiducial = fiducialList.begin(); fiducial!=fiducialList.end(); ) {

			char *remove_message = fiducial->checkRemoved();
			if (remove_message) {
				if(msg_listener) msg_listener->setMessage(std::string(remove_message));
				fiducial = fiducialList.erase(fiducial);
				// send a single alive message if any
				// of the objects reported to have been removed
				aliveMessage = true;
			} else {
				aliveList[aliveSize]=fiducial->session_id;
				//aliveList[aliveSize]=pos->classId;
				aliveSize++;
				fiducial++;
			} 
		}
		
		totalframes++;
		// after a certain number of frames we should send the redundant alive message
		if ((aliveSize>0) || (totalframes%30==0)) aliveMessage = true;
		
		// add the alive message
		if (aliveMessage) {
			tserver->addObjAlive(aliveList, aliveSize);
		
			char unsent = 0;
			// send a set message (in case anything changed compared to the last frame)
			for(fiducial = fiducialList.begin(); fiducial!=fiducialList.end(); fiducial++) {
				if (!tserver->freeObjSpace()) {
					tserver->addObjSeq(totalframes);
					tserver->sendObjMessages();
				}
				
				char *set_message = fiducial->addSetMessage(tserver);
				if(set_message && msg_listener) msg_listener->setMessage(std::string(set_message));
		
				if (fiducial->unsent>unsent) unsent = fiducial->unsent;
			}
	
			// fill the rest of the packet with unchanged objects
			// sending the least sent ones first
			while (unsent>0) {
				for(fiducial = fiducialList.begin(); fiducial!=fiducialList.end(); fiducial++) {
					
					if (fiducial->unsent==unsent) {
						if (tserver->freeObjSpace()) { 
							fiducial->redundantSetMessage(tserver);
						} else goto send;
					}
				}
				unsent--;
			}
				
			send:
			// finally send the packet
			tserver->addObjSeq(totalframes);
			tserver->sendObjMessages();
		}
		delete[] aliveList;
}

void FiducialFinder::sendMidiMessages() {

		for(fiducial = fiducialList.begin(); fiducial!=fiducialList.end();) {
			char *remove_message = fiducial->checkRemoved();
			if(remove_message) {
				mserver->sendRemoveMessage(fiducial->fiducial_id);
				fiducialList.erase(fiducial);
				if(msg_listener) msg_listener->setMessage(std::string(remove_message));
			} else {
				char *set_message = fiducial->addSetMessage(mserver);
				if(set_message && msg_listener) msg_listener->setMessage(std::string(set_message));
				fiducial++;
			}
		}
}



void FiducialFinder::finish() {
	
	// on exit we have to send an empty alive message to clear the scene
	if (tserver) {
		tserver->addObjAlive(NULL, 0);
		tserver->addObjSeq(-1);
		tserver->sendObjMessages();
	} else if (mserver){
		mserver->sendNullMessages();
	}
}


void FiducialFinder::drawGrid(unsigned char *src, unsigned char *dest) {

	if (!(distortion && show_grid)) return;
	int length = width*height;
	int offset = (width-height)/2;
	int half = height/2;

	// delete the dest image
	for (int i=0;i<length;i++) dest[i] =0;
	// distort the image
	for (int i=length-1;i>=0;i--) {
		FloatPoint gp = dmap[i];
		int dest_pixel = (int)gp.y*width+(int)gp.x;
		if((dest_pixel>0) && (dest_pixel<length)) dest[dest_pixel] = src[i];
	}
	// interpolate empty points
	for (int i=length-width-2;i>=0;i--) {
		if (dest[i]==0) dest[i]=(dest[i+1]+dest[i+width-1]+dest[i+width]+dest[i+width+1])/4;
	}

	// draw the circles
	for (double a=-M_PI;a<M_PI;a+=0.005) {

		// ellipse
		float x = width/2+cos(a)*width/2;
		float y = half+sin(a)*half;
		if ((x>=0) && (x<width) && (y>=0) && (y<height)) {
			int pixel = (int)y*width+(int)x;
			dest[pixel] = 255;
			//if (x<width-1) dest[pixel+1] = 0;
		}
		
		// outer circle
		x = offset+half+cos(a)*half;
		y = half+sin(a)*half;
		if ((x>=0) && (x<width) && (y>=0) && (y<height)) {
			int pixel = (int)y*width+(int)x;
			dest[pixel] = 255;
			//if (x<width-1) dest[pixel+1] = 0;
		}

		// middle circle
		x = offset+half+cos(a)*(half-cell_width);
		y = half+sin(a)*(half-cell_width);
		if ((x>=0) && (x<width) && (y>=0) && (y<height)) {
			int pixel = (int)y*width+(int)x;
			dest[pixel] = 255;
			//if (x<width-1) dest[pixel+1] = 0;
		}

		// inner circle
		x = offset+half+cos(a)*cell_width;
		y = half+sin(a)*cell_width;
		if ((x>=0) && (x<width) && (y>=0) && (y<height)) {
			int pixel = (int)y*width+(int)x;
			dest[pixel] = 255;
			//if (x<width-1) dest[pixel+1] = 0;
		}

	}
			
	// draw the horizontal lines
	for (int i=0;i<grid_size_y;i++) {
		float start_x = 0;
		float start_y = i*cell_width;
		float end_x = (grid_size_x-1)*cell_width;
		float end_y = start_y;
				
		for (float lx=start_x;lx<end_x;lx++) {
			float ly = end_y - (end_y-start_y)/(end_x-start_x)*(end_x-lx);
			int pos = (int)ly*width+(int)lx;
			if ((pos>=0) && (pos<length)) dest[pos]=255;	
			if ((pos>=0) && (pos<length+width)) dest[pos+width]=0;
		}
	}

	// draw the vertical lines
	for (int i=0;i<grid_size_x;i++) {

		float start_x = i*cell_width;
		float start_y = 0;
		float end_x = start_x;
		float end_y = (grid_size_y-1)*cell_width;
				
		for (float ly=start_y;ly<end_y;ly++) {
			float lx = end_x - (end_x-start_x)/(end_y-start_y)*(end_y-ly);	
			int pos = (int)ly*width+(int)lx;
			if ((pos>=0) && (pos<length)) dest[pos]=255;	
			if ((pos>=0) && (pos<length-1)) dest[pos+1]=0;	
		}
	}

}

