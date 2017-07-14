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

#include "portVideoSDL.h"

// the thread function which contantly retrieves the latest frame
int getFrameFromCamera(void *obj) {

		portVideoSDL *engine = (portVideoSDL *)obj;
		
		unsigned char *cameraBuffer = NULL;
		unsigned char *cameraWriteBuffer = NULL;
		
		while(engine->running_) {
			if(!engine->pause_) {
				cameraBuffer = engine->camera_->getFrame();
				if (cameraBuffer!=NULL) {
					cameraWriteBuffer = engine->ringBuffer->getNextBufferToWrite();
					if (cameraWriteBuffer!=NULL) {
						memcpy(cameraWriteBuffer,cameraBuffer,engine->ringBuffer->size());
						engine->framenumber_++;
						engine->ringBuffer->writeFinished();
					}
					SDL_Delay(1);
				} else {
					if (!engine->camera_->stillRunning()) {
						engine->running_=false;
						engine->error_=true;
					} else SDL_Delay(1);
				}
			} else SDL_Delay(5);
		}
		return(0);
}

void portVideoSDL::saveBuffer(unsigned char* buffer,int size) {
	char fileName[32];
	sprintf(fileName,"frame%ld_%dx%d.raw",framenumber_, width_, height_);
	FILE*  imagefile=fopen(fileName, "w");
	fwrite((const char *)buffer, 1,  size, imagefile);
	fclose(imagefile);
}


// the principal program sequence
void portVideoSDL::run() {

	if( !setupCamera() ) {
		if( !setupWindow() ) return;
		showError("No camera found!");
		teardownWindow();
		return;
	}

	if( !setupWindow() ) return;

	allocateBuffers();
	initFrameProcessors();

	bool success = camera_->startCamera();

	if( success ){

		// add the help message from all FrameProcessors
		for (frame = processorList.begin(); frame!=processorList.end(); frame++) {
                        std::list<std::string> processor_text = (*frame)->getOptions();
			if (processor_text.size()>0) help_text.push_back("\n");
			for(std::list<std::string>::iterator processor_line = processor_text.begin(); processor_line!=processor_text.end(); processor_line++) {
				help_text.push_back(*processor_line);
			} 
		}
		
		//print the help message
		for(std::list<std::string>::iterator help_line = help_text.begin(); help_line!=help_text.end(); help_line++) {
			std::cout << *help_line << std::endl;
		} std::cout << std::endl;

		running_=true;
		cameraThread = SDL_CreateThread(getFrameFromCamera,this);
		mainLoop();
		
		SDL_KillThread(cameraThread);
		teardownCamera();
	} else {
		showError("Could not start camera!");
	}	

	teardownWindow();
	freeBuffers();
	
}

void portVideoSDL::showError(const char* error)
{
	SDL_FillRect(window_,0,0);
	SDL_Flip(window_);
	SDL_Rect *image_rect = new SDL_Rect();
	image_rect->x = (width_-camera_rect.w)/2-47;
	image_rect->y = (height_-camera_rect.h)/2-39;
	image_rect->w = camera_rect.w;
	image_rect->h = camera_rect.h;
	SDL_BlitSurface(getCamera(), &camera_rect, window_, image_rect);

	std::string error_message = "Press any key to exit "+app_name_+" ...";
	drawString((width_- SFont_TextWidth(sfont,error))/2,height_/2+60,error);
	drawString((width_- SFont_TextWidth(sfont,error_message.c_str()))/2,height_/2+80,error_message.c_str());
	SDL_Flip(window_);

	error_=true;
	while(error_) {
		process_events();
		//SDL_Delay(10);
	}

}

void portVideoSDL::drawString(int x, int y, const char *str)
{
	if(sfont) SFont_Write(window_, sfont, x,y,str);
}

void portVideoSDL::drawHelp()
{
	int y = font_height;

	for(std::list<std::string>::iterator help_line = help_text.begin(); help_line!=help_text.end(); help_line++) {
		drawString(font_height,y,help_line->c_str());
		y+=font_height;
	}
}

