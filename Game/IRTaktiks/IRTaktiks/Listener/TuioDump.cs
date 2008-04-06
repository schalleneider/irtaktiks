using System;
using IRTaktiks.Input;

namespace IRTaktiks.Listener
{
    public class TuioDump : TuioListener
    {
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
            InputManager.Instance.RaiseCursorUpdate((int)s_id, new Microsoft.Xna.Framework.Vector2(0, 0));
        }

        public void updateTuioCur(long s_id, float x, float y, float X, float Y, float m)
        {
            InputManager.Instance.RaiseCursorUpdate((int)s_id, new Microsoft.Xna.Framework.Vector2(x, y));
        }

        public void removeTuioCur(long s_id)
        {
            InputManager.Instance.RaiseCursorUp((int)s_id, new Microsoft.Xna.Framework.Vector2(0, 0));
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
