using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ObstacleHandler : MonoBehaviour
{
    private void OnEnable()
    {
        EventHolder.Instance.OnObstacleCollided += SplitSkates;
    }
    private void SplitSkates(Transform skate)
    {
        int colliderSkate = SkateHolder.Instance.skateList.FindIndex(x => x == skate);
        if (colliderSkate != -1)
        {
            List<Transform> droppedSkates = SkateHolder.Instance.skateList.GetRange(colliderSkate,
                SkateHolder.Instance.SkateListCount - colliderSkate);

            foreach (var droppedSkate in droppedSkates)
            {
                droppedSkate.tag = "Collectable";
                droppedSkate.gameObject.layer = 6;
                SkateHolder.Instance.skateList.Remove(droppedSkate);
                Destroy(droppedSkate.GetComponent<Rigidbody>());
                Destroy(droppedSkate.GetComponent<CollectedSkate>());
                Vector3 currentPos = droppedSkate.transform.position;
                droppedSkate.transform.DOJump(new Vector3(currentPos.x + Random.Range(-1.5f, 1.5f), 0.1f, currentPos.z + Random.Range(3, 5)), 2f, 1, 0.5f);
                DamageNum.Instance.ShowNumberDecrease(droppedSkate);
            }
        }
    }
}