using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WalletView : MonoBehaviour
{
    private Wallet _wallet;
    [SerializeField] private TMP_Text _moneyBalance;


    private void Start()
    {
        
        _wallet = FindObjectOfType<Wallet>();
        _wallet.OnMoneyChanged.AddListener(UpdateMoneyText);
        UpdateMoneyText();
    }

    public void UpdateMoneyText()
    {
        _moneyBalance.text = "$ " + _wallet.Money.ToString();
    }
}
