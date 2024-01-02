using UnityEngine;

public class MoveObjectOnSurface : MonoBehaviour
{
    private bool isTouching = false;
    private GameObject touchedObject;
    private string targetTag = "SpawnSurface";

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    HandleTouchBegin(touch);
                    break;

                case TouchPhase.Moved:
                    HandleTouchMoved(touch);
                    break;

                case TouchPhase.Ended:
                    HandleTouchEnd();
                    break;
            }
        }
    }

    void HandleTouchBegin(Touch touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag(targetTag))
            {
                isTouching = true;
                touchedObject = hit.collider.gameObject;
                break; // Exit the loop after the first hit with the desired tag
            }
        }
    }

    void HandleTouchMoved(Touch touch)
    {
        if (isTouching && touchedObject != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag(targetTag))
            {
                // Move the object to the hit position with the surface
                MoveObjectToSurface(touchedObject, hit.point, hit.normal);
            }
        }
    }

    void HandleTouchEnd()
    {
        isTouching = false;
        touchedObject = null;
    }

    void MoveObjectToSurface(GameObject obj, Vector3 position, Vector3 normal)
    {
        // Move the object to the hit position with the surface
        obj.transform.position = position;

        // Keep the y axis of the object parallel to the normal of the surface
        obj.transform.rotation = Quaternion.LookRotation(obj.transform.right, normal);
    }
}
