using TMPro;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class ChooseWitchManager : MonoBehaviour
{
    [SerializeField] private float _interactionDistance = 2.0f;
    [SerializeField] private DialogueUI _dialogue;
    [SerializeField] private DialogueNode _dialogueStartNode;
    [SerializeField] private GameObject _chooseYourWitch;

    private DialogueNode _currentNode;
    private int _currentLine = 0;
    private bool _runningDialogue;
    private bool _waitingForPlayerResponse;

    void Start()
    {
        _currentNode = _dialogueStartNode;

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
            Cursor.lockState = CursorLockMode.None;
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
        _waitingForPlayerResponse = false;

        if (option == 0)
        {
            Debug.Log("0");
            GameController.Instance.Cauldron.RecieveQuest("fish");
            GameController.Instance.Cauldron.RecieveWitch("Chaser");
        }

        else if (option == 1)
        {
            Debug.Log("You Choose Chaser");
            GameController.Instance.Cauldron.RecieveQuest("Mushroom");
            GameController.Instance.Cauldron.RecieveWitch("BabyYaga");

        }
        Debug.Log("This is Working");
        Cursor.lockState = CursorLockMode.None;
        EndDialogue();
        _chooseYourWitch.SetActive(false);
    }
}
