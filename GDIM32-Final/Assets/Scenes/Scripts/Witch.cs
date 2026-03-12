using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Witch : NPCs
{
    private bool _isScared = false; 
    
    private DialogueManager _dialogueManager;
    
    
    // Start is called before the first frame update
    private void Start()
    {
        _dialogueManager = this.GetComponent<DialogueManager>();
        _rigidbody = this.GetComponent<Rigidbody>();
        _detectTimer = 0; 
        if (_animator == null)
        {
            _hasAnimator = false;
        }
    }

    // Update is called once per frame
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
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_playerTag) && _detectTimer <= 0)
        {
            Debug.Log("player entered sight");
            _scaredTimer = _scaredCool; 
            _hasLineOfSightToPlayer = true;
        }
        else
        {
            Debug.Log("dat is not the player");
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
    
    protected new void UpdateState()
    {
        if (!_isScared)
        {
            _state = NPCsState.Idle;
        }
        else if (_pickedUp)
        {
            _state = NPCsState.PickedUp;
        }
        else if (_hasLineOfSightToPlayer || _scaredTimer > 0)
        {
            _state = NPCsState.Pursued;
            Debug.Log("wahhhh");
        }
        else if (_scaredTimer <= 0)
        {
            _state = NPCsState.Wandering;
            _dialogueManager.enabled = false; 
            Debug.Log("im normal");
        }
    }
    
    protected new void RunState()
    {
        switch (_state)
        {
            case NPCsState.Idle:
                RunIdleState();
                break;
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

    private void RunIdleState()
    {
        
    }

    public override bool Interact(GameObject target)
    {
        if (!_isScared)
        {
            return false;
        }
        else
        {
            base.Interact(target);
            return true; 
        }
    }
    
}
