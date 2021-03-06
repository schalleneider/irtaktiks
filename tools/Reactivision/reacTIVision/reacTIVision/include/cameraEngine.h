/*  portVideo, a cross platform camera framework
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

#ifndef CAMERAENGINE_H
#define CAMERAENGINE_H

#define SAT(c) \
        if (c & (~255)) { if (c < 0) c = 0; else c = 255; }

class cameraEngine
{
public:
	cameraEngine() {};
	virtual ~cameraEngine() {};
	virtual bool findCamera() = 0;
	virtual bool initCamera(int width, int height, bool colour) = 0;
	virtual bool startCamera() = 0;
	virtual unsigned char* getFrame() = 0;
	virtual bool stopCamera() = 0;
	virtual bool resetCamera() = 0;
	virtual bool closeCamera() = 0;	
	virtual bool stillRunning() = 0;
	virtual void showSettingsDialog() = 0;
		
	int getFps() { return fps; }
	int getWidth() { return width; }
	int getHeight() { return height; }
	char* getName() { return cameraName; }
	
protected:
	int cameraID;
	char* cameraName;
	int maxWidth;
	int maxHeight;
	int minWidth;
	int minHeight;

	unsigned char* buffer;
	int bytes;
	
	int width;
	int height;
	int fps;
	bool colour;

	bool running;
	
	inline void uyvy2gray(int width, int heigth, unsigned char *src, unsigned char *dest) {

		for (int i=height*width/2;i>0;i--) {
			src++;
			*dest++ = *src++;
			src++;
			*dest++ = *src++;
		}
	}
	
	inline void uyvy2rgb(int width, int height, unsigned char *src, unsigned char *dest) {
	
			int R,G,B;
			int Y1,Y2;
			int cG,cR,cB;

			for(int i=height*width/2;i>0;i--) {
				cB = ((*src - 128) * 454) >> 8;
				cG = (*src++ - 128) * 88;
				Y1 = *src++;  
				cR = ((*src - 128) * 359) >> 8;
				cG = (cG + (*src++ - 128) * 183) >> 8;
				Y2 = *src++;
					
				R = Y1 + cR;
				G = Y1 + cG;
				B = Y1 + cB;
						
				SAT(R);
				SAT(G);
				SAT(B);
	
				*dest++ = B;
				*dest++ = G;
				*dest++ = R;

				R = Y2 + cR;
				G = Y2 + cG;
				B = Y2 + cB;
						
				SAT(R);
				SAT(G);
				SAT(B);
	
				*dest++ = B;
				*dest++ = G;
				*dest++ = R;
			}		
	
	};
};

#endif
