using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FigureManager))]
public class FigureEditor : Editor
{

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		FigureManager rmc = (FigureManager)target;

		if (GUILayout.Button("ModifyTwoGrabConstrain"))
		{
			rmc.ModifyTwoGrabConstrain();
		}

		if (GUILayout.Button("ChangeShaders"))
		{
			rmc.ChangeShaders();
		}
	}
}