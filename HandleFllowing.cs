using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleFllowing : MonoBehaviour
{
    public Transform handleObject;
    public Rigidbody handlePhyscis;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(handleObject.transform.position);
        rb.velocity = handlePhyscis.velocity;
        rb.angularVelocity = handlePhyscis.angularVelocity;
    }
}
