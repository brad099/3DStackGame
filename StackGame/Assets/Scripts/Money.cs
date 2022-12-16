using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Money : MonoBehaviour
{ 
    private Transform _exTransform;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cyber"))
        {
            Debug.Log("Omaaiwa");
            _exTransform = other.transform;
             other.transform.DOScale(30f, 0.3f).OnComplete(() =>{
             other.transform.DOScale(24f, 0.3f);
             Destroy(other.GetComponent<Collider>());
             });
            
             StartCoroutine(ReturnTransform(_exTransform,other.transform));
        }
    }

    private IEnumerator ReturnTransform(Transform exTransform, Transform other)
    {
        yield return new WaitForSeconds(0.3f);
        other = exTransform;
    }
}
