using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectedCoffee : MonoBehaviour
{
    public PlayerController Parent;
    [SerializeField] GameObject Change;
    private Rigidbody rb;

    private void Start()
    {
        Change = this.gameObject.transform.GetChild(0).gameObject;
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Skate"))
        {
            other.tag = "Skated";
            CollectedCoffeeData.Instance.CoffeeList.Add(other.transform);
            other.gameObject.AddComponent<CollectedCoffee>();
            other.gameObject.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

            var seq = DOTween.Sequence();
            seq.Kill();
            seq = DOTween.Sequence();
            for (int i = CollectedCoffeeData.Instance.CoffeeList.Count - 1; i > 0; i--)
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
            // int index = CollectedCoffeeData.Instance.CoffeeList.FindIndex(x => x.transform == transform);
            // List<Transform> transforms = CollectedCoffeeData.Instance.CoffeeList.GetRange(index, CollectedCoffeeData.Instance.CoffeeListCount - index);
            // foreach (Transform item in CollectedCoffeeData.Instance.CoffeeList)
            // {
            //     item.tag = "Collectable";
            //     CollectedCoffeeData.Instance.CoffeeList.Remove(item);
            //     item.position += new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));
            // }

            //   transform.DOLocalJump(new Vector3(transform.position.x, transform.position.y, transform.position.z) * Random.Range(1, 2f)
            // , 1, 1, 1);
            // for (int i = CollectedCoffeeData.Instance.CoffeeList.Count; i > 0; i--)
            // {
            //     //CollectedCoffeeData.Instance.CoffeeList.RemoveAt(0);
            //     //int index = CollectedCoffeeData.Instance.CoffeeList.FindIndex(x => x.transform == transform);
            //     rb.isKinematic = true;
            //     CollectedCoffeeData.Instance.CoffeeList.Remove(transform);
            // }


        }

        if (other.CompareTag("Finish"))
        {
            Debug.Log("Ending");
        }

                ////Finish
        //     foreach (GameObject drops in CollectedItems)
        // {
        //     distance = new Vector3 (Random.Range(-1, 3), 0.5f, Random.Range(-1, 3));
        //     drops.transform.parent = null;
        //     var seq = DOTween.Sequence();
        //     drops.AddComponent<Rigidbody>();
        //     seq.Append(drops.transform.DOLocalJump((gameObject.transform.position + distance), 1.3f, 1, 0.5f)).Join(drops.transform.DOScale(1f, 0.2f));
        //      CollectedCoffeeData.Instance.CoffeeList.Remove[transform];
        // } 



    }

}
