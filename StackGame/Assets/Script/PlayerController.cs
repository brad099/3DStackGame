using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private float VerticalSpeed;
    [SerializeField]private float _speed = 10f;
    [SerializeField] private float LerpSpeed = 10f;
    [SerializeField] private float OffSet = 2f;
    private Transform _transform;
    private Rigidbody _physics;
    EventHolder _eventHolder;

    void Start()
    {
        _transform = GetComponent<Transform>();
        _physics= gameObject.GetComponent<Rigidbody>();
        _eventHolder = GetComponent<EventHolder>();
        CollectedCoffeeData.Instance.CoffeeList.Add(transform.GetChild(0));
    }

    void Update()
    {
        //Move With Clamp
        float movement = (_speed * Input.GetAxis("Horizontal")) * Time.deltaTime;
            _transform.Translate(1 * movement,0, Time.deltaTime * _speed);
            _transform.localPosition = new Vector3((Mathf.Clamp(transform.localPosition.x, -3, 2.3f)),transform.localPosition.y, transform.localPosition.z);
         if(CollectedCoffeeData.Instance.CoffeeList.Count > 1)
            CoffeeFollow();
    }

    private void OnTriggerEnter(Collider other) 
    {
        // Collecting Skates
        if(other.CompareTag("Skate"))
        {

            // Adding to List
            CollectedCoffeeData.Instance.CoffeeList.Add(other.transform);
            other.gameObject.AddComponent<CollectedCoffee>();
            other.tag = "Skated";
            other.gameObject.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;


            // Seq for Doing Bounce
            var seq = DOTween.Sequence();
            seq.Kill();
            seq = DOTween.Sequence();
            for (int i =  CollectedCoffeeData.Instance.CoffeeList.Count - 1; i > 0; i--)
            {
                seq.Join(CollectedCoffeeData.Instance.CoffeeList[i].DOScale(0.6f, 0.2f));
                seq.AppendInterval(0.05f);
                seq.Join(CollectedCoffeeData.Instance.CoffeeList[i].DOScale(0.5f, 0.2f));
            }
        }

        //// Finish Line
        if (other.CompareTag("Finish"))
        {
            _speed = 0;
            _eventHolder.FinishArrivedEvent();
        }
    }

    public void SetSpeedZero()
    {
        _speed = 0;
    }
    public void CoffeeFollow()
    {
        // Checking List for Distance
        for (int i = 1; i < CollectedCoffeeData.Instance.CoffeeList.Count; i++)
        {
        Vector3 PrevPos = CollectedCoffeeData.Instance.CoffeeList[i-1].position + Vector3.forward*OffSet;
        PrevPos.y = CollectedCoffeeData.Instance.CoffeeList[i].position.y;
        Vector3 CurrentPos = CollectedCoffeeData.Instance.CoffeeList[i].transform.position;
        CollectedCoffeeData.Instance.CoffeeList[i].transform.position = Vector3.Lerp(CurrentPos, PrevPos, LerpSpeed * Time.deltaTime);   
        }
    }
}