using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�������� ��������� ���� �� ������� ��������� ��������

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private ShopCell _itemView;
    [SerializeField] private List<Item> _allItems = new List<Item>();
    [SerializeField] private List<ShopCell> _allShopCell = new List<ShopCell>();
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private BuyerConroller _buyerConroller;
    [SerializeField] private RectTransform _targetShopPosition;
    [SerializeField] private float _speed; 

    private List<Item> _selectedItem = new List<Item>();
    private Transform _startPosition;

    public List<Item> AllItems => _allItems;

    private void Start()
    {
        _startPosition.position = transform.position;
        FillShop();
        gameObject.SetActive(false);
        _buyerConroller.OnReadyBuyed.AddListener(ActivateShop);
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

    public void ActivateShop()
    {
        gameObject.SetActive(true);
        transform.position = Vector2.MoveTowards(transform.position, _targetShopPosition.position, _speed * Time.deltaTime);
    }

}