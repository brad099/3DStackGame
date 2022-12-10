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
        //Collect Skates
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
        //Change to Neon
        if (other.CompareTag("Change"))
        {
            Change.gameObject.SetActive(true);
        }


        //Ememy Line
        if (other.CompareTag("Enemy"))
        {
            RemoveFromList(gameObject);
        }



        //Finish
        if (other.CompareTag("Finish"))
        {
            Debug.Log("Ending");
        }
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



    ///Tural'S Pack\\\
    private void RemoveFromList(GameObject skate)
    {
        List<Transform> droppedSkate = new List<Transform>();
        int startIndex = CollectedCoffeeData.Instance.CoffeeList.IndexOf(skate.transform);
        if (startIndex <= -1) return;
        //startIndex += 1;
        int endIndex = CollectedCoffeeData.Instance.CoffeeList.Count - startIndex; // Slashed Index Removed - 1
        droppedSkate = CollectedCoffeeData.Instance.CoffeeList.GetRange(startIndex, endIndex);
        HandleDeletedSkates(droppedSkate);
        CollectedCoffeeData.Instance.CoffeeList.RemoveRange(startIndex, endIndex);
    }
    private void HandleDeletedSkates(List<Transform> droppedSushi)
    {
        var seq = DOTween.Sequence();
        foreach (var sushi in droppedSushi)
        {
            Destroy(sushi.GetComponent<Rigidbody>());
            Destroy(sushi.GetComponent<CollectedCoffee>());
            sushi.tag = "Skate";
            //sushi.GetComponent<CollectedSushi>().DeInitializeOnDropped();
            Vector3 currentPos = sushi.transform.position;
            sushi.transform.DOJump(new Vector3(currentPos.x + Random.Range(-1.5f, 1.5f), 0.2621811f, currentPos.z + Random.Range(3, 8)), 2f, 1, 0.5f);

        }
    }
}