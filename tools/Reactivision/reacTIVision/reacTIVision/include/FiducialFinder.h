/*  reacTIVision fiducial tracking framework
    FiducialFinder.h
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

#ifndef FIDUCIALFINDER
#define FIDUCIALFINDER

#include <vector>

#include "FrameProcessor.h"
#include "TuioServer.h"
#include "MidiServer.h"
#include "FiducialObject.h"
#include "floatpoint.h"

class FiducialFinder: public FrameProcessor
{
public:	
	FiducialFinder(MessageServer *server, char* grid_cfg) {
		
		if (server->getType()==TUIO_SERVER) {
			tserver = (TuioServer*)server;
			mserver = NULL;
		} else if (server->getType()==MIDI_SERVER) {
			mserver = (MidiServer*)server;
			tserver = NULL;
		}

		grid_config = grid_cfg;
		distortion = false;
		calibration = false;
		totalframes = 0;
		session_id = 0;
	}

	~FiducialFinder() {
		if (initialized) {
			if (distortion) delete dmap;
		}
	}
	
	virtual void process(unsigned char *src, unsigned char *dest) = 0;
	
	bool init(int w, int h, int sb ,int db);
	void toggleFlag(int flag);
	void finish();
	
protected:
	std::vector<FiducialObject> fiducialList;
	std::vector<FiducialObject>::iterator fiducial;

	long session_id;
	long totalframes;

	char* grid_config;
	int cell_width;
	int grid_size_x, grid_size_y;
	
	bool distortion, calibration, show_grid;
	FloatPoint* dmap;

	void drawGrid(unsigned char *src,unsigned char *dest);
	void computeGrid();
	
	TuioServer *tserver;
	MidiServer *mserver;
	void sendTuioMessages();
	void sendMidiMessages();

	MessageListener::DisplayMode prevMode;
};

#endif
