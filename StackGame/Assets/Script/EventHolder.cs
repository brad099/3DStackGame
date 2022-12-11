using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHolder : MonoBehaviour
{
    public event Action<CollectedCoffee> OnSkateCollected;
    public event Action OnFinishArrived;

    public void FinishArrivedEvent()
    {
        OnFinishArrived?.Invoke();
    }
    public void SkateActive(CollectedCoffee collectedCoffee)
    {
        OnSkateCollected?.Invoke(collectedCoffee);
    }
}
