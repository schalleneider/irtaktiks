/*
	TUIO Demo - part of the reacTIVision project
	http://mtg.upf.es/reactable

	Copyright (c) 2006 Martin Kaltenbrunner <mkalten@iua.upf.es>

	Permission is hereby granted, free of charge, to any person obtaining
	a copy of this software and associated documentation files
	(the "Software"), to deal in the Software without restriction,
	including without limitation the rights to use, copy, modify, merge,
	publish, distribute, sublicense, and/or sell copies of the Software,
	and to permit persons to whom the Software is furnished to do so,
	subject to the following conditions:

	The above copyright notice and this permission notice shall be
	included in all copies or substantial portions of the Software.

	Any person wishing to distribute modifications to the Software is
	requested to send the modifications to the original developer so that
	they can be incorporated into the canonical version.

	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
	EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
	MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
	IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR
	ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
	CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
	WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

#include "TuioDemo.h"
		
void TuioDemo::addTuioObj(unsigned int s_id, unsigned int f_id) {
	TuioObject nobj(s_id,f_id);
	objectList.push_back(nobj);
	//std::cout << "added " << s_id << std::endl;
}

void TuioDemo::updateTuioObj(unsigned int s_id, unsigned int f_id, float xpos, float ypos, float angle, float x_speed, float y_speed, float r_speed, float m_accel, float r_accel) {
	for (std::list<TuioObject>::iterator tuioObject = objectList.begin(); tuioObject!=objectList.end(); tuioObject++) {
		if (tuioObject->session_id==s_id) {
			tuioObject->update(xpos,ypos,angle);
			break;
		}
	}
	//std::cout << f_id << " (" << s_id << ") " << x << " " << y << " " << a << std::endl;
}

void TuioDemo::removeTuioObj(unsigned int s_id, unsigned int f_id) {
	for (std::list<TuioObject>::iterator tuioObject = objectList.begin(); tuioObject!=objectList.end(); tuioObject++) {
		if (tuioObject->session_id==s_id) {
			objectList.erase(tuioObject);
			break;
		}
	}
	//std::cout << "removed " << s_id << std::endl;
}

void TuioDemo::addTuioCur(unsigned int s_id) {
	TuioCursor tc(s_id);
	cursorList.push_back(tc);
	//std::cout << "added cursor " << s_id << std::endl;
}

void TuioDemo::updateTuioCur(unsigned int s_id, float xpos, float ypos, float x_speed, float y_speed, float m_accel) {
	for (std::list<TuioCursor>::iterator tuioCursor = cursorList.begin(); tuioCursor!=cursorList.end(); tuioCursor++) {
		if (tuioCursor->session_id==s_id) {
			tuioCursor->update(xpos,ypos);
			break;
		}
	}
	//std::cout << "cursor " << s_id << " " << (int)(xpos*640) << " " << (int)(ypos*480) << std::endl;
}

void TuioDemo::removeTuioCur(unsigned int s_id) {
	for (std::list<TuioCursor>::iterator tuioCursor = cursorList.begin(); tuioCursor!=cursorList.end(); tuioCursor++) {
		if (tuioCursor->session_id==s_id) {
			cursorList.erase(tuioCursor);
			break;
		}
	}
	//std::cout << "removed cursor " << s_id << std::endl;
}

void TuioDemo::refresh() {
	//drawObjects();
}


void TuioDemo::drawObjects() {
	glClear(GL_COLOR_BUFFER_BIT);
	char id[3];

	for (std::list<TuioObject>::iterator tuioObject = objectList.begin(); tuioObject!=objectList.end(); tuioObject++) {
		sprintf(id,"%d",tuioObject->fiducial_id);

		glColor3f(0.0, 0.0, 0.0);
		glPushMatrix();
		glTranslatef(tuioObject->xpos, tuioObject->ypos, 0.0);
		glRotatef(tuioObject->angle, 0.0, 0.0, 1.0);
		glBegin(GL_QUADS);
			glVertex2f(-25.0,-25.0);
			glVertex2f(-25.0, 25.0);
			glVertex2f(25.0, 25.0);
			glVertex2f(25.0, -25.0);
		glEnd();
		glPopMatrix();

		glColor3f(1.0, 1.0, 1.0);
		glRasterPos2f(tuioObject->xpos,tuioObject->ypos+5);
		drawString(id);
	}

	for (std::list<TuioCursor>::iterator tuioCursor = cursorList.begin(); tuioCursor!=cursorList.end(); tuioCursor++) {
		glBegin(GL_LINES);
		glColor3f(0.0, 0.0, 1.0);
		cur_point last_point = *tuioCursor->pointList.begin();
		for (std::list<cur_point>::iterator point = tuioCursor->pointList.begin(); point!=tuioCursor->pointList.end(); point++) {
			glVertex3f(last_point.xpos, last_point.ypos, 0.0f);
			glVertex3f(point->xpos, point->ypos, 0.0f);
			last_point.xpos = point->xpos;
			last_point.ypos = point->ypos;
		}
		glEnd();
	}

	SDL_GL_SwapBuffers();
}

void TuioDemo::drawString(char *str) {
	if (str && strlen(str)) {
		while (*str) {
			glutBitmapCharacter(GLUT_BITMAP_HELVETICA_12, *str);
			str++;
		}
	}
}


bool TuioDemo::Setup() {

	if ( SDL_Init(SDL_INIT_VIDEO) < 0 ) {
		printf("SDL could not be initialized: %s\n", SDL_GetError());
		return false;
	}
	
	SDL_Surface *window = SDL_SetVideoMode(640, 480, 32, SDL_OPENGL | SDL_DOUBLEBUF);
	if ( window == NULL ) {
		printf("Could not open window: %s\n", SDL_GetError());
		SDL_Quit();
		return false;
	}

	glClearColor(1.0f, 1.0f, 1.0f, 0.0f);
	glViewport(0, 0, 640, 480);
	glMatrixMode(GL_PROJECTION);	
	glLoadIdentity();
	gluOrtho2D(0, 640.0f,  480.0f, 0);
	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();
	SDL_WM_SetCaption("Tuio SDL Demo", NULL);

	return true;
}

void TuioDemo::Quit()
{
	stopClient();
	SDL_Quit();
}

void TuioDemo::processEvents()
{
    SDL_Event event;

    while( SDL_PollEvent( &event ) ) {

        switch( event.type ) {
		case SDL_KEYDOWN:
			if( event.key.keysym.sym == SDLK_ESCAPE ){	
				Quit();
			} 
			break;
		case SDL_QUIT:
			Quit();
			break;
        }
    }
}

void TuioDemo::startClient(int port) {

	client = new TuioClient(port);
	client->addTuioListener(this);
	client->start();
}


void TuioDemo::stopClient() {

	client->stop();
	delete(client);
	running=false;
}

TuioDemo::TuioDemo(int port) {
	Setup();
	startClient(port);
}

void TuioDemo::run() {
	running=true;
	while (running) {
		drawObjects();
		SDL_Delay(40);
		processEvents();
	} 
}

int main(int argc, char* argv[])
{
	if( argc >= 2 && strcmp( argv[1], "-h" ) == 0 ){
        	std::cout << "usage: TuioDemo [port]\n";
        	return 0;
	}

	int port = 3333;
	if( argc >= 2 ) port = atoi( argv[1] );

	glutInit(&argc,argv);
	TuioDemo *app = new TuioDemo(port);
	app->run();
	delete(app);

	return 0;
}


