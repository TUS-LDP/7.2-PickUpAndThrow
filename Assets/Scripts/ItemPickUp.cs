using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickUp : MonoBehaviour
{
    public Transform hand;
    public float pickUpSpeed;
    public float throwForce;

    private PickableItem item;
    private bool pickingUp;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (pickingUp) 
        {
            PickUpItem(item);
        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Item" && !pickingUp)
        {
            item = hit.gameObject.GetComponent<PickableItem>();

            pickingUp = true;
        }
    }

    private void PickUpItem(PickableItem item)
    {
        if (item != null)
        {
            item.transform.localRotation = Quaternion.identity;

            item.transform.position = Vector3.Lerp(item.transform.position, hand.position, pickUpSpeed * Time.deltaTime);

            if (Vector3.Distance(item.transform.position, hand.position) < 0.25)
            {
                item.transform.position = hand.position;

                pickingUp = false;

                item.rb.isKinematic = true;

                item.transform.parent = hand;
            }
        }
        else
        {
            pickingUp = false;
        }
    }

    public void OnDrop(InputAction.CallbackContext context)
    {
        if (item != null)
        {
            item.transform.parent = null;
            pickingUp = false;
            item.rb.isKinematic = false;
            item = null;
        }
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (item != null)
        {
            item.transform.parent = null;
            item.rb.isKinematic = false;
            item.rb.AddForce(GameManager.Instance.mainCamera.transform.forward * throwForce);
        }
    }
}
