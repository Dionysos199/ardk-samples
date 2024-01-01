using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class FilePickerSystem : MonoBehaviour
{
    public string FinalPath;
    public TextMeshProUGUI path_txt;

    public int textureWidth;
    public int textureHeight;
    public Texture2D LoadFile()
    {
        Texture2D myTexture = new Texture2D(textureWidth, textureHeight);
        NativeGallery.Permission Permission = NativeGallery.GetImageFromGallery((path) =>
        {

            if (File.Exists(path))
            
            {
                 myTexture = NativeGallery.LoadImageAtPath(path);
            
            }
            else
            {
                Debug.LogError("this following path does not exist"+ path);
            }
        });
        return myTexture;

    }
    public void SaveFile()
    {

    // Create a dummy text file
    string filePath = Path.Combine(Application.temporaryCachePath, "test.text");
    File.WriteAllText(filePath, "Hello world!");

    // Export the file
   // NativeGallery.Permission permission = NativeGallery.ExportFile(filePath, (success) => Debug.Log("File exported:+ success"));
    }
    private IEnumerator OutputRoutine(string url)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(www);
               // output.texture = texture;
            }
        }
    }
}


