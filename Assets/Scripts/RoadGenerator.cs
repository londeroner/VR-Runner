using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public List<RoadElement> RoadPrefabs;
    private LinkedList<GameObject> roadElements = new LinkedList<GameObject>();

    public float startSpeed = 1f;
    public float speedMultiplayer = 0.0000001f;
    private float speed = 0;

    public int maxRoadCount = 100;

    void Start()
    {
        ResetLevel();
        StartLevel();
    }

    void FixedUpdate()
    {
        if (speed == 0) return;

        foreach (var element in roadElements)
        {
            element.transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        }

        GameObject currElem = roadElements.First.Value;

        int counter = 0;
        foreach(var roadelement in roadElements)
        {
            if (roadelement.transform.position.z < - 15)
            {
                counter++;
            }
        }

        for (int i = 0; i < counter; i++)
        {
            Destroy(roadElements.First.Value);
            roadElements.RemoveFirst();

            CreateNextRoad();
        }

        speed += speed * speedMultiplayer * Time.deltaTime;
    }

    void StartLevel()
    {
        speed = startSpeed;
    }

    void ResetLevel()
    {
        speed = 0;

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

        int roadIndex = Math.GetElementIndexByWeight(RoadPrefabs.Select(p => p.Weight).ToList());

        GameObject go = new GameObject();

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
                new Vector3(0,0,0),
                Quaternion.identity);
        }
        go.name = "roadelement";
        go.transform.SetParent(transform);
        roadElements.AddLast(go);
    }
}