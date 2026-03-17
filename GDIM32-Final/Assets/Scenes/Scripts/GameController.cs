using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public CauldronController Cauldron { get; private set; }
    public GameObject Player { get; private set; }

    [SerializeField] private TMP_Text _questComplete;


    private void Awake()
    {

        //This is not working it does not delete whywhwywhywhwywhy
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        //This works:D
        GameObject cauldronObj = GameObject.FindWithTag("Cauldron");
        Cauldron = cauldronObj.GetComponent<CauldronController>();

        Player = GameObject.FindWithTag("Player");
        if (Player == null)
        {
            Debug.Log("Cant find player");
        }
        else
        {
            Debug.Log("Found player");
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        Cauldron.item += HandleItemRecieved;
        Cauldron.witchquest += HandleWitchQuest;
        Cauldron.witch += HandleWitchRecieved;

    }

    public void HandleItemRecieved(bool correctItem)
    {
        if (correctItem)
        {
            _questComplete.text = "Quest Completed, Go back to Witch";
        }


    }
    public void HandleWitchQuest(string newWitch)
    {
        _questComplete.text = "Witch Needed: ";
    }
    public void HandleWitchRecieved()
    {
        SceneManager.LoadScene("WinScreen");
    }

}
