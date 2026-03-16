using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class NPCs : Item
{
    protected enum NPCsState
    {
        Idle, Wandering, Pursued, PickedUp
    }


    [SerializeField] protected Transform _player;
    [SerializeField] protected LayerMask _lineOfSightLayers;
    [SerializeField] protected float _wanderTimeMax = 5.0f;
    [SerializeField] protected float _obstacleCheckDistance = 1.0f;
    [SerializeField] protected float _obstacleCheckRadius = 1.0f;
    [SerializeField] protected float _stopDistance = 0.5f;
    
    [SerializeField] protected float _rotateSpeed;
    [SerializeField] protected float _walkSpeed;
    [SerializeField] protected float _runSpeed;
    
    [SerializeField] protected Vector3 _raycastStartOffset;
    [SerializeField] protected Rigidbody _rigidBody;
    [SerializeField] protected MeshRenderer _renderer;
    [SerializeField] protected Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _grabSound;

    protected string _playerTag = "PlayerObj";
    protected NPCsState _state;
    protected float _wanderTime;
    protected Vector3 _wanderDirection;
    public event Action OnPickedUp;

    [SerializeField] protected float _detectCool;
    protected float _detectTimer;
    [SerializeField] protected float _scaredCool; 
    protected float _scaredTimer;

    // to be removed when all npcs have animators
    protected bool _hasAnimator = true;

    // variables used for drawing Gizmos
    protected Vector3 _raycastHitLocation;
    protected Vector3 _spherecastHitLocation;
    protected bool _hasLineOfSightToPlayer;
    protected Vector3 _meToPlayer;
    protected Vector3 _meAwayPlayer;


    protected Vector3 _runaway;

    
    

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

    //     // our position + raycast offset, in world space
    //     // _raycastStartOffset is used to make sure the raycast starts a little above the ground
    //     // TransformPoint is used to take the offset from local to world space
    private Vector3 _raycastStart
    {
        get
        {
            return transform.TransformPoint(_raycastStartOffset);
        }
    }
    
  private void Grab()
{
    if (_audioSource != null && _grabSound != null)
    {
        _audioSource.PlayOneShot(_grabSound);
    }
}


public void PickUp()
{
    if (_pickedUp) return; // prevents double pickup

    _pickedUp = true;

    Grab(); // play sound immediately

    if (_hasAnimator)
    {
        _animator.SetBool("_IsCaught", true);
    }
}
    private void Update()
    {

        UpdateState();
        RunState();


        _detectTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (_pickedUp)
        {
            Move();
        }
        
        if (this.transform.position.y < -5)
        {
            Correct(); 
        }
    }

    public void UpdateState()
    {
        if (_pickedUp)
        {
            _state = NPCsState.PickedUp;
        }
        else if (_hasLineOfSightToPlayer || _scaredTimer > 0)
        {
            _state = NPCsState.Pursued;
            //Debug.Log("wahhhh");
        }
        else if (_scaredTimer <= 0)
        {
            _state = NPCsState.Wandering;
            //Debug.Log("im normal");
        }
        
        //Debug.Log("I'm in " + _state + "state");
        
    }

    //     // suggested improvement: 
    //     // this state machine is curretly overkill because there's only 2 states
    //     // but if we were to want to implement state transitions,
    //     // like maybe if the duck wanted to finish turning before it started walking in a new direction,
    //     // then changing states would matter and this state machine would help with that!
    public void RunState()
    {
        switch (_state)
        {
            case NPCsState.Wandering:
                RunWanderState();
                break;

            case NPCsState.Pursued:
                RunPursueState();
                break;

            case NPCsState.PickedUp:
                if (_hasAnimator)
                {
                    _animator.SetBool("_IsCaught", true);
                }
                //implement fighting later
                break;
            default:
                Debug.LogError("unhandled state " + _state);
                break;
        }
    }



    protected void RunWanderState()
    {
        if (_hasAnimator)
        {
            _animator.SetBool("_IsCaught", false);
        }

        // switches to a new random direction every [_wanderTimeMax] seconds
        _wanderTime -= Time.deltaTime;
        if (_wanderTime <= 0.0f)
        {
            _wanderTime = _wanderTimeMax;
            GetNewWanderDirection();
        }

        // checks for obstacles, and gets a new direction if there are any
        // limit attempts per frame so we don't crash program if duck gets stuck
        int attempts = 0;
        while (HasCloseObstacles() && attempts < 3)
        {
            GetNewWanderDirection();
            attempts++;
        }

        // actually rotate towards and move in wander direction
        RotateTowards(_wanderDirection);
        transform.Translate(_wanderDirection * _walkSpeed * Time.deltaTime, Space.World);
    }

    protected void GetNewWanderDirection()
    {
        // get a random 2d location inside a circle and treat it as a direction
        Vector3 randomDir = UnityEngine.Random.insideUnitCircle;
        _wanderDirection = new Vector3(randomDir.x, 0.0f, randomDir.y);
        _wanderDirection = _wanderDirection.normalized;
    }

    protected bool HasCloseObstacles()
    {
        // do a spherecast in the direction we want to move in
        // if we hit anything, we'll check a new direction
        RaycastHit hitInfo;
        bool hasObstacle = Physics.SphereCast(
            _raycastStart,
            _obstacleCheckRadius,
           _wanderDirection,
            out hitInfo,
            _obstacleCheckDistance
        );

        if (hasObstacle)
        {
            _spherecastHitLocation = hitInfo.point;
        }

        return hasObstacle;
    }

    protected void RunPursueState()
    {
        _scaredTimer -= Time.deltaTime;

        if (_hasAnimator)
        {
            _animator.SetBool("_IsCaught", false);
        }
        
        // zero out y-axis because we only care about moving on x/z plane (ground)
        Vector3 playerPos = _player.position;
        playerPos = new Vector3(playerPos.x, 0, playerPos.z);

        // get vector pointing from duck to target point
        Vector3 me = new Vector3(transform.position.x, 0, transform.position.z);
        
        _meAwayPlayer = (me - playerPos).normalized;

        // checks for obstacles, and gets a new direction if there are any
        // limit attempts per frame so we don't crash program if duck gets stuck
        int attempts = 0;
        while (HasCloseObstacles() && attempts < 3)
        {
            Vector3 randomDir = UnityEngine.Random.insideUnitCircle;
            _runaway = new Vector3(randomDir.x, 0.0f, randomDir.y);
            attempts++;
        }
        
        
        RotateTowards(_meAwayPlayer);
        WalkTowards(_runaway);
    }

    protected void RotateTowards(Vector3 direction)
    {
        Vector3 currentForward = new Vector3(transform.forward.x, 0, transform.forward.z);
        Vector3 newForward = Vector3.RotateTowards(currentForward, direction, _rotateSpeed * Time.deltaTime, 0.0f);
        transform.forward = newForward;
    }

    protected void WalkTowards(Vector3 point)
    {
        Vector3 me = new Vector3(transform.position.x, 0, transform.position.z);

        if (Vector3.Distance(me, point) <= _stopDistance)
        {
            // exit early if i'm already close to player
            return;
        }

        // create a vector pointing from our position to the target position
        Vector3 meToTarget = point - me;
        meToTarget = meToTarget.normalized;

        // move in that direction
        transform.Translate(meToTarget * _runSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_playerTag) && _detectTimer <= 0)
        {
            //Debug.Log("player entered sight");
            _scaredTimer = _scaredCool; 
            _hasLineOfSightToPlayer = true;
        }
        else
        {
            //Debug.Log("dat is not the player");
            _hasLineOfSightToPlayer = false; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(_playerTag) && _detectTimer <= 0)
        {
            _detectTimer = _detectCool; 
        }
    }
    

    private void OnDrawGizmos()
    {
        // don't draw these gizmos unless game is running
        if (!Application.isPlaying) return;

        // draw player raycast stuff
        if (_hasLineOfSightToPlayer)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        if (_player != null) Gizmos.DrawSphere(_player.position, 0.1f);
        Gizmos.DrawSphere(_raycastHitLocation, 0.1f);

        // draw direction we want to move in based on state we're in 
        if (_state == NPCsState.Wandering)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(transform.position, _wanderDirection);

            // also visualize spherecast checking for obstacles
            Gizmos.DrawWireSphere(_raycastStart, _obstacleCheckRadius);
            Gizmos.DrawWireSphere(_raycastStart + _wanderDirection * _obstacleCheckDistance, _obstacleCheckRadius);

            // draw spherecast hit location
            Gizmos.DrawSphere(_spherecastHitLocation, 0.1f);
        }
        else if (_state == NPCsState.Pursued)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawRay(transform.position, _meToPlayer);
        }

        Gizmos.color = Color.magenta;
        
        Gizmos.DrawSphere(_runaway, 0.1f);
    }
}