void portVideoSDL::drawObjects()
{
	char *id = new char[4];
	for(std::list<tobj>::iterator obj = tobjList.begin(); obj!=tobjList.end(); obj++) {
		if(obj->fiducial_id>-1) sprintf(id,"%d",obj->fiducial_id);
		else sprintf(id,"F");
		drawString(obj->x_pos-4,obj->y_pos-8,id);
	}
	tobjList.clear();
	delete[] id;
}

// does what its name suggests
void portVideoSDL::mainLoop()
{
	unsigned char *cameraReadBuffer = NULL;

	while(running_) {
		
		process_events();

		// do nothing if paused
		if (pause_){
			SDL_Delay(5);
			continue;
		}

		cameraReadBuffer = ringBuffer->getNextBufferToRead();
		// loop until we get access to a frame
		while (cameraReadBuffer==NULL) {
			SDL_Delay(1);
			cameraReadBuffer = ringBuffer->getNextBufferToRead();
			if(!running_) { endLoop(); return; }// escape on quit
		}
		/*
		memcpy(sourceBuffer_,cameraReadBuffer,ringBuffer->size());
		ringBuffer->readFinished();
		*/
		
		// try again if we can get a more recent frame
		/*do {
			memcpy(sourceBuffer_,cameraReadBuffer,ringBuffer->size());
			ringBuffer->readFinished();
			
			cameraReadBuffer = ringBuffer->getNextBufferToRead();
		} while( cameraReadBuffer != NULL );*/
		
		// do the actual image processing job
		for (frame = processorList.begin(); frame!=processorList.end(); frame++)
			(*frame)->process(cameraReadBuffer,destBuffer_);
		
		// update display
		switch( displayMode_ ) {
			case NO_DISPLAY:
				break;
			case SOURCE_DISPLAY: {
				memcpy(sourceBuffer_,cameraReadBuffer,ringBuffer->size());
				SDL_BlitSurface(sourceImage_, NULL, window_, NULL);
				if (help_) drawHelp();
				if (verbose_) drawObjects();
				SDL_Flip(window_);
				break;
			}			
			case DEST_DISPLAY: {
				SDL_BlitSurface(destImage_, NULL, window_, NULL);
				if (help_) drawHelp();
				if (verbose_) drawObjects();
				SDL_Flip(window_);
				break;
			}
		}
		
		ringBuffer->readFinished();
		fpsCount();
	}
	
	endLoop(); 
}

void portVideoSDL::endLoop() {
	// finish all FrameProcessors
	for (frame = processorList.begin(); frame!=processorList.end(); frame++)
		(*frame)->finish();

	if(error_) showError("Camera disconnected!");
}

void portVideoSDL::fpsCount() {

	frames_++;

	time_t currentTime;
	time(&currentTime);
	long diffTime = (long)( currentTime - lastTime_ );
	
	if (diffTime >= 2) {
		current_fps = (int)( frames_ / diffTime );
		char caption[24] = "";
		sprintf(caption,"%s - %d FPS",app_name_.c_str(),current_fps);
		if ((fpson_) && (!calibrate_)) SDL_WM_SetCaption(caption, NULL );
		frames_ = 0;
		
		lastTime_ = (long)currentTime;
    }
}

bool portVideoSDL::setupWindow() {

	if ( SDL_Init(SDL_INIT_VIDEO) < 0 ) {
		printf("SDL could not be initialized: %s\n", SDL_GetError());
		return false;
	}
	
	window_ = SDL_SetVideoMode(width_, height_, 32, SDL_HWSURFACE);
	if ( window_ == NULL ) {
		printf("Could not open window: %s\n", SDL_GetError());
		SDL_Quit();
		return false;
	}

	#ifndef __APPLE__
	SDL_WM_SetIcon(getIcon(), getMask());
	#endif

	sfont = SFont_InitDefaultFont();
	font_height = SFont_TextHeight(sfont);
	SDL_EnableKeyRepeat(200, 10);
	SDL_WM_SetCaption(app_name_.c_str(), NULL);

	return true;
}

void portVideoSDL::teardownWindow()
{
	SFont_FreeFont(sfont);
	SDL_Quit();
}

