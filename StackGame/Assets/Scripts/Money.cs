using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Money : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cyber"))
        {
             other.transform.DOScale(2.5f, 0.3f).OnComplete(() =>{
             other.transform.DOScale(2.4f, 0.3f);});
        }
    }
}
