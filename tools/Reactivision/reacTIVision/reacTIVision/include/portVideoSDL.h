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

#ifndef PORTVIDEOSDL_H
#define PORTVIDEOSDL_H

#include <iostream>
#include <string>
#include <vector>
#include <list>
#include <algorithm>

#ifdef WIN32
#include <windows.h>
#else
#include <stdio.h>
#include <stdlib.h>
#endif

#include <SDL.h>
#include <SDL_thread.h>
#include <time.h>

#define WIDTH 640
#define HEIGHT 480

#include "cameraTool.h"
#include "RingBuffer.h"
#include "FrameProcessor.h"

#include "Resources.h"
#include "SFont.h"

class portVideoSDL: public MessageListener
{

public:
	portVideoSDL(char* name, bool srcColour, bool destColour);
	~portVideoSDL() {};
	
	void run();

	cameraEngine *camera_;
	unsigned char *cameraBuffer_;
	bool running_;
	bool error_;
	bool pause_;
	bool calibrate_;
	bool help_;
	
	RingBuffer *ringBuffer;
	
	void addFrameProcessor(FrameProcessor *fp);
	void removeFrameProcessor(FrameProcessor *fp);
	void setMessage(std::string message);	
	void setObject(int id, int x, int y, float a);
	
	long framenumber_;

	void setDisplayMode(DisplayMode mode);
	DisplayMode getDisplayMode() { return displayMode_; }

	static unsigned int current_fps;

protected:
	bool setupWindow();
	void teardownWindow();

	bool setupCamera();
	void teardownCamera();
	
	void initFrameProcessors();
	
	void allocateBuffers();
	void freeBuffers();
	
	void mainLoop();
	void endLoop();
	
	void process_events();
	void stopAndClose();
	
	void saveBuffer(unsigned char* buffer, int size);
	
	void drawString(int x, int y, const char *str);
	void drawHelp();
	void drawObjects();
	void showError(const char* error);

	SDL_Surface *window_;
	SDL_Surface *sourceImage_;
	SDL_Surface *destImage_;
	SDL_Color palette_[256];

	unsigned char* sourceBuffer_;
	unsigned char* destBuffer_;

private:
	long frames_;
  	long lastTime_;

	bool fpson_;
	void fpsCount();

	int width_;
	int height_;
	int fps_;
	int sourceDepth_;
	int destDepth_;
	int bytesPerSourcePixel_;
	int bytesPerDestPixel_;
	
	std::vector<FrameProcessor*> processorList;
	std::vector<FrameProcessor*>::iterator frame;
	std::list<tobj> tobjList;
	
	DisplayMode displayMode_;
	SDL_Thread *cameraThread;
	
	SFont_Font *sfont;
	int font_height;

	std::string app_name_;
	std::list<std::string> help_text;
};

#endif
