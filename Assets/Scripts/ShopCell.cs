using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopCell : MonoBehaviour
{
    [SerializeField] private GameObject _imageCheck;
    [SerializeField] private Sprite _correctCheck;
    [SerializeField] private Sprite _incorrectCheck;
    

    private PlayerInventory _playerInventory;

    private Item _item;
    private GameObject _itemPrefab;
    private Button _button;
    private SoundManager _soundManager;
    private bool _selected;
    

    public GameObject ItemPrefab => _itemPrefab;
    public Item Item => _item;
    public UnityEvent<Item> OnCellSelected;
    public UnityEvent<Item> OnCellUnselected;

    public Sprite CorrectCheck => _correctCheck;




    private void Start()
    {
        _button = GetComponent<Button>();
        _playerInventory = FindObjectOfType<PlayerInventory>();
        _soundManager = FindObjectOfType<SoundManager>();
    }

    public void SetItem(Item item)
    {
        this._item = item;
    }

    public void SetItemPrefab(GameObject prefab)
    {
        this._itemPrefab = prefab;
    }

    public void SetImageCheck(Sprite sprite)
    {
        _imageCheck.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void OnClickCell()
    {
       
        if (!_selected)
        {
            if (_playerInventory.SelectedItems.Count >= 3)
                return;  
            OnCellSelected.Invoke(_item);
            _selected = true;
            _imageCheck.SetActive(true);
            _button.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);

        }
        else
        {
            OnCellUnselected.Invoke(_item);
            _selected = false;
            _imageCheck.SetActive(false);
            _button.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        _soundManager.PlaySFX("ProductSelect");
    }

   
}
