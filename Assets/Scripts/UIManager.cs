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
    [SerializeField] private GameObject _selectedItemCloud;
    [SerializeField] private GameObject _emotion;

    [SerializeField] private float _timeToDisableOrderCloud = 5;

    [SerializeField] private PlayerInventory _playerInventory;

    [SerializeField] private Button _sellButton;

    [SerializeField] private Sprite _correctItem;
    [SerializeField] private Sprite _incorrectItem;

    [SerializeField] private Sprite _satisfiedEmotion;
    [SerializeField] private Sprite _dissatisfiedEmotion;
    private SoundManager _soundManager;

    public Button SellButton => _sellButton;

    private void Start()
    {

        _sellButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        _sellButton.enabled = false;
        _soundManager = FindObjectOfType<SoundManager>();
        AddListenerMethods();

    }

    private void AddListenerMethods()
    {
        _customer.OnPlaceOrder.AddListener(EnableOrderCloud);
        _customer.OnPlaceOrder.AddListener(DisplayOrder);
        _customer.OnWaitOrder.AddListener(DisableOrderCloud);
        _customer.OnWaitOrder.AddListener(EnableShop);
        _playerInventory.OnSelectedItemChanged.AddListener(SellButtonChangeState);
    }

    public void DisplayOrder()
    {
        for (int i = 0; i < _customer.OrderItems.Count; i++)
        {
            _orderItems[i].SetActive(true);
            _orderItems[i].GetComponent<SpriteRenderer>().sprite = _customer.OrderItems[i].Sprite;
        }
    }

    public void EnableOrderCloud()
    {
        _orderCloud.SetActive(true);
        _soundManager.PlaySFX("BubbleAppeared");
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
        if (_playerInventory.SelectedItems.Count < _customer.OrderItems.Count)
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


        for (int i = 0; i < _playerInventory.SelectedItems.Count; i++)
        {
            _selectedObject[i].SetActive(true);
            _selectedObject[i].GetComponent<SpriteRenderer>().sprite = _playerInventory.SelectedItems[i].Sprite;
            _selectedObject[i].GetComponent<ShopCell>().SetItem(_playerInventory.SelectedItems[i]);
        }
    }

    public void ValidateOrderDisplay()
    {
        StartCoroutine(ValidateOrderDisplayHelp());
    }

    private IEnumerator ValidateOrderDisplayHelp()
    {
        yield return new WaitForSeconds(1);
        _selectedItemCloud.SetActive(true);
        _soundManager.PlaySFX("BubbleAppeared");
        for (int i = 0; i < _playerInventory.SelectedItems.Count; i++)
        {
            yield return new WaitForSeconds(0.5f);
            var item = _selectedObject[i].GetComponent<ShopCell>().Item;
            _selectedObject[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
            if (_customer.OrderItems.Contains(item))
            {

                _selectedObject[i].GetComponent<ShopCell>().SetImageCheck(_correctItem);
            }
            else
            {
                _selectedObject[i].GetComponent<ShopCell>().SetImageCheck(_incorrectItem);
            }
        }
        yield return new WaitForSeconds(1);
        _selectedItemCloud.SetActive(false);
        _soundManager.PlaySFX("BubbleDisppeared");
        DisplaySatisfied();
        _customer.SetStateWalkToExit();

    }
    public void DisplaySatisfied()
    {
        foreach (var item in _orderItems)
        {
            item.SetActive(false);
        }
        EnableOrderCloud();
        _emotion.SetActive(true);
        var emotion = _emotion.GetComponent<SpriteRenderer>();

        if (_customer.IsSatisfied)
        {
            emotion.sprite = _satisfiedEmotion;
        }
        else
        {
            emotion.sprite = _dissatisfiedEmotion;
        }
    }

}


