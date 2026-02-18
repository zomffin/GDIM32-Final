using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform playerTransform;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _turnSpeed;

    [SerializeField] private GameObject targetPos; 
    
    [SerializeField] LayerMask raycastLayers;

    private Vector3 _angles; 
    private float _horizontal;
    private float _vertical;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rotationY = Input.GetAxis("Mouse Y") * _turnSpeed;
        rotationY = Mathf.Clamp(rotationY, -60.0f, 60.0f);

        float rotationX = Input.GetAxis("Mouse X") * _turnSpeed;

        
        transform.localEulerAngles = new Vector3(-rotationY, 0, 0 );
        playerTransform.localEulerAngles = new Vector3(0, -rotationX, 0);
        
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        
        playerTransform.Translate((playerTransform.right * _horizontal + playerTransform.forward * _vertical) * _moveSpeed * Time.deltaTime);

        this.transform.position = playerTransform.position; 
        
        
        
        /*if (rotationY > 0)
        {
            _angles = new Vector3(Mathf.MoveTowards(_angles.x, -80, rotationY), _angles.y + rotationX, 0);
        }
        else
        {
            _angles = new Vector3(Mathf.MoveTowards(_angles.x, 80, -rotationY), _angles.y + rotationX, 0);
        }*/
        
        
        //transform.position = this.transform.position;

        /*_angles.y = 0; :-(
        playerTransform.localEulerAngles = _angles;
        */

    }
}
