using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadComponent : MonoBehaviour
{
    public List<RoadElement> RoadComponents;

    void Start()
    {
        if (RoadComponents != null && RoadComponents.Count > 0)
        {
            int roadIndex = Math.GetElementIndexByWeight(RoadComponents.Select(p => p.Weight).ToList());

            RoadComponents[roadIndex].Road.SetActive(true);

            if (RoadComponents[roadIndex].Rotateble)
            {
                RoadComponents[roadIndex].Road.transform.rotation = Quaternion.Euler(
                    RoadComponents[roadIndex].Road.transform.rotation.x,
                    Random.Range(0, 360),
                    RoadComponents[roadIndex].Road.transform.rotation.z);
            }
        }
    }
}
