using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform _playerRigidbody; 

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
        
    }

    // Update is called once per frame
    void Update()
    {
     

        
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        
        _horizontalmovement = transform.right * _horizontal;
        _forwardmovement = transform.forward * _vertical; 
        
        
        this.transform.position = playerTransform.position; 
        
        
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

        transform.localEulerAngles = _angles;
        this.transform.position = playerTransform.position; 
        
        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * sensitivityX, 0)));


        _angles.y = 0;
        playerTransform.localEulerAngles = _angles;

    }
}
