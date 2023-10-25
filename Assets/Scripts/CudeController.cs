using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CudeController : MonoBehaviour
{
    private InputController _input;

    // Start is called before the first frame update
    void Start()
    {
        _input = GameManager.Instance.GetComponent<InputController>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((_input != null) && (_input.destroyCude))
        {
            Destroy(gameObject);
        }
    }
}
