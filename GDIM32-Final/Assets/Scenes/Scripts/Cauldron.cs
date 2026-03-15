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

    public delegate void newWitchQuest(string newWitch);
    public event newWitchQuest witchquest;


    [SerializeField] private string _questItem;
    private bool _questComplete = true;

    private string _questWitch;
    private string _questWitchActivated;

    [SerializeField] private TMP_Text _questText;




    void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.name);
        if (_questItem == null)
        {
            Debug.Log("You don't have a quest");
            return;
        }

        Debug.Log(_questItem + " is quest item, sending out event");

        if (collision.name.Contains(_questItem))
        {
            item?.Invoke(true);
            _questText.text = "";
            Destroy(collision.gameObject);
        }
        if (collision.name.Contains(_questWitchActivated))
        {
            Debug.Log("You Winn!!!");
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
    public void RecieveWitch(string newWitch)
    {
        Debug.Log(newWitch);
        _questWitch = newWitch;
    }
    public void ActivateWitch()
    {
        _questText.text = _questWitch;
        _questWitchActivated = _questWitch;
        witchquest?.Invoke(_questWitchActivated);
    }
}
