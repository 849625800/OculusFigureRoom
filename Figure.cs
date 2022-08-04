using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class Figure : MonoBehaviour
{
    public string figureName;
    private FigureContainer container;


    [Range(10,30)]
    public float rotatingSpeed;
    public bool onDisplay;
    private float rotatingValue;
    public bool isGrabbed;

    // Start is called before the first frame update
    void Start()
    {
        isGrabbed = false;
        onDisplay = true;
        rotatingValue = 360;
        InitFigure();

        if (gameObject.tag == "FigureItem")
        {
           gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, -180);
        }
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (onDisplay)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    void InitFigure()
    {
        //set rigidbody
        gameObject.AddComponent<Rigidbody>();
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        //set layer (layer6 :interaction : not collide with player)
        gameObject.layer = 6;

        //set shader, collider
        Shader toon = Resources.Load<Shader>("Shaders/MultiSteps");

        var skinMesh = GetComponentInChildren<SkinnedMeshRenderer>();

        //if have skinned mesh renderer:
        if (skinMesh != null)
        {
            foreach (Material m in skinMesh.materials)
            {
                m.shader = toon;
            }
            if (gameObject.tag == "Figure")
            {
                CapsuleCollider cc = gameObject.AddComponent<CapsuleCollider>();
                cc.center = new Vector3(0, skinMesh.bounds.extents.y, 0);
                cc.radius = 0.35f;
                cc.height = skinMesh.bounds.size.y;
            }
            else if (gameObject.tag == "FigureItem")
            {
                MeshCollider mc = gameObject.AddComponent<MeshCollider>();
                mc.sharedMesh = skinMesh.sharedMesh;
                mc.convex = true;
            }
        }
        else
        {
            //else get the mesh renderer.
            MeshRenderer meshR = GetComponentInChildren<MeshRenderer>();

            foreach (Material m in meshR.materials)
            {
                m.shader = toon;
            }
            if (gameObject.tag == "Figure")
            {
                CapsuleCollider cc = gameObject.AddComponent<CapsuleCollider>();
                cc.center = new Vector3(0, skinMesh.bounds.extents.y, 0);
                cc.radius = 0.35f;
                cc.height = skinMesh.bounds.size.y;
            }
            else if (gameObject.tag == "FigureItem")
            {
                MeshCollider mc = gameObject.AddComponent<MeshCollider>();
                mc.sharedMesh = GetComponentInChildren<MeshFilter>().mesh;
                mc.convex = true;
            }
        }

        var arm = gameObject.transform.Find("Armature");
        if (arm != null) arm.rotation = Quaternion.Euler(0, 0, 0);

        if (gameObject.tag == "Figure")
        {
            this.gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animation/FigureAction") as RuntimeAnimatorController;
            rb.mass = 50;
        }


        //mount grab transformers.
        gameObject.AddComponent<FigureOneGrabFreeTransformer>();
        FigureOneGrabFreeTransformer ot = gameObject.GetComponent<FigureOneGrabFreeTransformer>();
        ot.figure = this;


        gameObject.AddComponent<FigureTwoGrabFreeTransformer>();
        FigureTwoGrabFreeTransformer tt = gameObject.GetComponent<FigureTwoGrabFreeTransformer>();
        tt.figure = this;
        tt.Constraints = new FigureTwoGrabFreeTransformer.TwoGrabFreeConstraints();
        FloatConstraint fc_Min = new FloatConstraint();
        fc_Min.Constrain = true;
        fc_Min.Value = 0.15f;
        tt.Constraints.MinScale = fc_Min;

        FloatConstraint fc_Max = new FloatConstraint();
        fc_Max.Constrain = true;
        fc_Max.Value = 1.0f;
        tt.Constraints.MaxScale = fc_Max;


        //add grabbable
        gameObject.AddComponent<Grabbable>();
        Grabbable g = gameObject.GetComponent<Grabbable>();
        g.InjectOptionalOneGrabTransformer(ot);
        g.InjectOptionalTwoGrabTransformer(tt);

        gameObject.AddComponent<PhysicsGrabbable>();
        PhysicsGrabbable pg = gameObject.GetComponent<PhysicsGrabbable>();
        pg.InjectOptionalScaleMassWithSize(false);
        pg.InjectRigidbody(rb);
        pg.InjectGrabbable(g);

        // add interactable.
        GameObject hgi = Instantiate(new GameObject("HandGrabInteractable"));
        hgi.GetComponent<Transform>().position = new Vector3(0, 0, 0);
        hgi.transform.SetParent(gameObject.transform, false);
        HandGrabInteractable h = hgi.AddComponent<HandGrabInteractable>();
        h.InjectOptionalPointableElement(g);
        h.InjectRigidbody(rb);
        h.InjectOptionalPhysicsGrabbable(pg);

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "FigureContainer")
        {
            container = other.gameObject.GetComponent<FigureContainer>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "FigureContainer")
        {
            onDisplay = false;
        }
    }

    public void setContainerEmpty()
    {
        if (container == null) return;
        container.isEmpty = true;
    }



}
