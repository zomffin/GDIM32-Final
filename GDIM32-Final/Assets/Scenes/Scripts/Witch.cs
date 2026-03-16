using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Witch : NPCs
{
    [SerializeField] private float _maxEscapeTimer;
    private float _escapeTime;
    private bool _isScared = false;

    private DialogueManager _dialogueManager;

    private Player _playerHand;


    // Start is called before the first frame update
    private void Start()
    {
        _dialogueManager = this.GetComponent<DialogueManager>();
        _rigidbody = this.GetComponent<Rigidbody>();
        _player = GameController.Instance.Player.transform;
        _playerHand = GameController.Instance.Player.GetComponent<Player>();


        _detectTimer = 0;
        if (_animator == null)
        {
            _hasAnimator = false;
        }
        GameController.Instance.Cauldron.witchquest += HandleWitchQuest;
    }

    // Update is called once per frame
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

                _escapeTime -= Time.deltaTime;
                if (_escapeTime <= 0)
                {
                    Escape();
                }

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
            _escapeTime = _maxEscapeTimer;
            return true;
        }
    }

    public void Escape()
    {
        _playerHand.Drop();
        _pickedUp = false;
        _state = NPCsState.Pursued;
        _rigidbody.useGravity = true;
    }

    public void HandleWitchQuest(string newWitch)
    {
        if (this.name.Contains(newWitch))
        {
            _isScared = true;
            _dialogueManager.enabled = false;
        }

    }

}
