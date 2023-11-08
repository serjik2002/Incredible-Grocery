using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Customer _customer;
    [SerializeField] private GameObject[] _orderItems;
    [SerializeField] private GameObject[] _selectedObject;
    [SerializeField] private GameObject _orderCloud;

    [SerializeField] private float _timeToDisableOrderCloud = 5;

    [SerializeField] private PlayerInventory _playerInventory;

    [SerializeField] private Button _sellButton;

    [SerializeField] private Sprite _correctItem;
    [SerializeField] private Sprite _incorrectItem;
    

    public Button SellButton => _sellButton;

    private void Start()
    {

        _sellButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        _sellButton.enabled = false;

        _customer.OnPlaceOrder.AddListener(EnableOrderCloud);
        _customer.OnWaitOrder.AddListener(DisableOrderCloud);
        _customer.OnWaitOrder.AddListener(EnableShop);
        _playerInventory.OnSelectedItemChanged.AddListener(SellButtonChangeState);


    }

    private void Update()
    {
        DisplayOrder(_customer);
    }

    public void DisplayOrder(Customer customer)
    {
        for (int i = 0; i < _customer.OrderItems.Count; i++)
        {
            _orderItems[i].SetActive(true);
            _orderItems[i].GetComponent<SpriteRenderer>().sprite = customer.OrderItems[i].Sprite;
        }
    }

    public void EnableOrderCloud()
    {
        _orderCloud.SetActive(true);
    }

    public void DisableOrderCloud()
    {
        StartCoroutine(DisableOrderCloudHelp());
    }

    private IEnumerator DisableOrderCloudHelp()
    {
        yield return new WaitForSeconds(_timeToDisableOrderCloud);
        _orderCloud.SetActive(false);
    }

    public void EnableShop()
    {
        StartCoroutine(EnableShopHelp());
    }
    private IEnumerator EnableShopHelp()
    {
        yield return new WaitForSeconds(_timeToDisableOrderCloud);
        _playerInventory.ActivateShop();
    }

    private void SellButtonChangeState()
    {
        if(_playerInventory.SelectedItems.Count < _customer.OrderItems.Count)
        {
            _sellButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            _sellButton.enabled = false;
        }
        else
        {
            _sellButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            _sellButton.enabled = true;
        }
    }
    public void DisplaySelectedItems()
    {
        for (int i = 0; i < _selectedObject.Length; i++)
        {
            _selectedObject[i].SetActive(true);
            _selectedObject[i].GetComponent<SpriteRenderer>().sprite = _playerInventory.SelectedItems[i].Sprite;
            _selectedObject[i].GetComponent<ShopCell>().SetItem(_playerInventory.SelectedItems[i]);
        }
    }

    public void ValidateOrder()
    {
        for (int i = 0; i < _selectedObject.Length; i++)
        {
            var item = _selectedObject[i].GetComponent<ShopCell>().Item;
            //_selectedObject[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
            if (_customer.OrderItems.Contains(item))
            {
                //сделать галочку в хмарке
                _selectedObject[i].GetComponent<ShopCell>().SetImageCheck(_correctItem);
            }
            else
            {
                _selectedObject[i].GetComponent<ShopCell>().SetImageCheck(_incorrectItem);
            }
        }
    }
}
