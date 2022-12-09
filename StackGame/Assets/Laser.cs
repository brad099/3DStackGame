using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] LineRenderer liner;
    [SerializeField] Transform FirePoint;
    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(FirePoint.position, FirePoint.forward, out hit, 10f, layerMask);
        Debug.DrawRay(FirePoint.position, FirePoint.forward * 10f, Color.red);
        liner.SetPosition(0, FirePoint.position);
        liner.SetPosition(1, hit.point);
    }
}
