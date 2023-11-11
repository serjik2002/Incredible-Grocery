using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Wallet : MonoBehaviour
{
    private int _money = 0;
    private int _fixedValue = 10;
    private Customer _customer;
    private PlayerInventory _playerInventory;

    public int Money => _money;

    public UnityEvent OnMoneyChanged;

    private void Start()
    {
        _customer = FindObjectOfType<Customer>();
        _playerInventory = FindObjectOfType<PlayerInventory>();
    }

    public void AddMoney()
    {
        if(_customer.CorrectItemCount == _customer.OrderItems.Count)
        {
            _money += _fixedValue * _customer.CorrectItemCount * 2;
        }
        else
        {
            _money += _fixedValue * _customer.CorrectItemCount;
        }
        OnMoneyChanged.Invoke();
    }
}
