using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private float _timeToCorrect;

    private Transform _targetPos; 
    private Rigidbody _rigidbody;
    private bool _pickedUp = false;
    private Vector3 _velocity = Vector3.zero; 
    

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
    }

    void FixedUpdate()
    {
        if (_pickedUp)
        {
            Move(); 
        }
    }
    
    public virtual void Interact(GameObject target)
    {
        Debug.Log("Interacted with " + this.name);
        if (!_pickedUp)
        {
            Debug.Log("Picked up");
            _pickedUp = true;
            target.transform.position = this.transform.position;
            _targetPos = target.transform; 
            _rigidbody.useGravity = false;
        }
        else
        {
            Debug.Log("Dropped"); 
            _pickedUp = false;
            _targetPos = null; 
            _rigidbody.useGravity = true;
        }
    }

    public virtual void Move()
    {
        float distance = Vector3.Distance(_targetPos.position, this.transform.position);   
        if (Mathf.Abs(distance) > 0.1)
        {
            transform.position = Vector3.SmoothDamp(this.transform.position, _targetPos.position, ref _velocity, _timeToCorrect);
        }
        else
        {
            _rigidbody.velocity = Vector3.zero; 
        }
    }
}
