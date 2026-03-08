using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CauldronController : MonoBehaviour
{

    public delegate void ItemRecieved(bool x);
    public event ItemRecieved item;

    public delegate void newQuest(string newItem);

    public event newQuest quest; 

    
    private string _questItem;
    private bool _questComplete = true; 
    
    
    

    void OnTriggerEnter(Collider collision)
    {
        if (collision.name == _questItem)
        {
            item?.Invoke(true);
            Destroy(collision.gameObject);
        }
        else
        {
            item?.Invoke(false);
        }
    }

    void Update()
    {

    }

    public void RecieveQuest(string newItem)
    {
        if (_questComplete)
        {
            quest?.Invoke(newItem); 
            _questItem = newItem;
            _questComplete = false;
        }
    }
}
