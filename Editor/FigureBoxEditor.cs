using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FigureBoxManager))]
public class FigureBoxEditor : Editor
{

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		FigureBoxManager fbm = (FigureBoxManager)target;

		if (GUILayout.Button("ModifyFont"))
		{
			fbm.ModifyFont();
		}

		if (GUILayout.Button("ModifyBoxCollider"))
		{
			fbm.ModifyBoxCollider();
		}
	}
}