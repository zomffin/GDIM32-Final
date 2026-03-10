using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private RawImage _checkImage;
    [SerializeField] private Sprite _filledCheckSprite;


    private CauldronController _cauldron;

    private void Start()
    {
        

        if (_cauldron != null)
        {
            GameController.Instance.Cauldron.item += OnItemReceived;
        }

        _checkImage.enabled = false; 
    }

    private void OnItemReceived(bool check)
    {
        _checkImage.enabled = true;
    }
}