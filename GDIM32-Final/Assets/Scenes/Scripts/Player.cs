using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Rigidbody _playerRigidbody; 

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _turnSpeed;

    [SerializeField] private GameObject targetPos; 
    
    [SerializeField] LayerMask raycastLayers;

    private Vector3 _angles; 
    private float _horizontal;
    private float _vertical;
    private Vector3 _horizontalmovement;
    private Vector3 _forwardmovement; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
     

        
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        
        _horizontalmovement = _playerTransform.right * _horizontal;
        _forwardmovement = _playerTransform.forward * _vertical; 
        
        
        //this.transform.position = _playerTransform.position; 
        
        
        float rotationY = Input.GetAxis("Mouse Y") * _turnSpeed;
        float rotationX = Input.GetAxis("Mouse X") * _turnSpeed;
        
        if (rotationY > 0)
        {
            _angles = new Vector3(Mathf.MoveTowards(_angles.x, -80, rotationY), _angles.y + rotationX, 0);
        }
        else
        {
            _angles = new Vector3(Mathf.MoveTowards(_angles.x, 80, -rotationY), _angles.y + rotationX, 0);
        }

        
        _playerRigidbody.MoveRotation(_playerRigidbody.rotation * Quaternion.Euler(new Vector3(0, rotationX * _turnSpeed, 0)));

        this.transform.position = _playerTransform.position; 
        
        transform.localEulerAngles = _angles;


    }

    private void FixedUpdate()
    {
        _playerRigidbody.velocity = (_horizontalmovement + _forwardmovement) * _moveSpeed;
    }
}
