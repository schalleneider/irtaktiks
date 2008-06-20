/*  reacTIVision fiducial tracking framework
    FrameThresholder.h
    Copyright (C) 2005 Martin Kaltenbrunner <mkalten@iua.upf.es>

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

#ifndef FRAMETHRESHOLDER_H
#define FRAMETHRESHOLDER_H

#include "FrameProcessor.h"
#include "tiled_bernsen_threshold.h"

class FrameThresholder: public FrameProcessor
{
public:	
	FrameThresholder() { initialized = false; };
	~FrameThresholder() {
		if (initialized) {
				terminate_tiled_bernsen_thresholder( thresholder );
				delete thresholder;
		}
	};
	
	void process(unsigned char *src, unsigned char *dest);
	bool init(int w ,int h, int sb, int db);
	
	void setFlag(int flag, bool value);
	void toggleFlag(int flag);
	
private:
	TiledBernsenThresholder *thresholder;
	bool gradient;
};

#endif
