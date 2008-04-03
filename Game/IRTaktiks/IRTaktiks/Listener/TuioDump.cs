using System;
using IRTaktiks.Input;

namespace IRTaktiks.Listener
{
    public class TuioDump : TuioListener
    {
        public void addTuioObj(long session_id, int fiducial_id, float xpos, float ypos, float angle, float x_speed, float y_speed, float r_speed, float m_accel, float r_accel)
        {
            InputManager.Instance.RaiseObjectAdded(fiducial_id, new Microsoft.Xna.Framework.Vector2(xpos * 800, xpos * 600), angle);
        }

        public void updateTuioObj(long s_id, int f_id, float x, float y, float a, float X, float Y, float A, float m, float r)
        {
            InputManager.Instance.RaiseObjectUpdated(f_id, new Microsoft.Xna.Framework.Vector2(x * 800, y * 600), a);
        }

        public void removeTuioObj(long s_id, int f_id)
        {
            InputManager.Instance.RaiseObjectRemoved(f_id);
        }

        public void addTuioCur(long session_id)
        {
        }

        public void updateTuioCur(long s_id, float x, float y, float X, float Y, float m)
        {
            InputManager.Instance.RaiseCursorDown((int)s_id, new Microsoft.Xna.Framework.Vector2(x, y));
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
