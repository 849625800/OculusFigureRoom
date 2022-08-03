using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class LocomotionHandTrackController : MonoBehaviour
{
    public OVRCameraRig CameraRig;
    //public CharacterController CharacterController;
    public CapsuleCollider CharacterController;
    //public OVRPlayerController PlayerController;
    public SimpleCapsuleWithHandTrackingMovement PlayerController;

    void Start()
    {
        /*
        if (CharacterController == null)
        {
            CharacterController = GetComponentInParent<CharacterController>();
        }
        Assert.IsNotNull(CharacterController);
		*/
        //if (PlayerController == null)
        //{
        //PlayerController = GetComponentInParent<OVRPlayerController>();
        //}
        //Assert.IsNotNull(PlayerController);
        if (CameraRig == null)
        {
            CameraRig = FindObjectOfType<OVRCameraRig>();
        }
        Assert.IsNotNull(CameraRig);
#if UNITY_EDITOR
        OVRPlugin.SendEvent("locomotion_controller", (SceneManager.GetActiveScene().name == "Locomotion").ToString(), "sample_framework");
#endif
    }
}
