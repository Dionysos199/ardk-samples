
using UnityEngine;
using UnityEngine.Events;

public class addPhoto : MonoBehaviour
{
    public GameObject definedButton;
    public UnityEvent OnClick = new UnityEvent();

    // Use this for initialization
    void Start()
    {
        definedButton = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (touch.phase == TouchPhase.Ended )
            {
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
                {
                    Debug.Log("Button Clicked");
                    OnClick.Invoke();
                }
            }
        }
    }

    // Update is called once per frame
    public void addPhotoFn()
    {

            NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
            {
                if (path != null)
                {
                    // Load the image from the selected path

                    Texture2D myTexture = NativeGallery.LoadImageAtPath(path);

                    // Apply the texture to the plane object
                    this.gameObject.GetComponent<Renderer>().material.mainTexture = myTexture;
                }
            });
    }
}
