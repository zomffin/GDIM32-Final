using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Rigidbody _playerRigidbody; 

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _sprintSpeed; 
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _jumpForce;

    
    
    [Header("Interact")]
    // How far the interact should reach
    [SerializeField] private float _raycastDistance;
    // For raycasting for Items: Should ONLY have items layer selected or else it'll try to grab other stuff
    [SerializeField] LayerMask raycastLayers;
    // Empty game object that held items will snap to 
    [SerializeField] private GameObject targetPos;

    [SerializeField] private float _maxHoldDistance;
    [SerializeField] private float _minHoldDistance; 
    [SerializeField] private float _holdSensitivity;

    [SerializeField] private float _throwForce;
    
    
    
    
    // Everything needed for camera + player movement 
    private Vector3 _angles; 
    private float _horizontal;
    private float _vertical;
    private Vector3 _horizontalmovement;
    private Vector3 _forwardmovement;
    private float _sprintMod;
    private bool _isGrounded = true;
    
    private bool _hasItem = false;
    private Item _itemHeld; 
    
    
    void Start()
    {
        // Locks the cursor when u click into the game
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
     

        // Get WASD input, Raw means it can only be 0 or 1 (snappier movement)
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        
        _horizontalmovement = _playerTransform.right * _horizontal;
        _forwardmovement = _playerTransform.forward * _vertical;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _sprintMod = _sprintSpeed;
        }
        else
        {
            _sprintMod = 1; 
        }
        
        // Get mouse movement
        float rotationY = Input.GetAxis("Mouse Y") * _turnSpeed;
        float rotationX = Input.GetAxis("Mouse X") * _turnSpeed;
        
        // ill be real i lifted this off unity forums lmao . calculates the camera rotation somehow
        if (rotationY > 0)
        {
            _angles = new Vector3(Mathf.MoveTowards(_angles.x, -80, rotationY), _angles.y + rotationX, 0);
        }
        else
        {
            _angles = new Vector3(Mathf.MoveTowards(_angles.x, 80, -rotationY), _angles.y + rotationX, 0);
        }

        // Rotates player rigidbody on X axis ONLY 
        _playerRigidbody.MoveRotation(_playerRigidbody.rotation * Quaternion.Euler(new Vector3(0, rotationX * _turnSpeed, 0)));
        
        // Moves camera to player
        this.transform.position = _playerTransform.position; 
        
        // Rotates camera X + Y + Z
        transform.localEulerAngles = _angles;

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            Debug.Log("clicked space");
            _isGrounded = false; 
            _playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && _hasItem)
        {
            Throw(); 
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.E))
        {
            Interact(); 
        }
        
        float _mouseWheel = Input.GetAxisRaw("Mouse ScrollWheel");
        float _distanceToObject = Vector3.Distance(targetPos.transform.position, transform.position);
        
        if (_mouseWheel != 0f)
        {
            if (_mouseWheel < 0 && _distanceToObject > _minHoldDistance)
            {
                targetPos.transform.Translate(this.gameObject.transform.forward * _holdSensitivity * _mouseWheel, Space.World);
            } 
            else if (_mouseWheel > 0 && _distanceToObject < _maxHoldDistance)
            {
                targetPos.transform.Translate(this.gameObject.transform.forward * _holdSensitivity * _mouseWheel, Space.World);
            }
            
        }
        
        Debug.Log("Is grounded: " + _isGrounded);
        
    }

    private void FixedUpdate()
    {
        // Moves player- it's in fixed update because it's a physics call 
        Vector3 newvelocity = (_horizontalmovement + _forwardmovement) * _moveSpeed * _sprintMod;
        // Making sure the y value isn't replaced (not necessary for basic movement) also it messes with graviy if we dont lol 
        newvelocity.y = _playerRigidbody.velocity.y;
        _playerRigidbody.velocity = newvelocity; 
    }
    
    void OnCollisionEnter()
    {
        //THIS SHI ISNT WORKING HELP
        Debug.Log("on collision enter");
        _isGrounded = true;
    }

    // Method for interacting with items 
    private void Interact()
    {
        // If they already have an item in hand, interacting will drop it on the ground 
        if (_hasItem)
        {
            _itemHeld.Interact(targetPos);
            _itemHeld = null;
            _hasItem = false;
        }
        else
        {
            
            // If player doesn't have anything in hand, sends out a raycast
            RaycastHit hit;
            if (Physics.Raycast(
                    transform.position, 
                    transform.forward, 
                    out hit,
                    _raycastDistance,
                    raycastLayers))
            {
                // If raycast hits something: Fetch its Item component 
                _itemHeld = hit.collider.GameObject().GetComponent<Item>();
               
                Debug.Log("Succeeded raycast check");
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green, 10);
                
                // Call the item's interact function (the interact function will return "true" if it was picked up, "false" if it wasn't) 
                bool _successful = _itemHeld.Interact(targetPos);
                if (_successful)
                {
                    _hasItem = true; 
                }
                else
                {
                    _hasItem = false;
                    _itemHeld = null; 
                }

            }
            else
            {
                Debug.Log("Failed raycast check");
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red,10);
            }
        }
    }

    private void Throw()
    {
        _itemHeld.Interact(targetPos); 
        _itemHeld._rigidbody.AddForce(this.transform.forward * _throwForce, ForceMode.Impulse);
        _itemHeld = null;
        _hasItem = false;
    }
    
    
}
