using System.Collections.Generic;
using UnityEngine;

public class SkateHolder : MonoBehaviour
{
    [SerializeField] public List<Transform> skateList;
    public int SkateListCount => skateList.Count;
    public static SkateHolder Instance { get; private set; }

    private void Awake()
    {
        Instance ??= this;
    }

    /// <summary>
    /// Dont use it in foreach
    /// </summary>
    /// <param name="removeTransform"></param>
    public void RemoveSkateFromList(Transform removeTransform)
    {
        if (skateList.FindIndex(x => x == removeTransform) != -1)
        {
            skateList.Remove(removeTransform);
        }
    }
}