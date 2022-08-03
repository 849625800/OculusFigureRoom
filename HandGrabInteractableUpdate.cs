using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrabInteractableUpdate : MonoBehaviour
{
    public Figure mountedFigure; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //undate the grab point position refer to the changing scale of the figure.
        float scale = mountedFigure.GetComponent<Transform>().localScale.y;
        float size = mountedFigure.GetComponentInChildren<SkinnedMeshRenderer>().bounds.extents.y;
        gameObject.GetComponent<Transform>().position = new Vector3(0,scale * size ,0);
    }
}
