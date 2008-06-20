/*  reacTIVision fiducial tracking framework
    CalibrationEngine.cpp
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

#include "CalibrationEngine.h"

CalibrationEngine::CalibrationEngine(char* out) {
	
	calibration = false;
	file_exists = false;
	calib_out = out;
	#ifdef WIN32
	FILE *infile = fopen (calib_out, "r");
	if (infile != NULL) {
		file_exists = true;
		fclose (infile);
	}
	#else
	file_exists = (access (calib_out, F_OK )==0);
	#endif

	grid_size_x = 9;
	grid_size_y = 7;
	grid = new CalibrationGrid(grid_size_x,grid_size_y);
	grid->Reset();

	if (file_exists) {
		grid->Load(calib_out);
		calib_bak = new char[128];
		sprintf(calib_bak,"%s.bak",out);
		grid->Store(calib_bak);
	}
	
	grid_xpos = (char)(grid_size_x/2);
	grid_ypos = (char)(grid_size_y/2);

	help_text.push_back( "CalibrationEngine:");
	help_text.push_back( "   c - toggle calibration mode");
	help_text.push_back( "   j - reset calibration grid");
	help_text.push_back( "   k - reset selected point");
	help_text.push_back( "   l - revert to saved grid");
	help_text.push_back( "   r - show calibration result");
	help_text.push_back( "   a,d,w,x - move within grid");
	help_text.push_back( "   2,4,6,8 - adjust grid point");
};

CalibrationEngine::~CalibrationEngine() {
	delete grid;
	if (file_exists) {
		remove(calib_bak);
		delete [] calib_bak;
	}
};

bool CalibrationEngine::init(int w, int h, int sb, int db) {

	FrameProcessor::init(w,h,sb,db);
	
	cell_size = w/(grid_size_x-1);
	return true;
}


void CalibrationEngine::setFlag(int flag, bool value) {
	toggleFlag(flag);
}

void CalibrationEngine::toggleFlag(int flag) {

	if (flag=='c') {
		if (calibration) {
			calibration = false;
			if(msg_listener) msg_listener->setDisplayMode(prevMode);
		} else {
			calibration = true;
			if(msg_listener) {
				prevMode = msg_listener->getDisplayMode();
				msg_listener->setDisplayMode(msg_listener->DEST_DISPLAY);
			}
		}
	}

	if(!calibration) return;

	if (flag=='a') {
		grid_xpos--;
		if (grid_xpos<0) grid_xpos=0;
	} else if (flag=='d') {
		grid_xpos++;
		if (grid_xpos>grid_size_x-1) grid_xpos=grid_size_x-1;
	} else if (flag=='x') {
		grid_ypos++;
		if (grid_ypos>grid_size_y-1) grid_ypos=grid_size_y-1;
	} else if (flag=='w') {
		grid_ypos--;
		if (grid_ypos<0) grid_ypos=0;
	} else if (flag=='j') {
		grid->Reset();
	} else if (flag=='k') {
		grid->Set(grid_xpos,grid_ypos,0.0,0.0);
	} else if (flag=='l') {
		if (file_exists) grid->Load(calib_bak);
	} else if (flag==276) {
		GridPoint p = grid->GetInterpolated(grid_xpos,grid_ypos);
		grid->Set(grid_xpos,grid_ypos,p.x-0.01,p.y);
	} else if (flag==275)  {
		GridPoint p = grid->GetInterpolated(grid_xpos,grid_ypos);
		grid->Set(grid_xpos,grid_ypos,p.x+0.01,p.y);
	} else if (flag==273) {
		GridPoint p = grid->GetInterpolated(grid_xpos,grid_ypos);
		grid->Set(grid_xpos,grid_ypos,p.x,p.y-0.01);
	} else if (flag==274) {
		GridPoint p = grid->GetInterpolated(grid_xpos,grid_ypos);
		grid->Set(grid_xpos,grid_ypos,p.x,p.y+0.01);
	}

	grid->Store(calib_out);
}

void CalibrationEngine::process(unsigned char *src, unsigned char *dest) {

	if(!calibration) return;

	int length = width*height;
	
	// copy the source image first
	for (int i=length-1;i>=0;i--) dest[i] = src[i];

	// draw the circles
	int offset = (width-height)/2;
	for (double a=-M_PI;a<M_PI;a+=0.005) {
		int half = height/2;

		// ellipse
		float x = width/2+cos(a)*width/2;
		float y = half+sin(a)*half;

		GridPoint gp = grid->GetInterpolated(x/cell_size,y/cell_size);
		int gx = (int)(x + gp.x*cell_size);
		int gy = (int)(y + gp.y*cell_size);

		if ((gx>=0) && (gx<width) && (gy>=0) && (gy<height)) {
			int pixel = (int)(gy*width+gx);
			dest[pixel] = 255;
		}
		
		// outer circle
		x = offset+half+cos(a)*half;
		y = half+sin(a)*half;

		gp = grid->GetInterpolated(x/cell_size,y/cell_size);
		gx = (int)(x + gp.x*cell_size);
		gy = (int)(y + gp.y*cell_size);

		if ((gx>=0) && (gx<width) && (gy>=0) && (gy<height)) {
			int pixel = (int)(gy*width+gx);
			dest[pixel] = 255;
		}

		// middle circle
		x = offset+half+cos(a)*(half-cell_size);
		y = half+sin(a)*(half-cell_size);

		gp = grid->GetInterpolated(x/cell_size,y/cell_size);
		gx = (int)(x + gp.x*cell_size);
		gy = (int)(y + gp.y*cell_size);

		if ((gx>=0) && (gx<width) && (gy>=0) && (gy<height)) {
			int pixel = (int)(gy*width+gx);
			dest[pixel] = 255;
		}

		// inner circle
		x = offset+half+cos(a)*cell_size;
		y = half+sin(a)*cell_size;

		gp = grid->GetInterpolated(x/cell_size,y/cell_size);
		gx = (int)(x + gp.x*cell_size);
		gy = (int)(y + gp.y*cell_size);

		if ((gx>=0) && (gx<width) && (gy>=0) && (gy<height)) {
			int pixel = (int)(gy*width+gx);
			dest[pixel] = 255;
		}

	}
	
	// draw the horizontal lines
	for (int i=0;i<grid_size_y;i++) {
		float start_x = 0;
		float start_y = i*cell_size;
		float end_x = (grid_size_x-1)*cell_size;
		float end_y = start_y;
				
		for (float lx=start_x;lx<end_x;lx++) {
			float ly = end_y - (end_y-start_y)/(end_x-start_x)*(end_x-lx);
				
			GridPoint gp = grid->GetInterpolated(lx/cell_size,ly/cell_size);
			int gx = (int)(lx+gp.x*cell_size);
			int gy = (int)(ly+gp.y*cell_size);

			if ((gx>=0) && (gx<width) && (gy>=0) && (gy<height)) {
				int pixel = gy*width+gx;
				dest[pixel] = 255;
				if (pixel<length-width) dest[pixel+width] = 0;
			}
		}
	}

	// draw the vertical lines
	for (int i=0;i<grid_size_x;i++) {

		float start_x = i*cell_size;
		float start_y = 0;
		float end_x = start_x;
		float end_y = (grid_size_y-1)*cell_size;
				
		for (float ly=start_y;ly<end_y;ly++) {
			float lx = end_x - (end_x-start_x)/(end_y-start_y)*(end_y-ly);	
				
			GridPoint gp = grid->GetInterpolated(lx/cell_size,ly/cell_size);
			int gx = (int)(lx+gp.x*cell_size);
			int gy = (int)(ly+gp.y*cell_size);
			
			
			if ((gx>=0) && (gx<width) && (gy>=0) && (gy<height)) {
				int pixel = gy*width+gx;
				dest[pixel] = 255;
				if ((pixel<length-1) && ((pixel+1)%width!=0)) dest[pixel+1] = 0;
			}
		}
	}
	
	// draw the grid points
	for (int i=0;i<grid_size_x;i++) {
		for (int j=0;j<grid_size_y;j++) {
			GridPoint gp = grid->GetInterpolated(i,j);
			int gx = (int)(i*cell_size + gp.x*cell_size);
			int gy = (int)(j*cell_size + gp.y*cell_size);

			for (int xx=gx-1;xx<=gx+1;xx++) {
				for (int yy=gy-1;yy<=gy+1;yy++) {
					if ((xx>=0) && (xx<width) && (yy>=0) && (yy<height)) {
						int pixel = yy*width+xx;
						dest[pixel] = 0;
					}
				}
			}
		}
	}

	// draw the green box for the selected point
	GridPoint p = grid->GetInterpolated(grid_xpos,grid_ypos);
	int gx =(int)(grid_xpos*cell_size + p.x*cell_size);
	int gy =(int)(grid_ypos*cell_size + p.y*cell_size);

	for (int xx=gx-2;xx<gx+2;xx++) {
		for (int yy=gy-2;yy<gy+2;yy++) {
			if ((xx>=0) && (xx<width) && (yy>=0) && (yy<height)) {
				int pixel = yy*width+xx;
				dest[pixel] = 255;
			}
		}
	}

}


