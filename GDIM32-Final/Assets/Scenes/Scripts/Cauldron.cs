using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;


public class CauldronController : MonoBehaviour
{

    public delegate void ItemRecieved(bool x);
    public event ItemRecieved item;

    public delegate void newQuest(string newItem);

    public event newQuest quest;


    private string _questItem;
    private bool _questComplete = true;

    [SerializeField] private TMP_Text _questText;




    void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.name);
        if (_questItem == null)
        {
            Debug.Log("You don't have a quest");
            return;
        }

        if (collision.name.Contains(_questItem))
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
        Debug.Log(newItem);
        _questText.text = newItem;
        if (_questComplete)
        {
            quest?.Invoke(newItem);
            _questItem = newItem;
            _questComplete = false;
        }
    }
}