void portVideoSDL::process_events()
{
    SDL_Event event;
    while( SDL_PollEvent( &event ) ) {

        switch( event.type ) {
		case SDL_KEYDOWN:
			error_ = false;
			//printf("%d\n",event.key.keysym.sym);
			if( event.key.keysym.sym == SDLK_n ){
				displayMode_ = NO_DISPLAY;
				// turn the display black
				SDL_FillRect(window_,0,0);
				SDL_Flip(window_);
			} else if( event.key.keysym.sym == SDLK_s ){
				displayMode_ = SOURCE_DISPLAY;
			} else if( event.key.keysym.sym == SDLK_t ){
				displayMode_ = DEST_DISPLAY;
			} else if( event.key.keysym.sym == SDLK_o ){
				camera_->showSettingsDialog(); 
			} else if( event.key.keysym.sym == SDLK_v ){
				if (verbose_) {
					verbose_=false;
				} else {
					help_ = false;
					verbose_=true;
				}
			} else if( event.key.keysym.sym == SDLK_f ){
				if (fpson_) {
					fpson_=false;
					SDL_WM_SetCaption(app_name_.c_str(), NULL );
				}
				else {
					// reset the fps counter
					fpson_=true;
					char caption[24] = "";
					sprintf(caption,"%s - %d FPS",app_name_.c_str(),current_fps);
					SDL_WM_SetCaption( caption, NULL );
				} 
			} else if( event.key.keysym.sym == SDLK_p ) {
				if (pause_) {
					pause_=false;
					char caption[24] = "";
					if (fpson_) sprintf(caption,"%s - %d FPS",app_name_.c_str(),current_fps);
					else sprintf(caption,"%s",app_name_.c_str());
					SDL_WM_SetCaption( caption, NULL );
					
				} else {
					pause_=true;
					std::string caption = app_name_ + " - paused";
					SDL_WM_SetCaption( caption.c_str(), NULL );
					// turn the display black
					SDL_FillRect(window_,0,0);
					SDL_Flip(window_);
				}				
			} else if( event.key.keysym.sym == SDLK_c ) {
				if (calibrate_) {
					calibrate_=false;
					char caption[24] = "";
					if (fpson_) sprintf(caption,"%s - %d FPS",app_name_.c_str(),current_fps);
					else sprintf(caption,"%s",app_name_.c_str());
					SDL_WM_SetCaption( caption, NULL );
					
				} else {
					calibrate_=true;
					std::string caption = app_name_ + " - calibration";
					SDL_WM_SetCaption( caption.c_str(), NULL );
				}				
			} else if( event.key.keysym.sym == SDLK_b ){
				char fileName[32];
				sprintf(fileName,"frame%li.bmp",framenumber_);
				SDL_SaveBMP(sourceImage_, fileName);
			} else if( event.key.keysym.sym == SDLK_r ){
				saveBuffer(sourceBuffer_, width_ * height_ * bytesPerSourcePixel_);
			} else if( event.key.keysym.sym == SDLK_h ){
				help_=!help_;
				verbose_=false;
			} else if( event.key.keysym.sym == SDLK_ESCAPE ){
				running_=false;
			}

			for (frame = processorList.begin(); frame!=processorList.end(); frame++)
				(*frame)->toggleFlag(event.key.keysym.sym);

			break;
		case SDL_QUIT:
			running_ = false;
			error_ = false;
			break;
        }
    }
}

bool portVideoSDL::setupCamera() {

	camera_ = cameraTool::findCamera();	
	if (camera_ == NULL) return false;
	
	bool colour = false;
	if (sourceDepth_==24) colour = true;
	bool success = camera_->initCamera(width_, height_, colour);
	
	if(success) {
		width_ = camera_->getWidth();
		height_ = camera_->getHeight();
		fps_ = camera_->getFps();
					
		printf("camera: %s\n",camera_->getName());
		printf("format: %dx%d, %dfps\n\n",width_,height_,fps_);
		return true;
	} else {
		printf("could not initialize camera\n");
		camera_->closeCamera();
		delete camera_;
		return false;
	}
}

void portVideoSDL::teardownCamera()
{
	camera_->stopCamera();
	camera_->closeCamera();
	delete camera_;
}

