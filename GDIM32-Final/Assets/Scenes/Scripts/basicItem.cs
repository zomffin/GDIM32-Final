using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicItem : Item
{
    // This is a child class that doesn't implement anything new from the abstract Item. 
    // We can use this for all items until we have time to try doing special interactions 
    void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody>();

    }

    void Update()
    {
        if (_pickedUp)
        {
            Move(); 
        }
    }
}
