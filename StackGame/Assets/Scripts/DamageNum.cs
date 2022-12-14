using UnityEngine;
using DamageNumbersPro;

public class DamageNum : MonoBehaviour {
    public static DamageNum Instance;
    [SerializeField] public DamageNumber numberPrefab;
    // [SerializeField] DamageNumber damageNumber;

    private void Start() {
        if(Instance == null)
            Instance=this;
    }

    public void SpawnNum(int num, Transform _transform)
    {
        DamageNumber damageNumber = numberPrefab.Spawn(_transform.position, num);
    }
}