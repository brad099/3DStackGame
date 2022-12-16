using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoesRun : MonoBehaviour
{
    private Animator _anim;
    private bool stop = false;
    void Start()
    {
        _anim = GetComponent<Animator>();
        StartCoroutine(Animation());
    }

    void Update()
    {

    }

    IEnumerator Animation()
    {
        while (true)
        {
            if (stop != true)
            {      
            Debug.Log("omaiwa");    
            _anim.SetTrigger("ShowRun");
            yield return new WaitForSeconds(Random.Range(3, 5));
            }
        }
    }

    public void Power()
    {
        gameObject.GetComponentInParent<PlayerMovement>().verticalSpeed = 7;
    }
    public void PowerDown()
    {
        gameObject.GetComponentInParent<PlayerMovement>().verticalSpeed = 4;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Fly"))
        {
            stop = true;
        }
    }
}
