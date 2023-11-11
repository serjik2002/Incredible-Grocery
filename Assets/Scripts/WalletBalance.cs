using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wallet")]
public class WalletBalance : ScriptableObject
{
    [SerializeField]private int _balance;

    public int Balance => _balance;

    public void SetBalance(int balance)
    {
        _balance = balance;
    }

}
