using System;
using IRTaktiks.Input;

namespace IRTaktiks.Listener
{
    public class TuioDump : TuioListener
    {
        System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();

        public void addTuioObj(long session_id, int fiducial_id, float xpos, float ypos, float angle, float x_speed, float y_speed, float r_speed, float m_accel, float r_accel)
        { 
        }

        public void updateTuioObj(long s_id, int f_id, float x, float y, float a, float X, float Y, float A, float m, float r)
        {
        }

        public void removeTuioObj(long s_id, int f_id)
        {
        }

        public void addTuioCur(long s_id)
        {
        }

        public void updateTuioCur(long s_id, float x, float y, float X, float Y, float m)
        {
            if (list.Contains((int)s_id))
            {
                InputManager.Instance.RaiseCursorUpdate((int)s_id, new Microsoft.Xna.Framework.Vector2(1280 * x, 1024 * y));
            }
            else
            {
                list.Add((int)s_id);
                InputManager.Instance.RaiseCursorDown((int)s_id, new Microsoft.Xna.Framework.Vector2(1280 * x, 1024 * y));
            }
        }

        public void removeTuioCur(long s_id)
        {
            InputManager.Instance.RaiseCursorUp((int)s_id, new Microsoft.Xna.Framework.Vector2(0, 0));
            list.Remove((int)s_id);
        }

        public void refresh()
        {
        }

        public void Run(object args)
        {
            TuioDump demo = new TuioDump();
            TuioClient client = null;

            client = new TuioClient();

            if (client != null)
            {
                client.addTuioListener(demo);
                client.connect();
            }
        }
    }
}
