using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public List<RoadElement> RoadPrefabs;
    private LinkedList<GameObject> roadElements = new LinkedList<GameObject>();

    public float distanceThreshold = 15;
    public float startSpeed = 1f;
    public float speedStep = 0.001f;
    private float currentSpeed = 0;
    private uint fixedUpdateCounter = 0;

    public int maxRoadCount = 100;

    void Start()
    {
        ResetLevel();
        StartLevel();
    }

    void FixedUpdate()
    {
        if (currentSpeed == 0) return;

        
        foreach (var element in roadElements)
        {
            element.transform.position -= new Vector3(0, 0, currentSpeed * Time.fixedDeltaTime);
        }

        GameObject currElem = roadElements.First.Value;
        if (currElem.transform.position.z < -distanceThreshold)
        {
            Destroy(currElem);
            roadElements.RemoveFirst();

            CreateNextRoad();
        }

        currentSpeed += speedStep;

        fixedUpdateCounter++;
    }

    void StartLevel()
    {
        currentSpeed = startSpeed;
    }

    void ResetLevel()
    {
        currentSpeed = 0;

        while (roadElements.Count > 0)
        {
            Destroy(roadElements.First.Value);
            roadElements.RemoveFirst();
        }

        for (int i = 0; i < maxRoadCount; i++)
        {
            CreateNextRoad();
        }
        Debug.Log("RoadCount: " + roadElements.Count);
    }

    private void CreateNextRoad()
    {
        Vector3 pos = Vector3.zero;

        int[] weights = new int[RoadPrefabs.Count];
        for (int i = 0; i < RoadPrefabs.Count; i++)
        {
            weights[i] = RoadPrefabs[i].Weight;
        }
        int roadIndex = Math.GetElementIndexByWeight(weights);


        GameObject go = null;
        if (roadElements.Count > 0)
        {
            foreach (Transform child in roadElements.Last.Value.transform)
            {
                if (child.name == "EndPoint")
                {
                    foreach (Transform child2 in child)
                    {
                        if (child2.name == "ConnectionPoint")
                        {
                            //Debug.Log(child2.position);
                            go = Instantiate(
                                RoadPrefabs[roadIndex].Road,
                                child2.position,
                                Quaternion.identity);
                        }
                    }
                }
            }
        }
        else
        {
            go = Instantiate(
                RoadPrefabs[roadIndex].Road,
                new Vector3(0, 0, 0),
                Quaternion.identity);
        }

        if (go != null)
        {
            go.transform.SetParent(transform);
            roadElements.AddLast(go);
        }
    }
}