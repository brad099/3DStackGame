using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoesRun : MonoBehaviour
{
    private Animator _anim;
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    void Animation()
    {
        if (Random.Range(1, 10) == 3)
        {
            _anim.SetTrigger("ShowRun");
        }
    }
}
