using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<Item> _allItems = new List<Item>();
    [SerializeField] private Transform _parent;
    [SerializeField] private GameObject _itemPrefab;
    
    private List<Item> _selectedItems = new List<Item>();
    private List<ShopCell> _allShopCells = new List<ShopCell>();
    private Animator _animator;


    public List<Item> AllItems => _allItems;
    public List<Item> SelectedItems => _selectedItems;

    public UnityEvent OnSelectedItemChanged;


    private void Start()
    {   
        _animator = GetComponent<Animator>();
        GameManager.Instance.OnLevelEnd.AddListener(Restart);
    }

    private void Restart()
    {
        _selectedItems.Clear();
        foreach (var item in _allShopCells)
        {
            item.Unselect();
        }
    }

    public void AddToInventory(Item item)
    {
        if(_selectedItems.Count < 3)
        {
            _selectedItems.Add(item);
            OnSelectedItemChanged.Invoke();
        }
        return;
    }

    public void RemoveFromInventory(Item item)
    {
        _selectedItems.Remove(item);
        OnSelectedItemChanged.Invoke();
    }


    public void FillInventory()
    {
        if (_allShopCells.Count == 0)
        {
            for (int i = 0; i < _allItems.Count; i++)
            {
                var item = Instantiate(_itemPrefab, _parent);
                item.GetComponent<Image>().sprite = _allItems[i].Sprite;

                ShopCell shopCell = item.GetComponent<ShopCell>();
                shopCell.SetItem(_allItems[i]);
                shopCell.SetItemPrefab(item);
                _allShopCells.Add(shopCell);
                shopCell.OnCellSelected.AddListener(AddToInventory);
                shopCell.OnCellUnselected.AddListener(RemoveFromInventory);
            }
        }
       
    }

    public void ActivateShop()
    {
        FillInventory();
        gameObject.SetActive(true);
        _animator.SetTrigger("ActivateShop");
    }

}
