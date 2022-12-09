using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectedCoffee : MonoBehaviour
{
    [SerializeField]GameObject Change;
    public PlayerController Parent;

     private void Start() 
     {
        Change = this.gameObject.transform.GetChild(0).gameObject;
     }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Skate"))
        {
            other.tag = "Skated";
            CollectedCoffeeData.Instance.CoffeeList.Add(other.transform);
            other.gameObject.AddComponent<CollectedCoffee>();
            other.gameObject.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

            var seq = DOTween.Sequence();
            seq.Kill();
            seq = DOTween.Sequence();
            for (int i =  CollectedCoffeeData.Instance.CoffeeList.Count - 1; i > 0; i--)
            {
                seq.Join(CollectedCoffeeData.Instance.CoffeeList[i].DOScale(1f, 0.2f));
                seq.AppendInterval(0.05f);
                seq.Join(CollectedCoffeeData.Instance.CoffeeList[i].DOScale(0.7f, 0.2f));
            }
        }

        if (other.CompareTag("Change"))
        {
            Debug.Log("We need Change things");
            Change.gameObject.SetActive(true);
        }

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Time to Die");
        }

    }

}
