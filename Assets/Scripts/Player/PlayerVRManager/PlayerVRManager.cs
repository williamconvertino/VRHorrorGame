using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerVRManager : MonoBehaviour
{
    [Header("Desktop")]
    [SerializeField] private GameObject _desktopParent;
    
    [Header("VR")]
    [SerializeField] private GameObject _vrParent;
    [SerializeField] private CharacterControllerDriver _vrDriver;
    
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
        _vrParent.SetActive(_vrModeEnabled);
        _vrDriver.enabled = _vrModeEnabled;
        _desktopParent.SetActive(!_vrModeEnabled);
    }

}
