using System;
using System.Threading;
using System.Collections;
using IRTaktiks.Listener.Oscpack;

namespace IRTaktiks.Listener
{
    public struct tuio
    {
        public int f_id;
        public float xpos, ypos, angle;

        public tuio(int id, float x, float y, float a)
        {
            this.f_id = id;
            this.xpos = x;
            this.ypos = y;
            this.angle = a;
        }

        public void update(float x, float y, float a)
        {
            this.xpos = x;
            this.ypos = y;
            this.angle = a;
        }
    }

    public class TuioClient
    {
        private bool listening = false;
        private int port = 3333;
        private OSCReceiver receiver;
        private Thread thread;

        private Hashtable objectList = new Hashtable(32);
        private ArrayList aliveObjectList = new ArrayList(32);
        private ArrayList newObjectList = new ArrayList(32);
        private ArrayList aliveCursorList = new ArrayList(16);
        private ArrayList newCursorList = new ArrayList(16);

        private int currentFrame = 0;
        private int lastFrame = 0;

        private ArrayList listenerList = new ArrayList();

        public TuioClient() { }

        public TuioClient(int port)
        {
            this.port = port;
        }

        public int getPort()
        {
            return port;
        }

        public void connect()
        {
            try
            {
                receiver = new OSCReceiver(port);
                startListening();
            }
            catch
            { }
        }

        public void disconnect()
        {
            stopListening();
        }

        private void startListening()
        {
            listening = true;
            thread = new Thread(new ThreadStart(listen));
            thread.Start();
        }

        private void stopListening()
        {
            receiver.Close();
            listening = false;
            receiver = null;
        }

        private void listen()
        {
            while (listening)
            {
                try
                {
                    OSCPacket packet = receiver.Receive();
                    if (packet != null)
                    {
                        if (packet.IsBundle())
                        {
                            ArrayList messages = packet.Values;
                            for (int i = 0; i < messages.Count; i++)
                            {
                                processMessage((OSCMessage)messages[i]);
                            }
                        }
                        else processMessage((OSCMessage)packet);
                    }
                    else Console.WriteLine("null packet");
                }
                catch (Exception)
                { }
            }
        }

