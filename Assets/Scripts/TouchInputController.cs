using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TouchInputController : MonoBehaviour
{
    private float initialPinchDistance;
    private Vector3 initialScale;
    private GameObject selectedObject;
    [SerializeField]
    private float scaleSensitivity = 8;

    private bool deleteMode = false;

    [SerializeField]
    private GameObject photoCanvas;
    [SerializeField]
    private Texture[] photos;
    private GameObject lastSelectedObject;

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                // Raycast to determine the touched object
                selectedObject = GetTouchedObject(touch1.position) ?? GetTouchedObject(touch2.position);

                if (selectedObject != null)
                {
                    // Store initial pinch distance and object scale
                    initialPinchDistance = Vector2.Distance(touch1.position, touch2.position);
                    initialScale = selectedObject.transform.localScale;
                }
            }
            else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                if (selectedObject != null)
                {
                    //selectedObject.GetComponent<MeshRenderer>().material.color = Color.red; 
                    // Calculate pinch distance change
                    float currentPinchDistance = Vector2.Distance(touch1.position, touch2.position);
                    float pinchDelta = currentPinchDistance - initialPinchDistance;

                    // Scale based on pinch distance change
                    Vector3 newScale = initialScale + Vector3.one * pinchDelta * scaleSensitivity/1000;
                    selectedObject.transform.localScale = newScale;
                }
            }

            // Rotate based on touch delta position
            if (touch1.phase == TouchPhase.Moved)
            {
                RotateObject(touch1.deltaPosition);
            }

            if (touch2.phase == TouchPhase.Moved)
            {
                RotateObject(touch2.deltaPosition);
            }
        }
        else if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (deleteMode)
                {
                    // Raycast to determine the touched object and delete it
                    Debug.Log("deleted");
                    GameObject touchedObject = GetTouchedObject(touch.position);
                    if (touchedObject != null)
                    {
                        Destroy(touchedObject);
                    }
                }
                else
                {
                    GameObject touchedObject = GetTouchedObject(touch.position);
                    if (touchedObject != null)
                    {
                        if (touchedObject.CompareTag("SpawnedObject"))
                        {
                            Debug.Log("Start animation");
                            slideInFn();
                            lastSelectedObject = touchedObject;
                        }
                    }
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                
            }
        }
    }

    void RotateObject(Vector2 delta)
    {
        float rotationSpeed = 0.5f;
        if (selectedObject != null)
        {
            selectedObject.transform.Rotate(Vector3.up, -delta.x * rotationSpeed);
        }
    }

    GameObject GetTouchedObject(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("SpawnedObject"))
            {
                return hit.collider.gameObject;
            }
        return null;
    }
    public void ToggleDeleteMode()
    {
        deleteMode = !deleteMode;
        Debug.Log("Delete Mode: " + deleteMode);
    }
    public void slideInFn()
    {
        if (photoCanvas != null)
        {
            Animator animator = photoCanvas.GetComponent<Animator>();
            if (animator != null)
            {
               animator.SetBool("slideIn", true);
            }
        }
    }
    public void slideOutFn()
    {
        if (photoCanvas != null)
        {
            Animator animator = photoCanvas.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("slideIn", false);
            }
        }
    }
    public void addPhotoFn(int n)
    {
        if (photos[n] != null)
        {
            lastSelectedObject.GetComponent<MeshRenderer>().material.mainTexture = photos[n];
        }
        //NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        //{
        //    if (path != null)
        //    {
        //        // Load the image from the selected path

        //        Texture2D myTexture = NativeGallery.LoadImageAtPath(path);

        //        // Apply the texture to the plane object
        //        spawnedObject.GetComponent<Renderer>().material.mainTexture = myTexture;
        //    }
        //});
    }
}
