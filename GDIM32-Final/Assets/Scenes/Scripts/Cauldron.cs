using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if (collision.name == "test item")
        {
            Debug.Log(collision.name);
        }
        Debug.Log("Cooking");
    }
}
