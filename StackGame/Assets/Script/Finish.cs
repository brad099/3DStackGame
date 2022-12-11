using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using DG.Tweening;

public class Finish : MonoBehaviour
{
    public List<Transform> skatePlaces;
    EventHolder _skates;
    public static int count = 0;

    public void SkatePlacement(Transform skate)
    {
        if (skate.CompareTag("Player"))
        {
            skate.GetComponent<PlayerController>().SetSpeedZero();
        }
        if (count == 4 && !skate.CompareTag("Player")) 
        {
            skate.tag = "Untagged";
            skate.DORotate(new Vector3(0, 0, -90), 2f);
            skate.parent = skatePlaces[count].transform;
            skate.DOLocalMove(Vector3.zero, 1f);
            
            skate.GetComponent<Rigidbody>().isKinematic = true;
            CollectedCoffeeData.Instance.CoffeeList.Remove(skate);
        }else if(!skate.CompareTag("Player"))
        {
            skate.tag = "Untagged";
            skate.parent = skatePlaces[count].transform;
            skate.DOLocalMove(Vector3.zero, 1f);
            skate.GetComponent<Rigidbody>().isKinematic = true;
            CollectedCoffeeData.Instance.CoffeeList.Remove(skate);
            count++;
        }
        
    }
}
