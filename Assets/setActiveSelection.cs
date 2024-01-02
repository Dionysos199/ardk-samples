using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setActiveSelection : MonoBehaviour
{
    [SerializeField]
    GameObject highlight;
    bool active = false;
    public void setactive()
    {
        active = !active;
        highlight.SetActive(active);
    }
}
