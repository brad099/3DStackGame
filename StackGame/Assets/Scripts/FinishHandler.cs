using System.Collections;
using DG.Tweening;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class FinishHandler : MonoBehaviour
{
    [SerializeField] private List<Transform> skatePlaces;
    [SerializeField] private GameObject _finalMoney;
    private int _placeCount = 0;
    private SkateCollector _skateCollector;

    void OnEnable()
    {
        EventHolder.Instance.OnFinishCollider += FinishAnimationHandler;
        _skateCollector = FindObjectOfType<SkateCollector>();
    }

    private void FinishAnimationHandler()
    {
        StartCoroutine(StartAnimation());
        _skateCollector.IsFinished=false;
        
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
            yield return new WaitForSeconds(0.3f);
        }
        _finalMoney.transform.DOMoveY((float)(_finalMoney.transform.position.y + (DamageNum.Instance.TotalMoney * 0.03f)), 1f);
    }
}