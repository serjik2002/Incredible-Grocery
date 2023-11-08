using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum CustomerState
{
    WalkToCashier,
    PlaceOrder,
    WaitOrder,
    ValidateGoods,
    WalkToExit
}


public class Customer : MonoBehaviour
{
    [SerializeField] private int _minOrderCount;
    [SerializeField] private int _maxOrderCount;
    [SerializeField] private PlayerInventory _playerInventory;

    [SerializeField] private float _speed;
    [SerializeField] private Transform _targetPosition;

    [SerializeField] private GameObject _orderCloud;

    private List<Item> _orderItems = new List<Item>();
    private bool _isSatisfied;
    private CustomerState _currentState;

    public List<Item> OrderItems => _orderItems;
   

    public UnityEvent OnWaitOrder;
    public UnityEvent OnPlaceOrder;
    
    
    public CustomerState CurrentState => _currentState;

    private void Start()
    {
        _currentState = CustomerState.WalkToCashier;
    }

    private void Update()
    {
        HandlesState();
    }

    private void WalkToCashier()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetPosition.position, Time.deltaTime * _speed);
        if (Vector2.Distance(transform.position, _targetPosition.position) < 0.1)
        {
            _currentState = CustomerState.PlaceOrder;
        }
    }

    public void PlaceOrder(PlayerInventory playerInventory)
    {
        int countOrder = Random.Range(_minOrderCount, _maxOrderCount);
        Debug.Log(countOrder);
        var allItems = playerInventory.AllItems;

        while (_orderItems.Count < countOrder)
        {
            var item = allItems[(int)Random.Range(0, allItems.Count)];
            if (!_orderItems.Contains(item))
                _orderItems.Add(item);
        }
        _currentState = CustomerState.ValidateGoods;
    }
    

    public void ValidateGoods()
    {
       // var correctList = _playerInventory.CheckOrder(this);

    }


    private void HandlesState()
    {
        switch (_currentState)
        {
            case CustomerState.WalkToCashier:
                WalkToCashier();
                break;
            case CustomerState.PlaceOrder:
                OnPlaceOrder.Invoke();
                PlaceOrder(_playerInventory);
                OnWaitOrder.Invoke();
                break;
            case CustomerState.WaitOrder:
                
                break;
            case CustomerState.ValidateGoods:
                ValidateGoods();
                break;
            case CustomerState.WalkToExit:
                break;
            default:
                break;
        }
    }



   /* [SerializeField] private Transform _targetPosition;
    [SerializeField] private Transform _exitPosition;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _itemCloud;

    [SerializeField] private GameManager _gameManager;

    [SerializeField] GameObject[] _itemsInCloud;
    [SerializeField] private List<Item> _allItems = new List<Item>();

    [SerializeField] private float _timeToRemember = 5;

    private List<Item> _correctItems;
    private List<Item> _saveItems = new List<Item>();
    private BuyerState _currentState;


    public UnityEvent OnReadyBuyed;
    public UnityEvent OnReadyChosed;

    public List<Item> SaveItems => _saveItems;



    private void Start()
    {
        OnReadyChosed.AddListener(ChoseGoods);
        OnReadyChosed.AddListener(DisplayItems);
    }

    void Update()
    {
        HandleState();
    }

    public void ItemCloudDisable()
    {
        _itemCloud.SetActive(false);
    }

    public void ChoseGoods()
    {

        while (_saveItems.Count < 3)
        {
            var item = _allItems[(int)Random.Range(0, _allItems.Count)];
            if (!_saveItems.Contains(item))
                _saveItems.Add(item);
        } 

        Debug.Log("ChoseGoods");
    }

    public void DisplayItems()
    {
        for (int i = 0; i < _itemsInCloud.Length; i++)
        {
            _itemsInCloud[i].GetComponent<ShopCell>().SetItem(_saveItems[i]);
            _itemsInCloud[i].GetComponent<SpriteRenderer>().sprite = _saveItems[i].Sprite;
        }
    }

    private void HandleState()
    {
        switch (_currentState)
        {
            case BuyerState.WalkToCashier:
                WalkToCashier();
                break;
            case BuyerState.ChoseGoods:
                OnReadyChosed.Invoke();
                SubstractTime();
                break;
            case BuyerState.WaitGoods:
                WaitGoods();
                break;
            case BuyerState.ValidateGoods:
                ValidateGoods();
                break;
            case BuyerState.WalkToExit:
                break;
            default:
                break;
        }
    }

    public void SetState(BuyerState state)
    {
        _currentState = state;
    }

    public void WaitGoods()
    {
        OnReadyBuyed.Invoke();
    }

    private void WalkToCashier()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetPosition.position, Time.deltaTime * _speed);
        if (Vector2.Distance(transform.position, _targetPosition.position) < 0.1)
        {
            _itemCloud.SetActive(true);
            SetState(BuyerState.ChoseGoods);
        }

    }

    private void SubstractTime()
    {
        if (_timeToRemember > 0)
        {
            _timeToRemember -= Time.deltaTime;
        }
        else
        {
            SetState(BuyerState.WaitGoods);
            _itemCloud.SetActive(false);
        }

    }

    private void ValidateGoods()
    {
        _correctItems = _gameManager.CorrectItem;

        for (int i = 0; i < _itemsInCloud.Length; i++)
        {
            if (_correctItems.Contains(_itemsInCloud[i].GetComponent<ShopCell>().Item))
            {
                
            }
            else
            {
                // Обработка неправильного предмета
            }
        }
    }*/
}
