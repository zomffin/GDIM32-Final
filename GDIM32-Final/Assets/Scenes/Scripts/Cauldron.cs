using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cauldron : MonoBehaviour
{
    [SerializeField] int _itemcount = 0;
    void OnTriggerEnter(Collider collision)
    {
        if (collision.name == "test item")
        {
            Destroy(collision.gameObject);
            _itemcount++;
            Debug.Log(_itemcount);
        }

    }
    void Update()
    {
        if (_itemcount >= 2)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
