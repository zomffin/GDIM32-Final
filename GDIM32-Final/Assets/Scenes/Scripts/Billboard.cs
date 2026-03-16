using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform _player;
    private Transform _this; 
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameController.Instance.Player.transform; 
        _this = transform;
    }

    // Update is called once per frame
    void Update()
    {
        _this.LookAt(_player.position);
    }
}
