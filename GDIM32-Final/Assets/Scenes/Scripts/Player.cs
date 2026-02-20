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

    // Empty game object that held items will snap to 
    [SerializeField] private GameObject targetPos; 
    
    // For raycasting for Items: Should ONLY have items layer selected or else it'll try to grab other stuff
    [SerializeField] LayerMask raycastLayers;

    // Everything needed for camera + player movement 
    private Vector3 _angles; 
    private float _horizontal;
    private float _vertical;
    private Vector3 _horizontalmovement;
    private Vector3 _forwardmovement; 
    
    
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


    }

    private void FixedUpdate()
    {
        // Moves player- it's in fixed update because it's a physics call 
        _playerRigidbody.velocity = (_horizontalmovement + _forwardmovement) * _moveSpeed;
    }
}