        private void processMessage(OSCMessage message)
        {
            string address = message.Address;
            ArrayList args = message.Values;
            string command = (string)args[0];

            if (address == "/tuio/2Dobj")
            {
                if ((command == "set") && (currentFrame >= lastFrame))
                {
                    int s_id = (int)args[1];
                    int f_id = (int)args[2];
                    float x = (float)args[3];
                    float y = (float)args[4];
                    float a = (float)args[5];
                    float X = (float)args[6];
                    float Y = (float)args[7];
                    float A = (float)args[8];
                    float m = (float)args[9];
                    float r = (float)args[10];

                    //Console.WriteLine(s_id+" "+f_id+" "+x+" "+y+" "+a+" "+X+" "+Y+" "+A+" "+m+" "+r);

                    if (!objectList.ContainsKey(args[1]))
                    {
                        tuio t = new tuio(f_id, x, y, a);
                        objectList.Add(s_id, t);

                        for (int i = 0; i < listenerList.Count; i++)
                        {
                            TuioListener listener = (TuioListener)listenerList[i];
                            
                            if (listener != null)
                            {
                                listener.addTuioObj(s_id, f_id, x, y, a, X, Y, A, m, r);
                                listener.updateTuioObj(s_id, f_id, x, y, a, X, Y, A, m, r);
                            }
                        }
                    }
                    else
                    {
                        tuio t = (tuio)objectList[s_id];
                        if ((t.xpos != x) || (t.ypos != y) || (t.angle != a))
                        {
                            for (int i = 0; i < listenerList.Count; i++)
                            {
                                TuioListener listener = (TuioListener)listenerList[i];
                                if (listener != null) listener.updateTuioObj(s_id, f_id, x, y, a, X, Y, A, m, r);
                            }
                            //Console.WriteLine(t.xpos +" " + t.ypos + " " + t.angle);
                            t.update(x, y, a);
                            objectList[s_id] = t;
                        }
                    }

                }
                else if ((command == "alive") && (currentFrame >= lastFrame))
                {
                    for (int i = 1; i < args.Count; i++)
                    {
                        // get the message content
                        newObjectList.Add((int)args[i]);
                        // reduce the object list to the lost objects
                        if (aliveObjectList.Contains(args[i]))
                            aliveObjectList.Remove(args[i]);
                    }

                    // remove the remaining objects
                    for (int i = 0; i < aliveObjectList.Count; i++)
                    {
                        int s_id = (int)aliveObjectList[i];
                        int f_id = ((tuio)objectList[aliveObjectList[i]]).f_id;
                        objectList.Remove(aliveObjectList[i]);
                        //Console.WriteLine("remove "+f_id);
                        for (int j = 0; j < listenerList.Count; j++)
                        {
                            TuioListener listener = (TuioListener)listenerList[j];
                            if (listener != null) listener.removeTuioObj(s_id, f_id);
                        }
                    }

                    ArrayList buffer = aliveObjectList;
                    aliveObjectList = newObjectList;

                    // recycling of the ArrayList
                    newObjectList = buffer;
                    newObjectList.Clear();

                }
                else if (command == "fseq")
                {
                    lastFrame = currentFrame;
                    currentFrame = (int)args[1];
                    if (currentFrame == -1) currentFrame = lastFrame;

                    if (currentFrame >= lastFrame)
                    {
                        for (int i = 0; i < listenerList.Count; i++)
                        {
                            TuioListener listener = (TuioListener)listenerList[i];
                            if (listener != null) listener.refresh();
                        }
                    }
                }

            }

            else if (address == "/tuio/2Dcur")
            {

                if ((command == "set") && (currentFrame >= lastFrame))
                {
                    int s_id = (int)args[1];
                    float x = (float)args[2];
                    float y = (float)args[3];
                    float X = (float)args[4];
                    float Y = (float)args[5];
                    float m = (float)args[6];

                    for (int i = 0; i < listenerList.Count; i++)
                    {
                        TuioListener listener = (TuioListener)listenerList[i];
                        if (listener != null) listener.updateTuioCur(s_id, x, y, X, Y, m);
                    }
                }
                else if ((command == "alive") && (currentFrame >= lastFrame))
                {

                    for (int i = 1; i < args.Count; i++)
                    {
                        // get the message content
                        newCursorList.Add(args[i]);
                        
                        // reduce the object list to the lost objects
                        if (aliveCursorList.Contains(args[i]))
                            aliveCursorList.Remove(args[i]);
                        else
                        {
                            for (int j = 0; j < listenerList.Count; j++)
                            {
                                TuioListener listener = (TuioListener)listenerList[j];
                                if (listener != null)
                                    listener.addTuioCur((int)args[i]);
                            }
                        }
                    }

                    // remove the remaining objects
                    for (int i = 0; i < aliveCursorList.Count; i++)
                    {
                        int s_id = (int)aliveCursorList[i];
                        //Console.WriteLine("remove "+s_id);
                        for (int j = 0; j < listenerList.Count; j++)
                        {
                            TuioListener listener = (TuioListener)listenerList[j];
                            if (listener != null) listener.removeTuioCur(s_id);
                        }
                    }

                    ArrayList buffer = aliveCursorList;
                    aliveCursorList = newCursorList;

                    // recycling of the ArrayList
                    newCursorList = buffer;
                    newCursorList.Clear();
                }
            }
        }

        public void addTuioListener(TuioListener listener)
        {
            listenerList.Add(listener);
        }

        public void removeTuioListener(TuioListener listener)
        {
            listenerList.Remove(listener);
        }
    }
}
