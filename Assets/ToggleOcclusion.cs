using Niantic.Lightship.AR.Occlusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ToggleOcclusion : MonoBehaviour
{
    public bool Occlusion;
    public void toggle()
    {
        Occlusion = !Occlusion;
        if (Occlusion)
        {
            setOcclusionOn();
        }
        else
        {
            setOcclusionOff();
        }
    }
    public void setOcclusionOn()
    {
        GetComponent<AROcclusionManager>().enabled = true;
        GetComponent<LightshipOcclusionExtension>().enabled = true;
    }
    public void setOcclusionOff()
    {
        GetComponent<AROcclusionManager>().enabled = false;
        GetComponent<LightshipOcclusionExtension>().enabled = false;
    }
}
