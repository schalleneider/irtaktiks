/*  reacTIVision fiducial tracking framework
    FiducialObject.h
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

#ifndef FIDOBJECT_H
#define FIDOBJECT_H

#ifdef WIN32
#include <windows.h>
#else
#include <sys/time.h>
#endif


#include "TuioServer.h"
#include "MidiServer.h"
#include "portVideoSDL.h"
#include "math.h"
#include "stdio.h"
#include "stdlib.h"

#define DOUBLEPI 6.283185307179586

struct fframe {
	float xpos,ypos,angle;
	float raw_xpos, raw_ypos, raw_angle;
	float rotation_speed, rotation_accel;
	float motion_speed, motion_accel;
	float motion_speed_x, motion_speed_y;
	long time;
};

class FiducialObject {
  public:
	bool alive;
	int unsent;
	unsigned long session_id;
	int fiducial_id;
	float getAngle() { return (float)(DOUBLEPI-current.angle); }
	float getX() { return current.xpos; }
	float getY() { return current.ypos; }
	bool isUpdated() { return updated; }
	bool checkIdConflict(int s_id, int f_id);


  private:
	bool updated;
	int lost_frames;
	
	float width;
	float height;
	
	fframe current, last;
	
	void positionFilter();
	void computeSpeedAccel();
	bool removalFilter();
	void saveLastFrame();
	long getCurrentTime();

	char message[128];

  public:
	FiducialObject(unsigned long s_id, int f_id, int width, int height);
	~FiducialObject();
	void update(float x, float y, float a);
	char* addSetMessage(TuioServer *tserver);
	char* addSetMessage(MidiServer *mserver);
	void redundantSetMessage(TuioServer *server);

	char* checkRemoved();
	float distance(float x, float y);

	int id_buffer[6];
	short id_buffer_index;
};


#endif
