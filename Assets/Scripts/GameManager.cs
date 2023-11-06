using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ShopManager _shopManager;
    [SerializeField] private BuyerConroller _buyerController;

    private List<Item> _saveItems = new List<Item>();
    private List<Item> _selectedItems = new List<Item>();

    private void Start()
    {
        _saveItems = _buyerController.SaveItems;
        _selectedItems = _shopManager.SelectedItems;
    }

    public void SequenceList()
    {
        for (int i = 0; i < _selectedItems.Count; i++)
        {

        }
    }
}
