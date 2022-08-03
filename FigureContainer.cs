using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FigureContainer : MonoBehaviour
{
    public bool isEmpty;
    private string figureName;
    private Color frameColor;
    private float alphaValue;

    public Vector3 _figureScale = new Vector3(0.15f, 0.15f, 0.15f);

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "FigureContainer";
        figureName = "啥都没有";
        isEmpty = true;
        frameColor = Color.yellow;
        alphaValue = 1.0f;

        //size of box collider
        BoxCollider bc = gameObject.GetComponent<BoxCollider>();
        bc.center = new Vector3(0,0.14f, -0.04f);
        bc.size = new Vector3(0.25f,0.3f,0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isEmpty)
        {
            figureName = "啥都没有";
            frameColor = Color.yellow;
        }
        //if not empty, show the info of the figure on the lable.
        SetFrame();

    }

    private void SetFrame() {
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>() as TextMeshProUGUI;

        if (text != null) text.SetText(figureName);

        //alphaValue = Mathf.PingPong(Time.time/3, 1.0f);
        MaterialPropertyBlockEditor editor = GetComponentInChildren<MaterialPropertyBlockEditor>();
        if (editor == null) return;

        MaterialPropertyBlock block = editor.MaterialPropertyBlock;
        frameColor.a = alphaValue;
        block.SetColor(Shader.PropertyToID("_BorderColor"), frameColor);
    }

    private void OnTriggerStay(Collider other)
    {
        DisplayFigure(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Figure" || other.tag == "FigureItem")
        {
            Figure f = other.GetComponentInParent(typeof(Figure)) as Figure;
            figureName = "啥都没有";
            isEmpty = true;
            f.onDisplay = false;
            frameColor = Color.yellow;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Figure f = other.GetComponentInParent(typeof(Figure)) as Figure;
        if (other.tag == "FigureItem")
        {
            f.onDisplay = true;
            f.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            //f.gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, -180);
            figureName = f.figureName;
            frameColor = Color.blue;
            isEmpty = false;

            if (!f.isGrabbed)
            {
                f.transform.localScale = _figureScale;
            }
        }

    }

    private void DisplayFigure(Collider other)
    {
        // if figure collide with the frame, take it in and give it a right transformation.
        if (other.tag == "Figure" || other.tag == "FigureItem")
        {
            Figure f = other.GetComponentInParent(typeof(Figure)) as Figure;
            if (!f.isGrabbed && isEmpty)
            {
                f.transform.position = transform.position;
                Vector3 rotate = GetComponent<Transform>().rotation.eulerAngles;
                Vector3 position = GetComponent<Transform>().position;
                if (other.tag == "Figure")
                {
                    f.transform.rotation = Quaternion.Euler(rotate.x, 180 + rotate.y, 0 + rotate.z);
                }
                else
                {
                    //f.transform.rotation = Quaternion.Euler(rotate.x, 180 + rotate.y, 0 + rotate.z);
                    f.gameObject.GetComponent<Transform>().position = new Vector3(position.x, 1.0f + position.y, position.z);
                    f.gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(rotate.x, 180 + rotate.y, rotate.z);
                    //f.transform.position = new Vector3(position.x, 1 + position.y, position.z);
                }

                f.onDisplay = true;
                f.transform.localScale = _figureScale;
                f.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                figureName = f.figureName;
                frameColor = Color.blue;
                isEmpty = false;
            }
        }
    }
}
