using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cauldron : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if (collision.name == "test item")
        {
            SceneManager.LoadScene("WinScreen");
        }
        Debug.Log("Cooking");
    }
}
