using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuyerConroller : MonoBehaviour
{
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private Transform _exitPosition;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _itemCloud;

    public UnityEvent OnReadyBuyed;


    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetPosition.position, Time.deltaTime * _speed);
        if (Vector2.Distance(transform.position, _targetPosition.position) < 0.1)
        {
            _itemCloud.SetActive(true);
            OnReadyBuyed.Invoke();
            Invoke("ItemCloudDisable", 5);
        }
    }

    public void ItemCloudDisable()
    {
        _itemCloud.SetActive(false);
    }
}
