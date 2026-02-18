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
    
    private float horizontal;
    private float vertical;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        
        playerTransform.Translate((playerTransform.right * horizontal + playerTransform.forward * vertical) * _moveSpeed * Time.deltaTime);
    }
}
