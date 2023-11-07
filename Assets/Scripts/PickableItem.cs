using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PickableItem : MonoBehaviour
{
    private Rigidbody theRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        theRigidBody = GetComponent<Rigidbody>();
    }


    public Rigidbody GetRigidbody()
    {
        return theRigidBody;
    }
}
