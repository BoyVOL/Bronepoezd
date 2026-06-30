using System;
using Godot;

namespace PID
{
    /// <summary>
    /// Parent Class of  PID Controllers
    /// </summary>
    public class BaseV3PID
    {
        public float P;
        public float I;
        public float D;

        public float Scale = 1;

        protected Vector3 _PrevError = Vector3.Zero;
        protected Vector3 _ErrorIntegral = Vector3.Zero;

        
        public BaseV3PID(){
            P = 1;
            I = 1;
            D = 1;
        }

        public BaseV3PID(float p, float i, float d)
        {
            P=p;
            I=i;
            D=d;
        }

        /// <summary>
        /// Resets all error values
        /// </summary>
        public void Reset()
        {
            _PrevError = Vector3.Zero;
            _ErrorIntegral = Vector3.Zero;
        }

        protected Vector3 UpdateWithError(Vector3 _Error,float delta)
        {
            _Error *= Scale;
            _ErrorIntegral += _Error*delta;
            Vector3 ErrorD = (_Error - _PrevError)/delta;
            _PrevError = _Error;
            return P*_Error+I*_ErrorIntegral+D*ErrorD;
        }
    }
}