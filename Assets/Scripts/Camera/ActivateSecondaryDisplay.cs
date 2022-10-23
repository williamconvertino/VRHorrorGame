using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSecondaryDisplay : MonoBehaviour
{
    // Activates all displays.
    void Start()
    {
        Debug.Log ("displays connected: " + Display.displays.Length);
    
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }
}
