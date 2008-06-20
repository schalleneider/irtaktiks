/*  reacTIVision fiducial tracking framework
    FiducialObject.cpp
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

#include "FiducialObject.h"
#include <string>

FiducialObject::FiducialObject (unsigned long s_id, int f_id, int width, int height) {

	this->session_id    = s_id,
	this->fiducial_id   = f_id;
	this->width     = (float)width;
	this->height    = (float)height;
	
	updated = false;
	alive   = false;

	unsent = 0;
	lost_frames = 0;
	
	current.xpos = current.ypos = current.raw_xpos = current.raw_ypos = -100.0f;
	current.angle = current.raw_angle = 0.0f;
	current.rotation_speed = current.rotation_accel = 0.0f;
	current.motion_speed = current.motion_accel = 0.0f;
	current.motion_speed_x = current.motion_speed_y = 0.0f; 
	current.time = 0;

	id_buffer_index=0;

	sprintf(message," ");
	saveLastFrame();
}

FiducialObject::~FiducialObject() {}

void FiducialObject::update (float x, float y, float a) {

	current.time = getCurrentTime();
	
	current.raw_xpos = x;
	current.raw_ypos = y;
	current.raw_angle = (float)(DOUBLEPI-a);
	
	// fix possible position and angle jitter
	positionFilter();
	// calculate movement and rotation speed and acceleration
	computeSpeedAccel();
	
	updated = true;
	alive = true;
	lost_frames = 0;

}

void FiducialObject::saveLastFrame() {

	
	last.time = current.time;
	last.xpos = current.xpos;
	last.ypos = current.ypos;
	last.angle = current.angle;
	last.motion_speed = current.motion_speed;
	last.rotation_speed = current.rotation_speed;
}

void FiducialObject::positionFilter() {
	
	// TODO 
	// most definitely there is a more sophisticated way
	// to remove position and angle jitter
	// rather than defining a one pixel threshold 

	float threshold = 1.0;
	
	if (fabs(current.raw_xpos-last.xpos)>threshold) current.xpos = current.raw_xpos;
	else current.xpos = last.xpos;

	if (fabs(current.raw_ypos-last.ypos)>threshold) current.ypos = current.raw_ypos;
	else current.ypos = last.ypos;

	if (fabs(current.raw_angle-last.angle)>threshold/20) current.angle = current.raw_angle;
	else current.angle = last.angle;
}

void FiducialObject::computeSpeedAccel() {

	if (last.time==0) return;

	int   dt = current.time - last.time;
	float dx = current.xpos - last.xpos;
	float dy = current.ypos - last.ypos;
	float dist = sqrt(dx*dx+dy*dy);

	current.motion_speed  = dist/dt;
	current.motion_speed_x = fabs(dx/dt);
	current.motion_speed_y = fabs(dy/dt);
	current.rotation_speed  = fabs(current.angle-last.angle)/dt;

	current.motion_accel = (current.motion_speed-last.motion_speed)/dt;
	current.rotation_accel = (current.rotation_speed-last.rotation_speed)/dt;
}

char* FiducialObject::checkRemoved() {

	if(removalFilter()) {
		sprintf(message, "del obj %ld %d",session_id,fiducial_id);
		return message;
	} else return NULL;
	
}

bool FiducialObject::removalFilter() {
	// we remove an object if it hasn't appeared in the last sqrt(fps) frames

	if (!updated) {
		int frame_threshold  = (int)floor(sqrt((float)portVideoSDL::current_fps));
		if (lost_frames>frame_threshold) {
			alive = false;
			current.xpos = current.ypos = current.raw_xpos = current.raw_ypos = -100.0f;
			current.angle = current.raw_angle = 0.0f;
			current.rotation_speed = current.rotation_accel = 0.0f;
			current.motion_speed = current.motion_accel = 0.0f;
			current.motion_speed_x = current.motion_speed_y = 0.0f; 
			current.time = 0;
			lost_frames = 0;
			return true;
		} else {
			current.time = getCurrentTime();
			current.xpos = last.xpos;
			current.ypos = last.ypos;
			current.angle = last.angle;
			computeSpeedAccel();
			updated = true;
			lost_frames++;
		}
	}

	return false;	
}

float FiducialObject::distance(float x, float y) {

	// returns the distance to the current position
	float dx = x - current.xpos;
	float dy = y - current.ypos;
	return sqrt(dx*dx+dy*dy);
}

char* FiducialObject::addSetMessage (TuioServer *tserver) {

	if(updated) {
		updated = false;
		// see if anything has changed compared to the last appearence
		if (( current.xpos!=last.xpos) || ( current.ypos!=last.ypos) || ( current.angle!=last.angle) ) {
			
			unsigned long s_id = session_id;
			int f_id = fiducial_id;
			float x = current.xpos/width;
			float y = current.ypos/height;
			float a = current.angle;
			float X = current.motion_speed_x;
			float Y = current.motion_speed_y;
			float A = current.rotation_speed;
			float m = current.motion_accel;
			float r = current.rotation_accel;
	
			tserver->addObjSet(s_id,f_id,x,y,a,X,Y,A,m,r);
			sprintf(message,"set obj %ld %d %f %f %f %f %f %f %f %f",s_id,f_id,x,y,a,X,Y,A,m,r);	

			unsent = 0;
			saveLastFrame();
			return message;
		} else unsent++;
	}

	saveLastFrame();
	return NULL;

}

bool FiducialObject::checkIdConflict (int s_id, int f_id) {

	id_buffer[id_buffer_index] = f_id;

	int error=0;
	for (int i=0;i<6;i++) {
		int index = i+id_buffer_index;
		if(index==6) index-=6;
		if (id_buffer[index]==f_id) error++;
	}

	id_buffer_index++;
	if (id_buffer_index==6) id_buffer_index=0;

	if (error>4) {
		//printf("--> corrected wrong object ID from %d to %d\n",fiducial_id,f_id);
		fiducial_id=f_id;
		session_id=s_id+1;
		return true;
	}
	return false;
}


char* FiducialObject::addSetMessage (MidiServer *mserver) {

	if(updated) {
		updated = false;
		// see if anything has changed compared to the last appearance
		if (( current.xpos!=last.xpos) || ( current.ypos!=last.ypos) || ( current.angle!=last.angle) || (lost_frames>0) ) {
			
			unsigned long s_id = session_id;
			int f_id = fiducial_id;
			float x = current.xpos/width;
			float y = current.ypos/height;
			float a = current.angle;
			float X = current.motion_speed_x;
			float Y = current.motion_speed_y;
			float A = current.rotation_speed;
			float m = current.motion_accel;
			float r = current.rotation_accel;
	
			mserver->sendControlMessage(f_id, x, y, a);
			sprintf(message,"set obj %ld %d %f %f %f %f %f %f %f %f",s_id,f_id,x,y,a,X,Y,A,m,r);	

			unsent = 0;
			saveLastFrame();
			return message;
		} else unsent++;
	}

	saveLastFrame();
	return NULL;
}



void FiducialObject::redundantSetMessage (TuioServer *server) {

	unsigned long s_id = session_id;
	int f_id = fiducial_id;
	float x = current.xpos/width;
	float y = current.ypos/height;
	float a = current.angle;
	float X = current.motion_speed_x;
	float Y = current.motion_speed_y;
	float A = current.rotation_speed;
	float m = current.motion_accel;
	float r = current.rotation_accel;
	
	server->addObjSet(s_id,f_id,x,y,a,X,Y,A,m,r);	
	unsent = 0;
}

long FiducialObject::getCurrentTime() {

	#ifdef WIN32
		long timestamp = GetTickCount();
	#else
		struct timeval tv;
		struct timezone tz;
		gettimeofday(&tv,&tz);
		long timestamp = (tv.tv_sec*1000)+(tv.tv_usec/1000);
	#endif
	
	return timestamp;
}
