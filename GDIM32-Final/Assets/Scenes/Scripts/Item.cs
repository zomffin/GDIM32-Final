using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private float _timeToCorrect;

    protected Transform _targetPos; 
    protected Rigidbody _rigidbody;
    protected bool _pickedUp = false;
    protected Vector3 _velocity = Vector3.zero; 
    
    
    // you have to copy Start and FixedUpdate into child classes cuz they dont inherit ts :-( 
    void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
        
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
