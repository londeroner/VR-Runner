using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadComponent : MonoBehaviour
{
    public List<RoadDecorator> RoadComponents;

    void Start()
    {
        if (RoadComponents != null && RoadComponents.Count > 0)
        {
            int roadIndex = Math.GetElementIndexByWeight(RoadComponents.Select(p => p.Weight).ToList());

            RoadComponents[roadIndex].ContentObject.SetActive(true);

            if (RoadComponents[roadIndex].Rotateble)
            {
                RoadComponents[roadIndex].ContentObject.transform.rotation = Quaternion.Euler(
                    RoadComponents[roadIndex].ContentObject.transform.rotation.x,
                    Random.Range(0, 360),
                    RoadComponents[roadIndex].ContentObject.transform.rotation.z);
            }
        }
    }


    private void OnDestroy()
    {
        foreach (RoadDecorator element in RoadComponents)
        {
            Destroy(element.ContentObject);
        }
    }
}
