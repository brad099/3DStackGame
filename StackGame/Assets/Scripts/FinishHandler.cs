using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;

public class FinishHandler : MonoBehaviour
{
    [SerializeField] private List<Transform> skatePlaces;
    [SerializeField] private float _finalMoneyAmount;
    [SerializeField] private GameObject _finalMoney;

    private int _placeCount = 0;

    void OnEnable()
    {
        EventHolder.Instance.OnFinishCollider += FinishAnimationHandler;
    }

    private void FinishAnimationHandler()
    {
        List<Transform> skateList = SkateHolder.Instance.skateList;
        CameraManager.Instance.OpenCamera("FinishCamera");
        int tempCount = skateList.Count;
        for (int i = 1; i < tempCount; i++)
        {
            Transform skate = skateList[i];
            SkateHolder.Instance.RemoveSkateFromList(skate);
            skate.GetComponent<Rigidbody>().isKinematic = true;
            tempCount--;
            i--;
            if (_placeCount == 4)
            {
                skate.parent = skatePlaces[_placeCount];
                skate.DOLocalRotate(new Vector3(90, 0, 0), 0.25f).SetLoops(4, LoopType.Incremental);
                skate.DOLocalJump(Vector3.zero, 0.05f, 1, .5f);
            }
            else
            {
                skate.parent = skatePlaces[_placeCount];
                skate.DOLocalRotate(new Vector3(0, 0, 0), 0.3f);
                skate.DOLocalJump(Vector3.zero, 0.05f, 1, .5f);
                _placeCount++;
            }
        }
        _finalMoney.transform.DOMoveY((float)(_finalMoney.transform.position.y + (_finalMoneyAmount * 0.02f)), 1f);
    }
}