using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureManager : MonoBehaviour {

	public void ModifyTwoGrabConstrain () {
		Component[] components = GetComponentsInChildren (typeof (FigureTwoGrabFreeTransformer), true);

		foreach (var c in components) {
			//DestroyImmediate (c);
			FigureTwoGrabFreeTransformer T = c as FigureTwoGrabFreeTransformer;
			T.Constraints.ConstraintsAreRelative = false;

			FloatConstraint fc_Min = new FloatConstraint();
			fc_Min.Constrain = true;
			fc_Min.Value = 0.15f;
			T.Constraints.MinScale = fc_Min;

			FloatConstraint fc_Max = new FloatConstraint();
			fc_Max.Constrain = true;
			fc_Max.Value = 1.0f;
			T.Constraints.MaxScale = fc_Max;

		}
	}


	public Shader newShader;
	public Shader unlitShader;
	public void ChangeShaders() 
	{
		Component[] components = GetComponentsInChildren(typeof(SkinnedMeshRenderer), true);

		foreach (SkinnedMeshRenderer c in components) {
			foreach (Material m in c.materials) {
				m.shader = newShader;
			}
			

		}
	}

    private void Start()
    {
		ChangeShaders();
    }

}