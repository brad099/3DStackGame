using System;
using UnityEngine;

public class EventHolder : MonoBehaviour
{
    private static EventHolder _instance;

    public static EventHolder Instance
    {
        get
        {
            _instance = FindObjectOfType<EventHolder>();
            return _instance;
        }
        set { _instance = value; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public Action OnFinishCollider;
    public Action<Transform> OnSkateCollided;
    public Action<Transform> OnObstacleCollided;
    public Action<Transform> OnSellSkate;

    public Action OnPriceChange;

    public void PriceChangeEvent()
    {
        OnPriceChange?.Invoke();
    }

    public void SkateCollided(Transform skate)
    {
        OnSkateCollided?.Invoke(skate);
    }

    public void ObstacleCollided(Transform skate)
    {
        OnObstacleCollided?.Invoke(skate);
    }

    public void SellSkateCollider(Transform skate)
    {
        OnSellSkate?.Invoke(skate);
    }

    public void FinishCollider()
    {
        OnFinishCollider?.Invoke();
    }
}