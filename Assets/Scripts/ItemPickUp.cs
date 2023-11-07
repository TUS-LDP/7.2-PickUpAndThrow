using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItempPickUp : MonoBehaviour
{
    public Transform hand;
    public float pickUpSpeed;
    public float rotationSpeed;
    public float mouseSensitivity;

    private InputController _input;

    private bool pickingUp;
    private PickableItem item;

    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        _input = GameManager.Instance.GetComponent<InputController>();
    }

    // Update is called once per frame
    void Update()
    {
        // if we are in the process of picking up an item then
        // call, continuously, the PickUpItem function and pass 
        // it the item we want to pickup. This function lerps the
        // item towards the hand position
        if (pickingUp)
        {
            PickUpItem(item);
        }

        if (_input.dropItem)
        {
            DropItem();
        }

        if (_input.rotateItem)
        {
            RotateItem();
        }

    }

    // This is an event function that Unity will call if the CharacterController
    // collider hits another collider
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // It the Character Controller attached to this game object has hit
        // a Collider on another game object, and that object is tagged
        // "Item" AND we are not currently in the process of picking something
        // up, then ...
        if (hit.gameObject.tag == "Item" && !pickingUp)
        {
            // Get the PickableItem component off the Game Object that we
            // have just hit
            item = hit.gameObject.GetComponent<PickableItem>();

            // pickingUp is avariable that indicates that we are in the 
            // process of picking up an item i.e. lerping it towars the
            // characters hand.
            pickingUp = true;
        }
    }

    private void PickUpItem(PickableItem item)
    {
        // If the item is not equal to nothing, then ...
        // Remember that the item variable should contain the PickableItem component of the
        // game object we hit. If the game object we hit does not have a PickableItem then
        // item will be equal to null meaning we can't pick it up i.e. we can only pickup
        // items that have the PickableItem script attached to them.
        if (item != null)
        {
            // Set the rotation of the item to 0
            item.transform.localRotation = Quaternion.identity;

            // Lerp the item to the hand position. Everytime the Lerp function is called the item will be
            // moved closer to the hand position.
            item.transform.position = Vector3.Lerp(item.transform.position, hand.position, Time.deltaTime * pickUpSpeed);

            // If we are at, or pretty close to, the hand position then ...
            if (Vector3.Distance(item.transform.position, hand.position) < 0.25)
            {
                // Marking pickingUp as false as we have now finished picking the
                // item up
                pickingUp = false;

                // Set the items parent to be the hand (whose parent is the character) so that
                // when the character (and the hand) moves so too does the item
                item.transform.parent = hand;

                // Set the isKinematic property of the 
                item.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                xRotation = item.transform.localRotation.x;
            }
        }
        else
        {
            pickingUp = false;
        }
    }

    private void DropItem()
    {
        if (item != null)
        {
            item.transform.parent = null;
            pickingUp = false;
            item.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            item = null;
        }
    }

    private void RotateItem()
    {
        if (item != null)
        {
            float mouseX = _input.look.x * rotationSpeed * Time.deltaTime;
            float mouseY = _input.look.y * rotationSpeed * Time.deltaTime;

            // Rotate the item arounds the hands axis in world space
            item.transform.Rotate(hand.transform.up * mouseX * -1, Space.World);
            item.transform.Rotate(hand.transform.right * mouseY, Space.World);
        }
    }

}