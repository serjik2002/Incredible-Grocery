using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopCell : MonoBehaviour
{
    [SerializeField] GameObject _imageCheck;
    private Item _item;
    private GameObject _itemPrefab;
    private Button _button;
    private bool _selected;
    

    public GameObject ItemPrefab => _itemPrefab;
    public Item Item => _item;
    public UnityEvent<Item> OnCellSelected;
    public UnityEvent<Item> OnCellUnselected;


    private void Start()
    {
        _button = GetComponent<Button>();
    }

    public void SetItem(Item item)
    {
        this._item = item;
    }

    public void SetItemPrefab(GameObject prefab)
    {
        this._itemPrefab = prefab;
    }

    public void OnClickCell()
    {
        if (!_selected)
        {
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
    }
}
