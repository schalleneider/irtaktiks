/*
	TUIO C++ backend - part of the reacTIVision project
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

#ifndef INCLUDED_TUIOCLIENT_H
#define INCLUDED_TUIOCLIENT_H

#ifndef WIN32
#include <pthread.h>
#else
#include <windows.h>
#endif

#include <iostream>
#include <list>
#include <algorithm>

#include "osc/OscReceivedElements.h"
#include "osc/OscPrintReceivedElements.h"

#include "ip/UdpSocket.h"
#include "ip/PacketListener.h"

#include "TuioListener.h"

using namespace osc;

struct tuio {
	unsigned int s_id, f_id;
	float xpos, ypos, angle;
};

class TuioClient : public PacketListener { 
	

	public:
		TuioClient();
		TuioClient(int p);
		~TuioClient();

		void start(bool lk=false);
		void stop();

		void addTuioListener(TuioListener *l);
		void removeTuioListener();

		void ProcessPacket( const char *data, int size, const IpEndpointName& remoteEndpoint );

		UdpListeningReceiveSocket *socket;

	protected:
		void ProcessBundle( const ReceivedBundle& b, const IpEndpointName& remoteEndpoint);
		void ProcessMessage( const ReceivedMessage& m, const IpEndpointName& remoteEndpoint);

	private:
		TuioListener *listener;

		std::list<tuio> objectList;
		std::list<unsigned int> aliveObjectList, objectBuffer;
		std::list<unsigned int> aliveCursorList, cursorBuffer;
		int32 currentFrame;

		#ifndef WIN32
		pthread_t thread;
		#else
   		HANDLE thread;
		#endif	

		bool locked;
};

#endif /* INCLUDED_TUIOCLIENT_H */
