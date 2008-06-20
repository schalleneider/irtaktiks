/*  reacTIVision fiducial tracking framework
    FrameThresholder.cpp
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

#include "FrameThresholder.h"

bool FrameThresholder::init(int w, int h, int sb, int db) {
	
	FrameProcessor::init(w,h,sb,db);
	thresholder = new TiledBernsenThresholder();
	initialize_tiled_bernsen_thresholder(thresholder, w, h, 16 );

	gradient = true;

	help_text.push_back( "FrameThresholder:");
	help_text.push_back( "   g - gradient gate");

	return true;
}
void FrameThresholder::process(unsigned char *src, unsigned char *dest) {

	tiled_bernsen_threshold( thresholder, dest, src, srcBytes, width,  height, 16, (gradient?32:0) );
}

void FrameThresholder::setFlag(int flag, bool value) {
	if (flag=='g') gradient=value;
}

void FrameThresholder::toggleFlag(int flag) {
	if (flag=='g') gradient=!gradient;
}


