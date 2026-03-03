using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CauldronController : MonoBehaviour
{

    public delegate void IntDelegate(int x);
    public event IntDelegate ItemRecieved;


    void OnTriggerEnter(Collider collision)
    {
        if (collision.name == "test item" || collision.name == "fish")
        {
            ItemRecieved?.Invoke(1);
            Destroy(collision.gameObject);

        }
    }
    void Update()
    {

    }
}
