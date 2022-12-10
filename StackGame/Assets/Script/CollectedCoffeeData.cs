using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedCoffeeData : MonoBehaviour
{
    public static CollectedCoffeeData Instance;
    public List<Transform> CoffeeList;
    public int CoffeeListCount => CoffeeList.Count;
    private void Awake() 
    {
        if(Instance ==null)
            Instance = this;
    }
}