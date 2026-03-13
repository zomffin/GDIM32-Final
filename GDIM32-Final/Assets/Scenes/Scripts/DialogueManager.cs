using TMPro;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private float _interactionDistance = 2.0f;
    [SerializeField] private DialogueUI _dialogue;
    [SerializeField] private DialogueNode _dialogueStartNode;
    [SerializeField] private DialogueNode _questCompleteNode;

    private DialogueNode _currentNode;
    private int _currentLine = 0;
    private bool _runningDialogue;
    private bool _waitingForPlayerResponse;

    void Start()
    {
        _currentNode = _dialogueStartNode;
        GameController.Instance.Cauldron.item += HandleItemRecieved;

    }


    public void Update()
    {


        if (Vector3.Distance(transform.position, GameController.Instance.Player.transform.position) <= _interactionDistance)
        {


            if (!_waitingForPlayerResponse && (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.E)))
            {
                AdvanceDialogue();
            }
            else if (!_runningDialogue)
            {


            }
        }
        else
        {
            EndDialogue();
        }

    }

    private void AdvanceDialogue()
    {
        _runningDialogue = true;

        if (_currentLine < _currentNode._lines.Length)
        {
            // if we still have NPC lines left, keep playing NPC lines
            _dialogue.ShowDialogue(_currentNode._lines[_currentLine]);
            _currentLine++;
        }
        else if (_currentNode._playerReplyOptions != null && _currentNode._playerReplyOptions.Length > 0)
        {
            // show player dialogue options, if there are any
            _waitingForPlayerResponse = true;
            Cursor.lockState = CursorLockMode.Confined;
            _dialogue.ShowPlayerOptions(_currentNode._playerReplyOptions);
        }
        else
        {
            // if there are no NPC or player lines left, close dialogue UI
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        _runningDialogue = false;
        _waitingForPlayerResponse = false;
        _currentNode = _dialogueStartNode;
        _currentLine = 0;
        _dialogue.HideDialogue();

    }

    public void SelectedOption(int option)
    {
        _currentLine = 0;
        _waitingForPlayerResponse = false;
        Cursor.lockState = CursorLockMode.Locked;
        _currentNode = _currentNode._npcReplies[option];
        AdvanceDialogue();
    }
    public void HandleItemRecieved(bool correctItem)
    {
        if (correctItem)
        {
            _currentNode = _questCompleteNode;
            _currentLine = 0;

        }
    }
}
