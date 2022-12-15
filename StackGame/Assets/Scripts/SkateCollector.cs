using DG.Tweening;
using UnityEngine;

public class SkateCollector : MonoBehaviour
{
    [SerializeField] private float lerpDuration;
    [SerializeField] private float stackOffset;
    private Sequence _sequence;
    public bool IsFinished = true;
    void OnEnable()
    {
        EventHolder.Instance.OnSkateCollided += CollectSkate;
    }

    private void FixedUpdate()
    {
        if(IsFinished)
            StackFollow();
    }

    private void CollectSkate(Transform skate)
    {
        skate.tag = "Collected";
        SkateHolder.Instance.skateList.Add(skate);
        skate.gameObject.AddComponent<CollectedSkate>();

        if (!skate.gameObject.TryGetComponent<Rigidbody>(out Rigidbody addedRigidbody))
        {
            addedRigidbody = skate.gameObject.AddComponent<Rigidbody>();
        }

        addedRigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;

        _sequence = DOTween.Sequence();
        _sequence.Kill();
        _sequence = DOTween.Sequence();
        for (int i = SkateHolder.Instance.SkateListCount - 1; i > 0; i--)
        {
            _sequence.Join(SkateHolder.Instance.skateList[i].DOScale(1.3f, 0.1f));
            _sequence.AppendInterval(0.05f);
            _sequence.Join(SkateHolder.Instance.skateList[i].DOScale(1f, 0.1f));
        }
    }

    private void StackFollow()
    {
        float lerpSpeed = lerpDuration;
        for (int i = 1; i < SkateHolder.Instance.SkateListCount; i++)
        {
            Vector3 previousPos = SkateHolder.Instance.skateList[i - 1].transform.position +
            Vector3.forward * stackOffset;
            Vector3 currentPos = SkateHolder.Instance.skateList[i].transform.position;
            SkateHolder.Instance.skateList[i].transform.position =
            Vector3.Lerp(currentPos, previousPos, lerpSpeed * Time.fixedDeltaTime);
            lerpSpeed += lerpDuration * Time.fixedDeltaTime;
        }
    }
}