using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] _itemsInCloud;
    [SerializeField] private ShopManager _shopManager;
    [SerializeField] private List<Item> _allItems = new List<Item>();
    private List<Item> _saveItems = new List<Item>();


    private void Start()
    {
        ChoseItem();
        DisplayItems();
    }

    public void ChoseItem()
    {
        do
        {
            var item = _allItems[(int)Random.Range(0, _allItems.Count)];
            if (!_saveItems.Contains(item))
                _saveItems.Add(item);
        } while (_saveItems.Count < 3);
    }

    public void DisplayItems()
    {
        for (int i = 0; i < _itemsInCloud.Length; i++)
        {
            _itemsInCloud[i].GetComponent<SpriteRenderer>().sprite = _saveItems[i].Sprite;
        }
    }
}
