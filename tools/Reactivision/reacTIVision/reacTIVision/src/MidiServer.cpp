/*  reacTIVision fiducial tracking framework
    MidiServer.cpp
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

#include "MidiServer.h"

MidiServer::MidiServer(char* cfg, int device)
{
	//read config from file
	readConfig(cfg);
 
	int no_devices = Pm_CountDevices();
	if (no_devices==0) { std::cout << " no midi devices found!" << std::endl; return; }
	if (device>no_devices) { std::cout << "invalid midi device number!" << std::endl; return; }
	
	PmDeviceID device_id = Pm_GetDefaultOutputDeviceID();
	if ((device>=0) && (device<=no_devices)) {
		device_id = (PmDeviceID)device;
		const PmDeviceInfo *info = Pm_GetDeviceInfo( device_id );
		if (info->input) { std::cout << "invalid midi device number!" << std::endl; return; }
	}

	const PmDeviceInfo *info = Pm_GetDeviceInfo(device_id);
	std::cout << "opening midi device: " << info->name << std::endl;
	
	Pm_Initialize();	
	Pt_Start(1,0,0);
	Pm_OpenOutput(&midi_out,device_id,NULL,0,NULL,NULL,0);
}

void MidiServer::listDevices()
{
	int no_devices = Pm_CountDevices();	
	int out_devices = 0;
	for (int i=0;i<no_devices;i++) {
		const PmDeviceInfo *info = Pm_GetDeviceInfo( (PmDeviceID)i );
		if (info->output) out_devices++;
	}

	if (out_devices==0) {
		std::cout << "no midi out devices found!" << std::endl;
		return;
	} else if (out_devices==1) std::cout << "1 midi out device found:" << std::endl;
	else std::cout << out_devices << " midi out devices found:" << std::endl;
		
	for (int i=0;i<no_devices;i++) {
		const PmDeviceInfo *info = Pm_GetDeviceInfo( (PmDeviceID)i );
		if (info->output) std::cout << "\t" << i << ": " << info->name << std::endl;
	}
}



void MidiServer::readConfig(char* cfg) {

	TiXml::TiXmlDocument document( cfg );
	document.LoadFile();
	if( document.Error() )
	{
		std::cout << "Error loading MIDI configuration file: " << cfg << std::endl;
		return;
	}

	TiXml::TiXmlHandle docHandle( &document );
	TiXml::TiXmlElement* map = docHandle.FirstChild("midi").FirstChild("map").Element();

	while (map) {

		mapping m={0,0,0,0,0.0,0.0,0.0,0};

	
		if( map->Attribute("fiducial")!=NULL ) m.f_id = atoi(map->Attribute("fiducial"));
		else { std::cout << "missing 'fiducial' atribute in MIDI configuration file" << std::endl; return; }
	
		if( map->Attribute("type")!=NULL ) {
			if ( strcmp( map->Attribute("type"), "hfader" ) == 0 ) m.dimension=XPOS;
			if ( strcmp( map->Attribute("type"), "vfader" ) == 0 ) m.dimension=YPOS;
			if ( strcmp( map->Attribute("type"), "knob" ) == 0 )   m.dimension=ANGLE;
			if ( strcmp( map->Attribute("type"), "note" ) == 0 )   m.dimension=PRESENCE;
		}
		else { std::cout << "missing 'type' atribute in MIDI configuration file" << std::endl; return; }
	
		if( map->Attribute("channel")!=NULL ) m.channel = atoi(map->Attribute("channel")) + 0xB0;
		else m.channel = 0xB0;
	
		if( map->Attribute("control")!=NULL ) {
			m.control = atoi(map->Attribute("control"));

			if( map->Attribute("min")!=NULL ) {
				m.min = atof(map->Attribute("min"));
				if (m.dimension==ANGLE) m.min*=2*M_PI;
			} else m.min = 0.0;
	
			if( map->Attribute("max")!=NULL ) {
				m.max = atof(map->Attribute("max"));
				if (m.dimension==ANGLE) m.max*=2*M_PI;
			} else m.max = 1.0;
			
			m.range = m.max  - m.min;
			
		} else if( map->Attribute("note")!=NULL ) m.control = atoi(map->Attribute("note"));
		else { std::cout << "missing 'control' ore 'note' atribute in MIDI configuration file" << std::endl; return; }
	
		m.value = 0;
		mapping_list.push_back(m);

		map = map->NextSiblingElement( "map" );
	}

	/*for(std::list<mapping>::iterator map = mapping_list.begin(); map!=mapping_list.end(); map++) {
		std::cout << map->f_id << " " << map->dimension << " " << map->channel << " " << map->control << " "<< map->min << " " << map->max << std::endl;
	}*/

}

