using System;

namespace IRTaktiks.Listener
{
    public interface TuioListener
    {
        void addTuioObj(long session_id, int fiducial_id, float xpos, float ypos, float angle, float x_speed, float y_speed, float r_speed, float m_accel, float r_accel);
        void updateTuioObj(long session_id, int fiducial_id, float xpos, float ypos, float angle, float x_speed, float y_speed, float r_speed, float m_accel, float r_accel);
        void removeTuioObj(long session_id, int fiducial_id);

        void addTuioCur(long session_id);
        void updateTuioCur(long session_id, float xpos, float ypos, float x_speed, float y_speed, float m_accel);
        void removeTuioCur(long session_id);

        void refresh();
    }
}
