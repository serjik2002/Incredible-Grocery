using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private ShopCell _itemView;
    [SerializeField] private List<Item> _allItems = new List<Item>();
    [SerializeField] private List<ShopCell> _allShopCell = new List<ShopCell>();
    [SerializeField] private GameObject _itemPrefab;

    private List<Item> _selectedItem = new List<Item>();

    private void Start()
    {
        FillShop();
    }

    public void FillShop()
    {
        if (_itemPrefab == null)
        {
            Debug.LogError("Item prefab is not assigned.");
            return;
        }

        for (int i = 0; i < _allItems.Count; i++)
        {
            var item = Instantiate(_itemPrefab, _parent);
            item.GetComponent<Image>().sprite = _allItems[i].Sprite;

            ShopCell shopCell = item.GetComponent<ShopCell>();
            shopCell.SetItem(_allItems[i]);
            shopCell.SetItemPrefab(item);
            _allShopCell.Add(shopCell);
            shopCell.OnCellSelected.AddListener(Select);
            shopCell.OnCellUnselected.AddListener(Unselect);
        }
    }

    public void Select(Item item)
    {
        _selectedItem.Add(item);
    }

    public void Unselect(Item item)
    {
        _selectedItem.Remove(item);
    }

}