/*
void MidiServer::readConfig(char* cfg) {


	char line[128];
        std::fstream infile(cfg,std::ios::in);
        while (!infile.eof()) {
                infile.getline(line,128);
		if ( (strstr(line,"#")==NULL) && (strstr(line,":")!=NULL) ) {
			//std::cout << line << std::endl;
			mapping m;
			char* value = strtok(line,":");
			if (value!=NULL) m.f_id = atoi(value);
			else { std::cout << "syntax error in line: " << line << std::endl; continue; }
			value = strtok(NULL,":");
			if (value!=NULL) m.dimension = atoi(value);
			else { std::cout << "syntax error in line: " << line << std::endl; continue; }
			value = strtok(NULL,":");
			if (value!=NULL) m.channel = atoi(value) + 0xB0;
			else { std::cout << "syntax error in line: " << line << std::endl; continue; }
			value = strtok(NULL,":");
			if (value!=NULL) m.control = atoi(value);
			else { std::cout << "syntax error in line: " << line << std::endl; continue; }
			value = strtok(NULL,":");
			if (value!=NULL) m.min = atof(value);
			else { std::cout << "syntax error in line: " << line << std::endl; continue; }
			value = strtok(NULL,":");
			if (value!=NULL) m.max = atof(value);
			else { std::cout << "syntax error in line: " << line << std::endl; continue; }
			m.range = m.max  - m.min;
			m.value = 0;
			mapping_list.push_back(m);
		}	
	}
	infile.close();
	//std::cout << "file closed" << std::endl;

	for(std::list<mapping>::iterator map = mapping_list.begin(); map!=mapping_list.end(); map++) {
		std::cout << map->f_id << " " << map->dimension << " " << map->channel << " " << map->control << " "<< map->min << " " << map->max << std::endl;
	}
}*/

void MidiServer::sendControlMessage(int f_id, float xpos, float ypos, float angle)
{	

	if(invert_x) xpos=1-xpos;
	if(invert_y) ypos=1-ypos;
	if(invert_a) angle=TWO_PI-angle;

	for(std::list<mapping>::iterator map = mapping_list.begin(); map!=mapping_list.end(); map++) {

		if (map->f_id==f_id) {
			switch (map->dimension) {
				case XPOS:
					if ((xpos>=map->min) && (xpos<=map->max)) {
						map->value = (int)(((xpos - map->min)/map->range)*127);
						Pm_WriteShort(midi_out,0,Pm_Message((char)map->channel,(char)map->control, (char)map->value));
					}
					break;
				case YPOS:
					if ((ypos>=map->min) && (ypos<=map->max)) {
						map->value = (int)(((map->max - ypos)/map->range)*127);
						Pm_WriteShort(midi_out,0,Pm_Message((char)map->channel,(char)map->control, (char)map->value));
					}
					break;
				case ANGLE:
					if ((angle>=map->min) && (angle<=map->max)) {
						map->value = (int)(((angle - map->min)/map->range)*127);
						Pm_WriteShort(midi_out,0,Pm_Message((char)map->channel,(char)map->control, (char)map->value));
					}
					break;
			}
		}
	}
}

void MidiServer::sendAddMessage(int f_id)
{
	for(std::list<mapping>::iterator map = mapping_list.begin(); map!=mapping_list.end(); map++) {
		if ((map->f_id==f_id) && (map->dimension==PRESENCE)) {
			Pm_WriteShort(midi_out,0,Pm_Message(NOTE_ON,(char)map->control,127));
			//else Pm_WriteShort(midi_out,0,Pm_Message((char)map->channel,(char)map->control,(char)0));
		}
	}
}


void MidiServer::sendRemoveMessage(int f_id)
{
	for(std::list<mapping>::iterator map = mapping_list.begin(); map!=mapping_list.end(); map++) {
		if ((map->f_id==f_id) && (map->dimension==PRESENCE)) {
			Pm_WriteShort(midi_out,0,Pm_Message(NOTE_OFF,(char)map->control,127));
			//else Pm_WriteShort(midi_out,0,Pm_Message((char)map->channel,(char)map->control,(char)0));
		}
	}
}

void MidiServer::sendNullMessages()
{
	for(std::list<mapping>::iterator map = mapping_list.begin(); map!=mapping_list.end(); map++) {
		if (map->dimension==PRESENCE) Pm_WriteShort(midi_out,0,Pm_Message(NOTE_OFF,(char)map->control,127));
		else Pm_WriteShort(midi_out,0,Pm_Message((char)map->channel,(char)map->control,(char)0) );
	}
}


MidiServer::~MidiServer()
{
	// terminate MIDI
	Pm_Close(midi_out);
	Pm_Terminate();
}
