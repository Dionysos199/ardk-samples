using UnityEngine;
using XDPaint.Tools.Raycast.Base;
using XDPaint.Tools.Raycast.Data;

namespace XDPaint.Controllers.InputData
{
    public class InputDataVR : InputDataMesh
    {
        private Transform penTransform;

        public override void Init(PaintManager paintManagerInstance, Camera camera)
        {
            base.Init(paintManagerInstance, camera);
            penTransform = InputController.Instance.PenTransform;
        }
        
        protected override void OnHoverSuccess(int fingerId, Vector3 position, RaycastData raycast)
        {
            var data = InputData[fingerId];
            data.Position = -Vector3.one;
            data.Ray = new Ray(penTransform.position, penTransform.forward);
            RaycastController.Instance.RequestRaycast(PaintManager, data.Ray.Value, fingerId, data.PreviousPosition, position, container =>
            {
                OnHoverSuccessEnd(container, fingerId, position);
            });
            data.PreviousPosition = position;
        }
        
        protected override void OnHoverSuccessEnd(RaycastRequestContainer request, int fingerId, Vector3 position)
        {
            var data = InputData[fingerId];
            if (data.RaycastData == null)
            {
                data.RaycastData = RaycastController.Instance.TryGetRaycast(request);
            }
            
            if (data.RaycastData != null)
            {
                data.Position = Camera.WorldToScreenPoint(data.RaycastData.WorldHit);
                OnHoverSuccessHandlerInvoke(fingerId, data.Position, data.RaycastData);
            }
            else
            {
                base.OnHoverFailed(fingerId);
            }
        }
        
        protected override void OnDownSuccess(int fingerId, Vector3 position, float pressure = 1.0f)
        {
            var data = InputData[fingerId];
            data.Position = position;
            if (data.Ray == null)
            {
                data.Ray = new Ray(penTransform.position, penTransform.forward);
            }
            
            if (data.RaycastData == null)
            {
                RaycastController.Instance.RequestRaycast(PaintManager, data.Ray.Value, fingerId, data.PreviousPosition, position, container =>
                {
                    OnDownSuccessCallback(container, fingerId, position, pressure);
                });
                data.PreviousPosition = position;
            }
        }
        
        protected override void OnDownSuccessCallback(RaycastRequestContainer request, int fingerId, Vector3 position, float pressure = 1.0f)
        {
            var data = InputData[fingerId];
            if (data.RaycastData == null)
            {
                data.RaycastData = RaycastController.Instance.TryGetRaycast(request);
            }

            if (data.RaycastData == null)
            {
                OnDownFailed(fingerId, position, pressure);
            }
            else
            {
                data.Position = Camera.WorldToScreenPoint(data.RaycastData.WorldHit);
                OnDownSuccessInvoke(fingerId, data.Position, pressure, data.RaycastData);
            }
        }

        protected override void OnPressSuccess(int fingerId, Vector3 position, float pressure = 1.0f)
        {
            var data = InputData[fingerId];
            data.Position = position;
            if (data.Ray == null)
            {
                data.Ray = new Ray(penTransform.position, penTransform.forward);
            }

            if (data.RaycastData == null)
            {
                RaycastController.Instance.RequestRaycast(PaintManager, data.Ray.Value, fingerId, data.PreviousPosition, position, container =>
                {
                    OnPressSuccessCallback(container, fingerId, position, pressure);
                });
                data.PreviousPosition = position;
            }
        }

        protected override void OnPressSuccessCallback(RaycastRequestContainer request, int fingerId, Vector3 position, float pressure = 1.0f)
        {
            var data = InputData[fingerId];
            if (data.RaycastData == null)
            {
                data.RaycastData = RaycastController.Instance.TryGetRaycast(request);
            }

            if (data.RaycastData == null)
            {
                OnPressFailed(fingerId, position, pressure);
            }
            else
            {
                data.Position = Camera.WorldToScreenPoint(data.RaycastData.WorldHit);
                OnPressSuccessInvoke(fingerId, data.Position, pressure, data.RaycastData);
            }
        }
    }
}