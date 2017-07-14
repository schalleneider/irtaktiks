/*  reacTIVision fiducial tracking framework
    FrameEqualizer.cpp
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

#include "FrameEqualizer.h"

bool FrameEqualizer::init(int w, int h, int sb, int db) {
	
	FrameProcessor::init(w,h,sb,db);
	
	if (sb!=1) {
		printf("source buffer must be grayscale");
		return false;
	}

	help_text.push_back( "FrameEqualizer:");
	help_text.push_back( "   e - toggle frame equalizer");
	
	pointmap = new int[width*height];
	for (int i=w*h-1;i>=0;i--) pointmap[i] = 0;
	return true;
}

void FrameEqualizer::process(unsigned char *src, unsigned char *dest) {

	if (equalize) {
		for (int i=width*height-1;i>=0;i--) {
			int equalized = src[i]+pointmap[i];
			if (equalized > 255) equalized = 255;
			if (equalized < 0) equalized = 0;
			src[i] = (unsigned char)equalized;
		}
	} else if (calibrate) {
		long sum = 0;
		for (int x=width/2-5;x<width/2+5;x++) {
			for (int y=height/2-5;y<height/2+5;y++) {
				sum+=src[y*width+x];
			}
		}
		unsigned char average = (unsigned char)(sum/100);

		for (int i=width*height-1;i>=0;i--) 
			pointmap[i] = average-src[i];

		calibrate = false;
		equalize = true;
	} 
	
}

void FrameEqualizer::toggleFlag(int flag) {
	if (flag=='e') {
		if (equalize) {
			calibrate=false;
			equalize=false;
		} else {
			calibrate=true;
		}
	}
}


