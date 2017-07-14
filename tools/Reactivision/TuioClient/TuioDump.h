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

#ifndef INCLUDED_TUIODUMP_H
#define INCLUDED_TUIODUMP_H

#include "TuioListener.h"
#include "TuioClient.h"
#include <math.h>

class TuioDump : public TuioListener {
	
	public:
		void addTuioObj(unsigned int s_id, unsigned int f_id);
		void updateTuioObj(unsigned int s_id, unsigned int f_id, float xpos, float ypos, float angle, float x_speed, float y_speed, float r_speed, float m_accel, float r_accel);
		void removeTuioObj(unsigned int s_id, unsigned int f_id);

		void addTuioCur(unsigned int s_id);
		void updateTuioCur(unsigned int s_id, float xpos, float ypos, float x_speed, float y_speed, float m_accel);
		void removeTuioCur(unsigned int s_id);

		void refresh() {}
};

#endif /* INCLUDED_TUIODUMP_H */
