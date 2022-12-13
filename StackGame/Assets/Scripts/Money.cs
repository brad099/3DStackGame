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
             other.transform.DOScale(26f, 0.3f).OnComplete(() =>{
             other.transform.DOScale(25f, 0.3f);});
        }
    }
}
