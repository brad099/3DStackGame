using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TagPriceHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _tagPrice;
    private int _sum;

    private void OnEnable()
    {
        EventHolder.Instance.OnPriceChange += StartCalculation;
    }

    private void StartCalculation()
   {
        foreach (var skate in SkateHolder.Instance.skateList)
        {
            if(skate.TryGetComponent<PriceModel>(out var price))
            {
                _sum += price.Price;
            }
        }
        _tagPrice.text = _sum.ToString();
        _sum = 0;
    }

    public int GetMoney()
    {
        return int.Parse(_tagPrice.text);
    }
}
