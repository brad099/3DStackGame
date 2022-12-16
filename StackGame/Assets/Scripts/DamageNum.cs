using UnityEngine;
using DamageNumbersPro;
using TMPro;

public class DamageNum : MonoBehaviour
{
    public static DamageNum Instance;
    [SerializeField] public DamageNumber numberPrefab;
    [SerializeField] public DamageNumber numberPrefabDecrease;
    [SerializeField] public TMP_Text _money;
    public float _newnumberMultipl;
    private float _totalMoney = 0;
    public float TotalMoney => _totalMoney;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
    }


    public void ShowNumber(float numberMultpl, Transform _transform)
    {

        //float NewnumberMultipl = numberMultpl;
        //_newnumberMultipl = NewnumberMultipl;
        //_totalMoney += NewnumberMultipl;
        DamageNumber damageNumber = numberPrefab.Spawn(_transform.position, _transform.GetComponent<PriceModel>().Price);
        //_money.text = System.Convert.ToInt32(_totalMoney).ToString();
    }

    public void ShowNumberDecrease(Transform _transform)
    {
        //float NewnumberMultipl = _transform.GetComponent<CollectedSkate>()._SkatePrice;
        //_totalMoney -= NewnumberMultipl;
        DamageNumber damageNumber = numberPrefabDecrease.Spawn(_transform.position, _transform.GetComponent<PriceModel>().Price);
        //if (_totalMoney < 0)
        //    _totalMoney = 0;
        //else
        //    _money.text = System.Convert.ToInt32(_totalMoney).ToString();

    }
}