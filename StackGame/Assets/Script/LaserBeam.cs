using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public GameObject HitEffect;
    public float HitOffset = 0;

    public float MaxLength;
    private LineRenderer Laser;

    public float MainTextureLength = 1f;
    public float NoiseTextureLength = 1f;
    private Vector4 Length = new Vector4(1,1,1,1);
    private bool UpdateSaver = false;
    [SerializeField] Transform FirePoint;
    private ParticleSystem[] Effects;
    private ParticleSystem[] Hit;

    void Start ()
    {
        Laser = GetComponent<LineRenderer>();
        Effects = GetComponentsInChildren<ParticleSystem>();
        Hit = HitEffect.GetComponentsInChildren<ParticleSystem>();
    }

    void Update()
    {

        Laser.material.SetTextureScale("_MainTex", new Vector2(Length[0], Length[1]));                    
        Laser.material.SetTextureScale("_Noise", new Vector2(Length[2], Length[3]));
        //To set LineRender position
        if (Laser != null && UpdateSaver == false)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, MaxLength);
                Laser.SetPosition(0, transform.position);
                Laser.SetPosition(1, FirePoint.position);
                //End laser position if collides with object
                HitEffect.transform.position = hit.point + hit.normal * HitOffset;
                //Hit effect zero rotation
                HitEffect.transform.rotation = Quaternion.identity;


                Length[0] = MainTextureLength * (Vector3.Distance(transform.position, hit.point));

        }  
    }

}
