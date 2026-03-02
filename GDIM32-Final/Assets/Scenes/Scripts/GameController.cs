using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public CauldronController Cauldron { get; private set; }
    [SerializeField] int _itemcount = 0;
    [SerializeField] int _totalItemRequired = 2;

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

    }
    // Start is called before the first frame update
    void Start()
    {
        Cauldron.ItemRecieved += HandleItemRecieved;


    }

    // Update is called once per frame
    void Update()
    {
        if (_itemcount >= _totalItemRequired)
        {
            SceneManager.LoadScene("WinScreen");
        }

    }
    public void HandleItemRecieved(int item)
    {
        _itemcount += item;
        Debug.Log(_itemcount);
    }
}
