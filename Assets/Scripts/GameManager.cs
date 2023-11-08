using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /*[SerializeField] private PlayerInventory _shopManager;
    [SerializeField] private Customer _buyerController;
    private List<Item> _correctItem = new List<Item>();

    public List<Item> CorrectItem => _correctItem;

    private List<Item> _saveItems = new List<Item>();
    private List<Item> _selectedItems = new List<Item>();

    private void Start()
    {
        _saveItems = _buyerController.SaveItems;
        _selectedItems = _shopManager.SelectedItems;
        _shopManager.GetComponent<Button>().onClick.AddListener(NextBuyerState);
    }

    public void SequenceList()
    {
          
        for (int i = 0; i < _saveItems.Count; i++)
        {
            if(_selectedItems.Contains(_saveItems[i]))
            {
                _correctItem.Add(_saveItems[i]);
            }
        }
    }
    private void NextBuyerState()
    {
        _buyerController.SetState(BuyerState.ValidateGoods);
    }*/
}
