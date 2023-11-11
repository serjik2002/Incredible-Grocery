using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WalletView : MonoBehaviour
{
    private Wallet _wallet;
    private SoundManager _soundManager;
    [SerializeField] private TMP_Text _moneyBalance;


    private void Start()
    {
        
        _wallet = FindObjectOfType<Wallet>();
        _wallet.OnMoneyChanged.AddListener(UpdateMoneyText);
        _soundManager = FindObjectOfType<SoundManager>();
        _moneyBalance.text = "$ " + _wallet.Money;
    }

    public void UpdateMoneyText()
    {
        _moneyBalance.text = "$ " + _wallet.Money;
        _soundManager.PlaySFX("Money");
    }
}
