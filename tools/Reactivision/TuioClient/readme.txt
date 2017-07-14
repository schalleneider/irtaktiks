TUIO C++ CLIENT
---------------
Copyright (c) 2006 Martin Kaltenbrunner <mkalten@iua.upf.es>
Developed at the Music Technology Group, UPF, Barcelona

This software is part of reacTIVision, an open source fiducial
tracking framework based on computer vision. 

http://mtg.upf.es/reactable/

Demo Applications:
------------------
This package contains two demo applications which are able
to receive TUIO messages from the reacTIVision engine.

* TuioDump prints the TUIO events directly to the console
* TuioDemo graphically draws the objects' state on the screen

You can use these demo applications freely for debugging purposes, 
and using them as a starting point for the development of you own
C++ applications implementing the TUIO protocol. Please refer to
the source code of the both examples and the following section.


Application Programming Interface:
----------------------------------
First you  need to create an instance of TuioClient. This class 
is listening to TUIO messages on the specified port and generates
higher level messages based on the object events. The method start(true)
will start the TuioClient in a blocking mode, simply calling start()
will start the TuioClient in the background. Call stop() in
order to stop listening to incoming TUIO messages. 

Your application needs to implement the TuioListener interface,
and has to be added to the TuioClient in order to receive messages.

A TuioListener needs to implement the following methods:

* addTuioObj(unsigned int s_id, unsigned int f_id):
  this is called when an object becomes visible
* removeTuioObj(unsigned int s_id, unsigned int f_id):
  an object was removed from the table
* updateTuioObj(unsigned int s_id, unsigned int f_id, ... ):
  the parameters of an object are updated
  these include the IDs and current position and orientation
* addTuioCur(unsigned int s_id):
  this is called when a new cursor is detected
* removeTuioCur(unsigned int s_id):
  a cursor was removed from the table
* updateTuioCur(unsigned int s_id, ... ):
  the parameters of a cursor are updated
  these include the ID and current position
* refresh():
  this method is called after each bundle,
  use it to repaint your screen for example

Typically you will need just the following code to start with:

  MyTuioListener listener;	     // implements a TuioListener
  TuioClient client(port);	     // creates the TuioClient
  client.addTuioListener(&listener); // registers the TuioListener
  client.start();		     // starts the TuioClient


Building the Examples:
----------------------
This packeage includes a Visual Studio .NET 2003 project and a
Linux Makefile for building the two example applications.
The Win32 project already includes the necessary libraries.
To build the GUI example for Linux make sure you have the SDL,
OpenGL and GLUT libraries and header installed on your system.


License:
--------
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


References:
-----------
This application uses the oscpack OpenSound Control library.
For more information and the source code see:
http://www.audiomulch.com/~rossb/code/oscpack/
