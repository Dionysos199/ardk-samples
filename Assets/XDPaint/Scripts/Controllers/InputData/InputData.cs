using UnityEngine;
using XDPaint.Tools.Raycast.Data;

namespace XDPaint.Controllers.InputData
{
    public class InputData
    {
        public Ray? Ray;
        public RaycastData RaycastData;
        public Vector3 Position;
        public Vector3? PreviousPosition;
    }
}