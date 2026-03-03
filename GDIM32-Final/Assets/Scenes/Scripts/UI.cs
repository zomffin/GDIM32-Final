using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Image _checkImage;
    [SerializeField] private Sprite _filledCheckSprite;

    private CauldronController _cauldron;

    private void Start()
    {
        _cauldron = FindObjectOfType<CauldronController>();

        if (_cauldron != null)
        {
            _cauldron.ItemRecieved += OnItemReceived;
        }

        _checkImage.enabled = false; 
    }

    private void OnItemReceived(int amount)
    {
        _checkImage.enabled = true;
        _checkImage.sprite = _filledCheckSprite;
    }
}