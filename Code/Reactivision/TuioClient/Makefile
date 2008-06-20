# should be either OSC_HOST_BIG_ENDIAN or OSC_HOST_LITTLE_ENDIAN
# Apple: OSC_HOST_BIG_ENDIAN
# Win32: OSC_HOST_LITTLE_ENDIAN
# i386 LinuX: OSC_HOST_LITTLE_ENDIAN

ENDIANESS=OSC_HOST_LITTLE_ENDIAN

SDL_CFLAGS  := $(shell sdl-config --cflags)
SDL_LDFLAGS := $(shell sdl-config --libs)

TUIO_DEMO = TuioDemo
TUIO_DUMP = TuioDump

DEMO_SOURCES = TuioDemo.cpp
DEMO_OBJECTS = TuioDemo.o
DUMP_SOURCES = TuioDump.cpp
DUMP_OBJECTS = TuioDump.o

COMMON_SOURCES = TuioClient.cpp ./oscpack/osc/OscTypes.cpp ./oscpack/osc/OscReceivedElements.cpp ./oscpack/osc/OscPrintReceivedElements.cpp ./oscpack/ip/posix/NetworkingUtils.cpp ./oscpack/ip/posix/UdpSocket.cpp
COMMON_OBJECTS = $(COMMON_SOURCES:.cpp=.o)

INCLUDES = -I./oscpack
COPTS  = -Wall -O3 $(SDL_CFLAGS)
CDEBUG = -Wall -g $(SDL_CFLAGS)
CXXFLAGS = $(COPTS) $(INCLUDES) -D$(ENDIANESS)

all: dump demo

dump:	$(COMMON_OBJECTS) $(DUMP_OBJECTS)
	$(CXX) -o $(TUIO_DUMP) $+  -lpthread

demo:	$(COMMON_OBJECTS) $(DEMO_OBJECTS)
	$(CXX) -o $(TUIO_DEMO) $+  $(SDL_LDFLAGS) -lGL -lGLU -lglut

clean:
	rm -rf $(TUIO_DUMP) $(TUIO_DEMO) $(COMMON_OBJECTS) $(DUMP_OBJECTS) $(DEMO_OBJECTS)
