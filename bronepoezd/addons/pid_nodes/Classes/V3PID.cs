using Godot;
using System;

namespace PID
{
    /// <summary>
    /// Class implementing PID controller functional for Vector3
    /// </summary>
    public class V3PID : BaseV3PID
    {
    
        public V3PID() : base()
        {
            
        }

        public V3PID(float p, float i, float d):base(p, i, d)
        {
        }

        /// <summary>
        /// Generates new vector based on existing error, prev errors and delta
        /// </summary>
        /// <param name="error">difference between actual vector and target vector</param>
        /// <param name="delta">Time interval</param>
        public Vector3 newVector(Vector3 Current, Vector3 Desired, float delta)
        {
            Vector3 _Error = Desired-Current*Scale;
            return UpdateWithError(_Error,delta);
        }
    }
}