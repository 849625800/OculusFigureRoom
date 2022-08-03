using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCapsuleWithHandTrackingMovement : MonoBehaviour
{

	public bool EnableLinearMovement = false;
	public bool EnableRotation = true;
	public bool HMDRotatesPlayer = false;
	public bool RotationEitherThumbstick = false;
	public float RotationAngle = 45.0f;
	public float Speed = 0.0f;
	public OVRCameraRig CameraRig;

	private bool ReadyToSnapTurn;
	private Rigidbody _rigidbody;

	public event Action CameraUpdated;
	public event Action PreCharacterMove;


	[SerializeField] private ActiveStateSelector MoveEnablePose;
	[SerializeField] private ActiveStateSelector MoveDisablePose;
	[SerializeField] private ActiveStateSelector[] MoveForwardPose;
	[SerializeField] private ActiveStateSelector[] MoveBackPose;
	[SerializeField] private ActiveStateSelector[] MoveLeftPose;
	[SerializeField] private ActiveStateSelector[] MoveRightPose;
	[SerializeField] private ActiveStateSelector[] MoveStopPose;

	[SerializeField] private ActiveStateSelector SnapTurnLeftGesture;
	[SerializeField] private ActiveStateSelector SnapTurnRightGesture;

	private Vector3 direction;

	private enum Direction 
	{
		forward, back, left, right, stop
	}

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		if (CameraRig == null) CameraRig = GetComponentInChildren<OVRCameraRig>();
	}

	void Start()
	{
		MoveEnablePose.WhenSelected += () => LinearMoveTrigger(true);
		MoveDisablePose.WhenSelected += () => LinearMoveTrigger(false);

		foreach (ActiveStateSelector f in MoveForwardPose)
		{
			f.WhenSelected += () => HandTrackDirection(Direction.forward);
		}

		foreach (ActiveStateSelector b in MoveBackPose)
		{
			b.WhenSelected += () => HandTrackDirection(Direction.back);
		}

		foreach (ActiveStateSelector l in MoveLeftPose)
		{
			l.WhenSelected += () => HandTrackDirection(Direction.left);
		}

		foreach (ActiveStateSelector r in MoveRightPose)
		{
			r.WhenSelected += () => HandTrackDirection(Direction.right);
		}

		foreach (ActiveStateSelector s in MoveStopPose)
		{
			s.WhenSelected += () => HandTrackDirection(Direction.stop);
		}

		SnapTurnLeftGesture.WhenSelected += () => SnapTurn(false);
		SnapTurnRightGesture.WhenSelected += () => SnapTurn(true);
	}

	private void FixedUpdate()
	{
		if (CameraUpdated != null) CameraUpdated();
		if (PreCharacterMove != null) PreCharacterMove();
		if (HMDRotatesPlayer) RotatePlayerToHMD();
		if (EnableLinearMovement) HandTrackMovement();
		//if (EnableRotation) SnapTurn();
	}

	void RotatePlayerToHMD()
	{
		Transform root = CameraRig.trackingSpace;
		Transform centerEye = CameraRig.centerEyeAnchor;

		Vector3 prevPos = root.position;
		Quaternion prevRot = root.rotation;

		transform.rotation = Quaternion.Euler(0.0f, centerEye.rotation.eulerAngles.y, 0.0f);

		root.position = prevPos;
		root.rotation = prevRot;
	}

	void HandTrackDirection(Direction d)
	{
		if (!EnableLinearMovement) return;

		Quaternion ort = CameraRig.centerEyeAnchor.rotation;
		Vector3 ortEuler = ort.eulerAngles;
		ortEuler.z = ortEuler.x = 0f;
		ort = Quaternion.Euler(ortEuler);

		Vector3 moveDir = Vector3.zero;
		//Vector2 primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

		switch (d) {
			case Direction.forward: moveDir += ort * Vector3.forward;
				break;
			case Direction.back: moveDir += ort * Vector3.back;
				break;
			case Direction.left:  moveDir += ort * Vector3.left;
				break;
			case Direction.right:  moveDir += ort * Vector3.right;
				break;
			default:break;
		}
		Debug.Log(d);
		//_rigidbody.MovePosition(_rigidbody.transform.position + moveDir * Speed * Time.fixedDeltaTime);
		//_rigidbody.MovePosition(_rigidbody.position + moveDir * Speed * Time.fixedDeltaTime);
		direction = moveDir;
	}

	void HandTrackMovement() 
	{
		_rigidbody.MovePosition(_rigidbody.position + direction * Speed * Time.fixedDeltaTime);
	}

	void LinearMoveTrigger(bool isEnable) 
	{
		EnableLinearMovement = isEnable;
		direction = Vector3.zero;
	}

	void SnapTurn(bool isClockwise)
	{
		if (!EnableLinearMovement) return;
		if (isClockwise)
		{
			transform.RotateAround(CameraRig.centerEyeAnchor.position, Vector3.up, RotationAngle);
		}
		else
		{
			transform.RotateAround(CameraRig.centerEyeAnchor.position, Vector3.up, -RotationAngle);
		}
	}
}
