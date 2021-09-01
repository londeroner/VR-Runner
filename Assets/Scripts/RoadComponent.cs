using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadComponent : MonoBehaviour
{
    public List<RoadElement> RoadComponents;

    void Start()
    {
        int roadIndex = Math.GetElementIndexByWeight(RoadComponents.Select(p => p.Weight).ToList());

        RoadComponents[roadIndex].Road.SetActive(true);
    }
}
