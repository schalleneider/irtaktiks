/*  reacTIVision fiducial tracking framework
    Main.cpp
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

#include "portVideoSDL.h"
#include "FrameEqualizer.h"
#include "FrameThresholder.h"
#include "FidtrackFinder.h"
#include "FidtrackFinderClassic.h"
#include "DtouchFinder.h"
#include "CalibrationEngine.h"

int main(int argc, char* argv[]) {

	int port = 3333;
	char *host = "localhost";
	char *tree_config = "none";
	char *grid_config = "none";
	char *midi_config = "none";
	bool invert_x = false;
	bool invert_y = false;
	bool invert_a = false;
	bool midi = false;
	bool amoeba = true;
	bool classic = false;
	bool dtouch = false;
	bool calibration = false;
	
	int video_device = -1;
	int midi_device = -1;
	
	int display_mode = 2;

	if (argc>1) { 
		if  ((argc%2==0) || (strcmp( argv[1], "--help" ) == 0 )) {
		
			std::cout << std::endl;
			std::cout << "usage: reacTIVision -option [argument]" << std::endl;
			std::cout << "-h [host] -p [port]\tsends TUIO messages to the specifed host & port" << std::endl;
			std::cout << "-m [midi_cfg]\t\tsends MIDI messages and uses the specified mapping" << std::endl;
			std::cout << "-t [tree_file]\t\tselects an optional fiducial tree configuration" << std::endl;
			std::cout << "-g [grid_file] \t\tuses the specified grid file for distortion" << std::endl;
			std::cout << "-i [xya]\t\tinverts x-axis, y-axis and angle (choose any)" << std::endl;
			std::cout << "-f [classic,dtouch]\tloads an alternative fiducial engine" << std::endl;
			std::cout << "-d [src,dest,none]\tstarts with the specified display mode" << std::endl;
			std::cout << "-l [video,midi]\t\tlist available video or midi devices" << std::endl;
			std::cout << std::endl;
			return 0;
		} 
		
		for (int i=1;i<argc;i+=2) {
			if( strcmp( argv[i], "-h" ) == 0 ) {
				host = argv[i+1];
			}
			else if( strcmp( argv[i], "-p" ) == 0 ) {
				port = atoi(argv[i+1]);
			}
			else if( strcmp( argv[i], "-t" ) == 0 ) {
				tree_config = argv[i+1];
			}
			else if( strcmp( argv[i], "-g" ) == 0 ) {
				grid_config = argv[i+1];
				calibration = true;
			}
			else if( strcmp( argv[i], "-m" ) == 0 ) {
				midi = true;
				midi_config = argv[i+1];
			}
			else if( strcmp( argv[i], "-d" ) == 0 ) {
				if ( strcmp( argv[i+1], "none" ) == 0 ) display_mode = 0;
				else if ( strcmp( argv[i+1], "src" ) == 0 ) display_mode = 1;
				else if ( strcmp( argv[i+1], "dest" ) == 0 ) display_mode = 2;
			}
			else if( strcmp( argv[i], "-f" ) == 0 ) {
				if ( strcmp( argv[i+1], "classic" ) == 0 ) { classic = true; amoeba=false; }
				else if ( strcmp( argv[i+1], "dtouch" ) == 0 ) { dtouch = true; ; amoeba=false; }
			}
			else if( strcmp( argv[i], "-i" ) == 0 ) {
				if (strstr(argv[i+1],"x")>0) invert_x = true;
				if (strstr(argv[i+1],"y")>0) invert_y = true; 
				if (strstr(argv[i+1],"a")>0) invert_a = true; 
			}
			else if( strcmp( argv[i], "-l" ) == 0 ) {
				if ( strcmp( argv[i+1], "midi" ) == 0 ) { MidiServer::listDevices(); return 0; }
				else if ( strcmp( argv[i+1], "video" ) == 0 ) { cameraTool::listDevices(); return 0; }
			}
			else if( strcmp( argv[i], "--midi-device" ) == 0 ) {
				midi_device = atoi(argv[i+1]);
			}
			else if( strcmp( argv[i], "--video-device" ) == 0 ) {
				video_device = atoi(argv[i+1]);
				std::cout << "video device selection will be available in reacTIVision 1.4" << std::endl;
			}
		
		}  
	}

	portVideoSDL *engine = new portVideoSDL("reacTIVision", false, false);
	switch (display_mode) {
		case 0: engine->setDisplayMode(engine->NO_DISPLAY); break;
		case 1: engine->setDisplayMode(engine->SOURCE_DISPLAY); break;
		case 2: engine->setDisplayMode(engine->DEST_DISPLAY); break;
	}
		
	MessageServer  *server		= NULL;
	FrameProcessor *fiducialfinder	= NULL;
	FrameProcessor *thresholder	= NULL;
	FrameProcessor *equalizer	= NULL;
	FrameProcessor *calibrator	= NULL;

	if(midi) server = new MidiServer(midi_config, midi_device);
	else server = new TuioServer(host,port);
	server->setInversion(invert_x, invert_y, invert_a);

	if (!dtouch) {
		equalizer = new FrameEqualizer();
		thresholder = new FrameThresholder();
		if (amoeba) fiducialfinder = new FidtrackFinder(server, tree_config, grid_config);
		else if (classic) fiducialfinder = new FidtrackFinderClassic(server, grid_config);
		engine->addFrameProcessor(equalizer);
		engine->addFrameProcessor(thresholder);
	} else {
		fiducialfinder = new DtouchFinder(server, grid_config);
	} 
		
	engine->addFrameProcessor(fiducialfinder);
		
	if(calibration) {
		calibrator = new CalibrationEngine(grid_config);
		engine->addFrameProcessor(calibrator);
	}	

	engine->run();
		
	if(calibration) {
		engine->removeFrameProcessor(calibrator);
		delete calibrator;
	}

	engine->removeFrameProcessor(fiducialfinder);
	if (!dtouch) {
		engine->removeFrameProcessor(thresholder);
		engine->removeFrameProcessor(equalizer);
		delete thresholder;
		delete equalizer;
	}

	delete fiducialfinder;
	delete engine;
	delete server;

	return 0;
}
