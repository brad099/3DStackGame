using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class CollectedCoffee : MonoBehaviour
{
    [SerializeField] GameObject Simple;
    [SerializeField] GameObject Spray;
    [SerializeField] GameObject Graffiti;
    [SerializeField] GameObject Neon;
    [SerializeField] GameObject Fly;
    public PlayerController Parent;
    private Rigidbody rb;
    EventHolder _eventHolder;
    Finish _finish;
    private void Start()
    {
        Simple = this.gameObject.transform.GetChild(0).transform.GetChild(0).gameObject;
        Spray = this.gameObject.transform.GetChild(0).transform.GetChild(1).gameObject;
        Graffiti = this.gameObject.transform.GetChild(0).transform.GetChild(2).gameObject;
        Neon = this.gameObject.transform.GetChild(0).transform.GetChild(3).gameObject;
        Fly = this.gameObject.transform.GetChild(0).transform.GetChild(4).gameObject;
        rb = GetComponent<Rigidbody>();
        _eventHolder = FindObjectOfType<EventHolder>();
        _eventHolder.OnFinishArrived += StartAnimation;
        _finish = FindObjectOfType<Finish>();
    }

    private void StartAnimation()
    {
        if (!transform.CompareTag("Player"))
        {
            _finish.SkatePlacement(gameObject.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Collect Skates
        if (other.transform.CompareTag("Skate"))
        {
            other.transform.tag = "Skated";
            CollectedCoffeeData.Instance.CoffeeList.Add(other.transform);
            other.gameObject.AddComponent<CollectedCoffee>();
            other.gameObject.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            other.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            var seq = DOTween.Sequence();
            seq.Kill();
            seq = DOTween.Sequence();
            for (int i = CollectedCoffeeData.Instance.CoffeeList.Count - 1; i > 0; i--)
            {
                seq.Join(CollectedCoffeeData.Instance.CoffeeList[i].DOScale(0.6f, 0.2f));
                seq.AppendInterval(0.05f);
                seq.Join(CollectedCoffeeData.Instance.CoffeeList[i].DOScale(0.5f, 0.2f));
            }
        }
        //Change to Spray
        if (other.transform.CompareTag("Spray"))
        {
            Simple.gameObject.SetActive(false);
            Spray.gameObject.SetActive(true);
            Graffiti.gameObject.SetActive(false);
            Neon.gameObject.SetActive(false);
            Fly.gameObject.SetActive(false);
        }

        //Change to Graffiti
        if (other.transform.CompareTag("Graffiti"))
        {
            Simple.gameObject.SetActive(false);
            Spray.gameObject.SetActive(false);
            Graffiti.gameObject.SetActive(true);
            Neon.gameObject.SetActive(false);
            Fly.gameObject.SetActive(false);
        }

        //Change to Neon
        if (other.transform.CompareTag("Neon"))
        {
            Simple.gameObject.SetActive(false);
            Spray.gameObject.SetActive(false);
            Graffiti.gameObject.SetActive(false);
            Neon.gameObject.SetActive(true);
            Fly.gameObject.SetActive(false);
        }

        //Change to Fly
        if (other.transform.CompareTag("Fly"))
        {
            Simple.gameObject.SetActive(false);
            Spray.gameObject.SetActive(false);
            Graffiti.gameObject.SetActive(false);
            Neon.gameObject.SetActive(false);
            Fly.gameObject.SetActive(true);
        }        
        //Ememy Line
        if (other.transform.CompareTag("Enemy"))
        {
            RemoveFromList(gameObject);
        }

        //Finish
        if (other.transform.CompareTag("Finish"))
        {
            Debug.Log("Ending");
        }
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