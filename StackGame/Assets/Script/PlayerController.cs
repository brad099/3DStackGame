using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private float VerticalSpeed;
    [SerializeField]private float _speed = 10f;
    [SerializeField] private float LerpSpeed = 10f;
    [SerializeField] private float OffSet = 2f;
    private float Horizontal;
    private Transform _transform;

    

    void Start()
    {
        _transform = GetComponent<Transform>();
        CollectedCoffeeData.Instance.CoffeeList.Add(transform.GetChild(0));
    }

    void Update()
    {
        // Horizontal = Input.GetAxis("Horizontal");
        float movement = (_speed * Input.GetAxis("Horizontal")) * Time.deltaTime;
            _transform.Translate(1 * movement,0, Time.deltaTime * _speed);
            _transform.localPosition = new Vector3((Mathf.Clamp(transform.localPosition.x, -4.0f, 4.0f)),transform.localPosition.y, transform.localPosition.z);
        
         if(CollectedCoffeeData.Instance.CoffeeList.Count > 1)
            CoffeeFollow();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Skate"))
        {
            CollectedCoffeeData.Instance.CoffeeList.Add(other.transform);
            other.tag = "Skated";
            other.gameObject.AddComponent<CollectedCoffee>();
            other.gameObject.AddComponent<Rigidbody>().isKinematic = true;
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

        if (other.CompareTag("Finish"))
        {
           _speed = 0;
        }
    }

    public void CoffeeFollow()
    {
        // Checking List for Distance
        for (int i = 1; i < CollectedCoffeeData.Instance.CoffeeList.Count; i++)
        {
        Vector3 PrevPos = CollectedCoffeeData.Instance.CoffeeList[i-1].position + Vector3.forward*OffSet;
        Vector3 CurrentPos = CollectedCoffeeData.Instance.CoffeeList[i].transform.position;
        CollectedCoffeeData.Instance.CoffeeList[i].transform.position = Vector3.Lerp(CurrentPos, PrevPos, LerpSpeed * Time.deltaTime);   
        }
    }
}