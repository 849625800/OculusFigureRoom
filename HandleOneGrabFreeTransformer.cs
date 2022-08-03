using UnityEngine;

namespace Oculus.Interaction
{
    /// <summary>
    /// A Transformer that moves the target in a 1-1 fashion with the GrabPoint.
    /// Updates transform the target in such a way as to maintain the target's
    /// local positional and rotational offsets from the GrabPoint.
    /// </summary>
    public class HandleOneGrabFreeTransformer : MonoBehaviour, ITransformer
    {

        private IGrabbable _grabbable;
        private Pose _previousGrabPose;
        public Transform handle;

        public void Initialize(IGrabbable grabbable)
        {
            _grabbable = grabbable;
        }

        public void BeginTransform()
        {
            Pose grabPoint = _grabbable.GrabPoints[0];
            _previousGrabPose = grabPoint;
        }

        public void UpdateTransform()
        {
            Pose grabPoint = _grabbable.GrabPoints[0];
            var targetTransform = _grabbable.Transform;

            Vector3 worldOffsetFromGrab = targetTransform.position - _previousGrabPose.position;
            Vector3 offsetInGrabSpace = Quaternion.Inverse(_previousGrabPose.rotation) * worldOffsetFromGrab;
            Quaternion rotationInGrabSpace = Quaternion.Inverse(_previousGrabPose.rotation) * targetTransform.rotation;

            targetTransform.position = (grabPoint.rotation * offsetInGrabSpace) + grabPoint.position;
            targetTransform.rotation = grabPoint.rotation * rotationInGrabSpace;

            _previousGrabPose = grabPoint;
        }

        public void EndTransform() 
        {
            var targetTransform = _grabbable.Transform;
            targetTransform.position = handle.position;
            targetTransform.rotation = handle.rotation;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}
