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
    private List<bool> _isCorrect = new List<bool>();
    private List<ShopCell> _allShopCells = new List<ShopCell>();
    private Animator _animator;

    public List<Item> AllItems => _allItems;
    public List<Item > SelectedItems => _selectedItems;

    public UnityEvent OnSelectedItemChanged;


    private void Start()
    {
        
        _animator = GetComponent<Animator>();
        //OnSelectedItemChanged.AddListener();

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

    public void CheckOrder(Customer customer)
    {
        foreach (var item in _selectedItems)
        {
            if (customer.OrderItems.Contains(item))
            {
                _isCorrect.Add(true);
            }
            else
            {
                _isCorrect.Add(false);
            }
        }
        
    }

    public void FillInventory()
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

    public void ActivateShop()
    {
        FillInventory();
        gameObject.SetActive(true);
        _animator.SetTrigger("ActivateShop");
    }


    /*[SerializeField] private Transform _parent;
    [SerializeField] private ShopCell _itemView;
    [SerializeField] private List<Item> _allItems = new List<Item>();
    [SerializeField] private List<ShopCell> _allShopCell = new List<ShopCell>();
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private Customer _buyerConroller;
    [SerializeField] private RectTransform _targetShopPosition;
    [SerializeField] private float _speed; 


    private Animator _animator;
    private List<Item> _selectedItems = new List<Item>();
    private Transform _startPosition;

    public List<Item> AllItems => _allItems;
    public List<Item> SelectedItems => _selectedItems;

    private void Start()
    {
        //_startPosition.position = transform.position;
        FillShop();
        gameObject.SetActive(false);
        _buyerConroller.OnReadyBuyed.AddListener(ActivateShop);
        _animator = GetComponent<Animator>();
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
        _selectedItems.Add(item);
    }

    public void Unselect(Item item)
    {
        _selectedItems.Remove(item);
    }

    public void ActivateShop()
    {
        gameObject.SetActive(true);
        _animator.SetTrigger("ActivateShop");
    }*/

}
