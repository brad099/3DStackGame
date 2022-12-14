using System.Collections;
using DG.Tweening;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class FinishHandler : MonoBehaviour
{
    [SerializeField] private List<Transform> skatePlaces;
    [SerializeField] private float _finalMoneyAmount;
    [SerializeField] private GameObject _finalMoney;
    private SkateCollector _skateCollector;
    private int _placeCount = 0;

    void OnEnable()
    {
        EventHolder.Instance.OnFinishCollider += FinishAnimationHandler;
        _skateCollector = FindObjectOfType<SkateCollector>();
    }

    private void FinishAnimationHandler()
    {
        StartCoroutine(StartAnimation());
        _skateCollector.IsFinished=false;
        _finalMoney.transform.DOMoveY((float)(_finalMoney.transform.position.y + (_finalMoneyAmount * 0.02f)), 1f);
    }

    IEnumerator StartAnimation()
    {
        List<Transform> skateList = SkateHolder.Instance.skateList;
        CameraManager.Instance.OpenCamera("FinishCamera");
        int tempCount = skateList.Count;
        for (int i = 1; i < tempCount; i++)
        {
            Transform skate = skateList[i];
            SkateHolder.Instance.RemoveSkateFromList(skate);
            skate.GetComponent<Rigidbody>().isKinematic = true;
            Destroy(skate.GetComponent<Animator>());
            Destroy(skate.GetComponent<CollectedSkate>());
            tempCount--;
            i--;
            if (_placeCount == 4)
            {
                skate.parent = skatePlaces[_placeCount];
                skate.DOLocalRotate(new Vector3(0, 0, 0), 0.25f);
                skate.DOLocalJump(Vector3.zero, 0.05f, 1, .2f);
            }
            else
            {
                skate.parent = skatePlaces[_placeCount];
                skate.DOLocalRotate(new Vector3(0, 0, 0), 0.3f);
                skate.DOLocalJump(Vector3.zero, 0.05f, 1, .2f);
                _placeCount++;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}