using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.XR.Management;

public class VRManager : MonoBehaviour
{
    public static VRManager Instance { private set; get; }

    #region Core
    public bool IsVRActive { private set; get; }

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
        
        //By default it trys to connect to the headset.
        SetVRActiveStatus(true);
    }

    private void Update()
    {
        if (!IsVRActive && IsHeadsetDetected())
        {
            IsVRActive = true;
        }
    }
    #endregion

    #region Set Status
    //Trys to set the status to the desired status and returns the new status. 

    public bool SetVRActiveStatus(bool status)
    {
        if (status == IsVRActive)
        {
            return IsVRActive;
        }

        if (status && IsHeadsetDetected(true))
        {
            EnableVR();
        }
        else
        {
            EnableDesktop();    
        }
        return IsVRActive;
    }

    private void EnableVR()
    {
        Debug.Log("VR Mode Loaded");
        IsVRActive = true;
    }

    public void EnableDesktop()
    {
        Debug.Log("Desktop Mode Loaded");
        IsVRActive = false;
    }
    #endregion

    #region Detect Status
    private bool IsHeadsetDetected(bool showLog = false)
    {
        var xrSettings = XRGeneralSettings.Instance;
        if (xrSettings == null)
        {
            if (showLog)
            {
                Debug.Log("Could not find xrSettings");
            }
            return false;
        }
        
        var xrManager = xrSettings.Manager;
        if (xrManager == null)
        {
            if (showLog)
            {
                Debug.Log("Could not find xrManager");
            }
            return false;
        }
        
        var xrLoader = xrManager.activeLoader;
        if (xrLoader == null)
        {
            if (showLog)
            {
                Debug.Log("Could not find xrLoader");
            }
            return false;
        }

        return true;
    }
    #endregion
}
