using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FigureBoxManager : MonoBehaviour {

	public TMP_FontAsset newFont;
	public void ModifyFont() 
	{
		Component[] components = GetComponentsInChildren(typeof(TextMeshProUGUI), true);

		foreach (TextMeshProUGUI c in components) {
			c.font = newFont;			
		}
	}


	public Vector3 newCenter;
	public Vector3 newSize;
	public void ModifyBoxCollider() 
	{
		Component[] components = GetComponentsInChildren(typeof(FigureContainer), true);

		foreach (FigureContainer f in components) {
			BoxCollider bc = f.gameObject.GetComponent<BoxCollider>();
			bc.center = newCenter;
			bc.size = newSize;
		}
	}

}