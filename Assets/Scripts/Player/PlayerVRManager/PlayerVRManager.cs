using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVRManager : MonoBehaviour
{
    [SerializeField] private GameObject VRParent;
    [SerializeField] private GameObject DesktopParent;

    private bool _vrModeEnabled = false;

    private void Awake()
    {
        SetMode();
    }

    private void Update()
    {
        if (_vrModeEnabled != VRManager.Instance.IsVRActive)
        {
            SetMode();
        }
    }
    
    private void SetMode()
    {
        _vrModeEnabled = VRManager.Instance.IsVRActive;
        VRParent.SetActive(_vrModeEnabled);
        DesktopParent.SetActive(!_vrModeEnabled);
    }

}
