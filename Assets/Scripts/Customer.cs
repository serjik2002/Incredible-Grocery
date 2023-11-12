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

//here it was possible to write a state machine, but I have not yet fully understood how it works

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
    private bool _waitOrderInvoked = false;
    private Wallet _wallet;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;


    public UnityEvent OnWaitOrder;
    public UnityEvent OnPlaceOrder;


    public List<Item> OrderItems => _orderItems;
    public CustomerState CurrentState => _currentState;
    public bool IsSatisfied => _isSatisfied;
    public int CorrectItemCount => _correctItemCount;

    private void Start()
    {
        GameManager.Instance.OnLevelEnd.AddListener(Restart);
        _spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        _currentState = CustomerState.WalkToCashier;
        _wallet = FindObjectOfType<Wallet>();
        _animator = GetComponent<Animator>();
        SoundManager.Instance.PlayMusic("BackgroundMusic");
        _animator = GetComponentInChildren<Animator>();
        _correctItemCount = 0;
    }

    private void Update()
    {
        HandlesState();
    }

    private void Restart()
    {
        StartCoroutine(RestartHelp());
    }

    private IEnumerator RestartHelp()
    {
        yield return new WaitForSeconds(1);
        _spriteRenderer.enabled = true;
        _correctItemCount = 0;
        _orderItems.Clear();
        _isSatisfied = false;
        _orderCloud.SetActive(false);
        _waitOrderInvoked = false;
        transform.localRotation = new Quaternion(0, 0, 0, 0);
        _currentState = CustomerState.WalkToCashier;
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
        if (Vector2.Distance(transform.position, _exitPosition.position) < 0.1)
        {
            _spriteRenderer.enabled = false;
            GameManager.Instance.OnLevelEnd.Invoke();
        }
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
                
                _animator.SetTrigger("Walk");
                WalkToCashier();               
                break;
            case CustomerState.PlaceOrder:
                _animator.ResetTrigger("Walk");
                _animator.SetTrigger("Stay");
                PlaceOrder(_playerInventory);
                OnPlaceOrder.Invoke();
                _currentState = CustomerState.WaitOrder;
                break;
            case CustomerState.WaitOrder:
                if(!_waitOrderInvoked)
                {
                    OnWaitOrder.Invoke();
                    _waitOrderInvoked = true;
                }
                break;
            case CustomerState.WalkToExit:
                _animator.SetTrigger("Walk");
                transform.localRotation = new Quaternion(0, 180, 0, 0);
                WalkToExit();
                break;
            default:
                GameManager.Instance.OnLevelEnd.Invoke();
                break;
        }
    }
    

}
