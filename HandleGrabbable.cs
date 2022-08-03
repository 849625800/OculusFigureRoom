using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleGrabbable : OVRGrabbable
{
    public Transform handle;
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(Vector3.zero, Vector3.zero);

        transform.position = handle.transform.position;
        transform.rotation = handle.transform.rotation;
    }
}
