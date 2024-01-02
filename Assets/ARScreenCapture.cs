using UnityEngine;
using UnityEngine.UI;


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
    void CaptureScreenshot()
    {
        Debug.Log("a photo was taken");
            ScreenCapture.CaptureScreenshot(Application.dataPath+"/AR_Screenshot.png",2);
    }
}
