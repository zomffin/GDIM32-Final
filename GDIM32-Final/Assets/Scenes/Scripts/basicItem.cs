using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicItem : Item
{
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (_pickedUp)
        {
            Move(); 
        }
    }
}
