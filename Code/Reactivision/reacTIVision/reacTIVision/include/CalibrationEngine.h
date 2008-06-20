/*  reacTIVision fiducial tracking framework
    CalibrationEngine.h
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

#ifndef CALIBRATIONENGINE_H
#define CALIBRATIONENGINE_H

#include "FrameProcessor.h"
#include "CalibrationGrid.h"

class CalibrationEngine: public FrameProcessor
{
public:	
	CalibrationEngine(char* out);
	~CalibrationEngine();
	
	void process(unsigned char *src, unsigned char *dest);
	bool init(int w ,int h, int sb, int db);

	void setFlag(int flag, bool value);
	void toggleFlag(int flag);
	
private:
	CalibrationGrid *grid;
	char* calib_out;
	char *calib_bak;
	bool file_exists;

	bool calibration;

	int grid_xpos, grid_ypos;
	int grid_size_x, grid_size_y;
	int cell_size;

	MessageListener::DisplayMode prevMode;
};

#endif
