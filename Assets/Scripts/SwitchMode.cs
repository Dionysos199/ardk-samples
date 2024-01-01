using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMode : MonoBehaviour
{
    [SerializeField]
    GameObject [] setfalse;

    [SerializeField]
    GameObject [] setTrue;
   public void switchModeFn()
    {
        foreach (var item in setfalse)
        {
            item.SetActive(false);
        }
        foreach (var item in setTrue)
        {
            item.SetActive(true);
        }
    }
}
