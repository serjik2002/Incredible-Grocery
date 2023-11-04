using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum BuyerState
{
    WalkToCashier,
    ChoseGoods,
    WaitGoods,
    ValidateGoods,
    WalkToExit
}

//закончил написание кода на моменте активации магазина

public class BuyerConroller : MonoBehaviour
{
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private Transform _exitPosition;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _itemCloud;


    [SerializeField] GameObject[] _itemsInCloud;
    [SerializeField] private List<Item> _allItems = new List<Item>();

    [SerializeField] private float _timeToRemember = 5;

    private List<Item> _saveItems = new List<Item>();
    private BuyerState _currentState;

    public UnityEvent OnReadyBuyed;
    public UnityEvent OnReadyChosed;


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
                OnReadyBuyed.Invoke();
                break;
            case BuyerState.ValidateGoods:
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
        if(_timeToRemember > 0)
        {
            _timeToRemember -= Time.deltaTime;
        }
        else
        {
            SetState(BuyerState.WaitGoods);
            _itemCloud.SetActive(false);
        }
        
    }
}
