using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemThrow : MonoBehaviour
{
    public float throwForce;

    private InputController _input;


    // Start is called before the first frame update
    void Start()
    {
        _input = GameManager.Instance.GetComponent<InputController>();

        if (throwForce == 0)
        {
            throwForce = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.throwItem)
        {
            Debug.Log("Throwing ...");
            PickableItem theItem = GetComponentInChildren(typeof(PickableItem)) as PickableItem;

            if (theItem != null)
            {
                // Get the forward direction of the eyes before we unparent the
                // item from the hand
                Vector3 eyesForward = GameObject.Find("MainCamera").transform.forward;

                // Unparent the item from the hand
                theItem.transform.parent = null;

                // Make sure isKinematic is false so that it can take a force
                theItem.GetRigidbody().isKinematic = false;

                // Now throw it
                theItem.GetRigidbody().AddForce(eyesForward * throwForce);
            }
        }
    }
}
