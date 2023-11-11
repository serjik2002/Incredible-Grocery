using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Wallet : MonoBehaviour
{
    [SerializeField] WalletBalance _walletBalance;
    private int _money;
    private int _fixedValue = 10;
    private Customer _customer;
    

    public int Money => _money;

    public UnityEvent OnMoneyChanged;

    private void Awake()
    {
        _money = _walletBalance.Balance;
    }

    private void Start()
    {
        _customer = FindObjectOfType<Customer>(); 
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
        _walletBalance.SetBalance(_money);
        OnMoneyChanged.Invoke();
    }
}
