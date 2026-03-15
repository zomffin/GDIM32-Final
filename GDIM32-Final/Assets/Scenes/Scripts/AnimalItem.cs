using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalItem : NPCs
{
    [SerializeField] int ID = 1;
    // This is a child class that doesn't implement anything new from the abstract Item. 
    // We can use this for all items until we have time to try doing special interactions 
    private void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
        _player = GameController.Instance.Player.transform;
        
        _detectTimer = 0; 
        if (_animator == null)
        {
            _hasAnimator = false;
        }
    }

    private void Update()
    {

        UpdateState();
        RunState();


        _detectTimer -= Time.deltaTime;
        _scaredTimer -= Time.deltaTime; 
    }

    
    private void FixedUpdate()
    {
        if (_pickedUp)
        {
            Move();
        }
    }
}
