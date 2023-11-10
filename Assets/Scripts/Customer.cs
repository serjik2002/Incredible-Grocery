using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum CustomerState
{
    WalkToCashier,
    PlaceOrder,
    WaitOrder,
    WalkToExit
}


public class Customer : MonoBehaviour
{
    [SerializeField] private int _minOrderCount;
    [SerializeField] private int _maxOrderCount;
    [SerializeField] private PlayerInventory _playerInventory;

    [SerializeField] private float _speed;
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private Transform _exitPosition;
    [SerializeField] private Button _button;
 
    [SerializeField] private GameObject _orderCloud;

    private List<Item> _orderItems = new List<Item>();
    private bool _isSatisfied;
    private CustomerState _currentState;
    private int _correctItemCount = 0;
    private bool WaitOrderInvoked = false;
    private Wallet _wallet;


    public UnityEvent OnWaitOrder;
    public UnityEvent OnPlaceOrder;


    public List<Item> OrderItems => _orderItems;
    public CustomerState CurrentState => _currentState;
    public bool IsSatisfied => _isSatisfied;
    public int CorrectItemCount => _correctItemCount;

    private void Start()
    {
        _currentState = CustomerState.WalkToCashier;
        _wallet = FindObjectOfType<Wallet>();
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

    private void WalkToExit()
    {
        transform.position = Vector2.MoveTowards(transform.position, _exitPosition.position, Time.deltaTime * _speed);
    }

    public void PlaceOrder(PlayerInventory playerInventory)
    {
        int countOrder = Random.Range(_minOrderCount, _maxOrderCount);
        
        var allItems = playerInventory.AllItems;
        while (_orderItems.Count < countOrder)
        {
            var item = allItems[(int)Random.Range(0, allItems.Count)];
            if (!_orderItems.Contains(item))
                _orderItems.Add(item);
        }
        
    }

    public void SetStateWalkToExit()
    {
        _currentState = CustomerState.WalkToExit;
        _wallet.AddMoney();
    }
    
    public void SatisfiedUpdate()
    {
        
        for (int i = 0; i < _playerInventory.SelectedItems.Count; i++)
        {
            if(_orderItems.Contains(_playerInventory.SelectedItems[i]))
            {
                _correctItemCount++;
            }
        }
        if(_correctItemCount == _orderItems.Count)
        {
            _isSatisfied = true;
        }
        else
        {
            _isSatisfied = false;
        }
    }

    private void HandlesState()
    {
        switch (_currentState)
        {
            case CustomerState.WalkToCashier:
                WalkToCashier();               
                break;
            case CustomerState.PlaceOrder:
                PlaceOrder(_playerInventory);
                OnPlaceOrder.Invoke();
                _currentState = CustomerState.WaitOrder;
                break;
            case CustomerState.WaitOrder:
                if(!WaitOrderInvoked)
                {
                    OnWaitOrder.Invoke();
                    WaitOrderInvoked = true;
                }
                break;
            case CustomerState.WalkToExit:               
                WalkToExit();
                break;
            default:
                break;
        }
    }

}
