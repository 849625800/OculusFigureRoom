using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ApkURLtest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TextMeshPro>().SetText(Application.dataPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
