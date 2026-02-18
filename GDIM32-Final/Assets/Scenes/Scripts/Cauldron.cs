using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Cooking");
    }
}
