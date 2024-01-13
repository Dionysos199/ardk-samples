using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ARScreenCapture : MonoBehaviour
{
    public Button captureButton;

    void Start()
    {
        if (captureButton != null)
        {
            captureButton.onClick.AddListener(CaptureScreenshot);
        }
     
    }
    private IEnumerator Screenshot()
    {
       yield return new WaitForEndOfFrame();
       Texture2D texture2D = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false);
        texture2D.ReadPixels(new Rect(0, 0 , Screen.width, Screen.height),0,0);
        texture2D.Apply();
        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string fileName = "ScreenShot" + timeStamp + ".png";
        NativeGallery.SaveImageToGallery(texture2D, "BenchCanvas", fileName);

    }
    private void CaptureScreenshot()
    {
        StartCoroutine(Screenshot());
    }
}
