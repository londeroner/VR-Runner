using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadComponent : MonoBehaviour
{
    public List<RoadElement> RoadComponents;

    void Start()
    {
        int[] weights = new int[RoadComponents.Count];
        for (int i = 0; i < RoadComponents.Count; i++)
        {
            weights[i] = RoadComponents[i].Weight;
        }

        int roadIndex = Math.GetElementIndexByWeight(weights);

        RoadComponents[roadIndex].Road.SetActive(true);
    }


    private void OnDestroy()
    {
        foreach (RoadElement element in RoadComponents)
        {
            Destroy(element.Road);
        }
    }
}