void portVideoSDL::allocateBuffers()
{
	bytesPerSourcePixel_ = sourceDepth_/8;	
	bytesPerDestPixel_ = destDepth_/8;
	sourceBuffer_ = new unsigned char[width_*height_*bytesPerSourcePixel_];
	destBuffer_ = new unsigned char[width_*height_*bytesPerDestPixel_];
	cameraBuffer_ = NULL;
		
	sourceImage_ = SDL_CreateRGBSurfaceFrom(sourceBuffer_, width_, height_, sourceDepth_, width_*bytesPerSourcePixel_, 0 , 0, 0, 0);
	if (sourceDepth_==8)
		SDL_SetPalette(sourceImage_, SDL_LOGPAL|SDL_PHYSPAL, palette_, 0, 256 );

	destImage_ = SDL_CreateRGBSurfaceFrom(destBuffer_, width_, height_, destDepth_, width_*bytesPerDestPixel_, 0 , 0, 0, 0);
	if (destDepth_==8)
		SDL_SetPalette(destImage_, SDL_LOGPAL|SDL_PHYSPAL, palette_, 0, 256 );
		
	SDL_DisplayFormat(sourceImage_);
	SDL_DisplayFormat(destImage_);
	
	ringBuffer = new RingBuffer(width_*height_*bytesPerSourcePixel_);
}

void portVideoSDL::freeBuffers()
{
	SDL_FreeSurface(sourceImage_);
	SDL_FreeSurface(destImage_);
	delete [] sourceBuffer_;
	delete [] destBuffer_;
	
	delete ringBuffer;
}

void portVideoSDL::addFrameProcessor(FrameProcessor *fp) {

	processorList.push_back(fp);
	fp->addMessageListener(this);
}


void portVideoSDL::removeFrameProcessor(FrameProcessor *fp) {
	frame = std::find( processorList.begin(), processorList.end(), fp );
	if( frame != processorList.end() ) {
		processorList.erase( frame );
		fp->removeMessageListener(this);
	}
}

void portVideoSDL::setObject(int fiducial_id, int x_pos, int y_pos, float angle)
{
	if ((!verbose_) || (displayMode_ == NO_DISPLAY)) return;

	tobj nobj;
	nobj.fiducial_id = fiducial_id;
	nobj.x_pos  = x_pos;
	nobj.y_pos  = y_pos;
	nobj.angle  = angle;
	tobjList.push_back(nobj);
}

void portVideoSDL::setMessage(std::string message)
{
	if (verbose_) {
		std::cout << message << std::endl;
		std::cout.flush();
	}
}

void portVideoSDL::setDisplayMode(DisplayMode mode) {
	displayMode_ = mode;
}

void portVideoSDL::initFrameProcessors() {
	for (frame = processorList.begin(); frame!=processorList.end(); ) {
		bool success = (*frame)->init(width_ , height_, bytesPerSourcePixel_, bytesPerDestPixel_);
		if(!success) {	
			processorList.erase( frame );
			printf("removed frame processor\n");
		} else frame++;
	}
}

unsigned int portVideoSDL::current_fps = 0;

portVideoSDL::portVideoSDL(char* name, bool srcColour, bool destColour)
	: error_( false )
	, pause_( false )
	, calibrate_( false )
	, help_( false )
	, framenumber_( 0 )
	, frames_( 0 )
	, fpson_( true )
	, width_( WIDTH )
	, height_( HEIGHT )
	, displayMode_( DEST_DISPLAY )
{
	time_t start_time;
	time(&start_time);
	lastTime_ = (long)start_time;

	app_name_ = std::string(name);
	sourceDepth_ = (srcColour?24:8);
	destDepth_   = (destColour?24:8);

	sourceBuffer_ = NULL;
	destBuffer_ = NULL;

	for(int i=0;i<256;i++){
		palette_[i].r=i;
		palette_[i].g=i;
		palette_[i].b=i;
	}
	
	help_text.push_back("display:");
 	help_text.push_back("   n - no image");
	help_text.push_back("   s - source image");
	help_text.push_back("   t - target image");	
	help_text.push_back("   f - fps in title");
	help_text.push_back("   v - verbose output");
	help_text.push_back("   h - this help text");
	help_text.push_back("");
	help_text.push_back("commands:");
	help_text.push_back("   b - save BMP frame");
	help_text.push_back("   r - save RAW frame");
	help_text.push_back("   o - camera options");
	help_text.push_back("   p - pause processing");
	help_text.push_back("   ESC - quit " + app_name_);
}



