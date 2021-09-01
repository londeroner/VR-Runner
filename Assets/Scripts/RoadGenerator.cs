using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public List<RoadElement> RoadPrefabs;
    private LinkedList<GameObject> roadElements = new LinkedList<GameObject>();

    public float startSpeed = 10f;
    public float speedMultiplayer = 0.0000001f;
    private float speed = 0;

    public int maxRoadCount = 5;

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
            element.transform.position -= new Vector3(0, 0, speed * Time.fixedDeltaTime);
        }

        GameObject currElem = roadElements.First.Value;
        if (currElem.transform.position.z < -45)
        {
            Destroy(currElem);
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
    }

    private void CreateNextRoad()
    {
        Vector3 pos = Vector3.zero;

        int roadIndex = GetElementIndexByWeight(RoadPrefabs.Select(p => p.Weight).ToList());

        GameObject go = new GameObject();

        if (roadElements.Count > 0)
        {
            foreach (Transform child in roadElements.Last.Value.transform)
            {
                if (child.name == "EndPoint")
                {
                    Debug.Log(child.position);
                    foreach (Transform child2 in child)
                    {
                        if (child2.name == "ConnectionPoint")
                        {
                            Debug.Log(child2.position);
                            go = Instantiate(
                                RoadPrefabs[roadIndex].RoadPrefab,
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
                RoadPrefabs[roadIndex].RoadPrefab,
                new Vector3(0,0,0),
                Quaternion.identity);
        }

        go.transform.SetParent(transform);
        roadElements.AddLast(go);
    }

    private int GetElementIndexByWeight(List<int> weights)
    {
        int randomNumber = Random.Range(0, weights.Sum() + 1);

        int indexOf = -1;
        int currentWeight = 0;
        for (int i = 0; i < weights.Count; i++)
        {
            if (i == 0)
            {
                if (randomNumber >= 0 && randomNumber <= weights[0])
                {
                    indexOf = 0;
                    break;
                }
            }
            else
            {
                if (randomNumber > currentWeight && randomNumber <= currentWeight + weights[i])
                {
                    indexOf = i;
                    break;
                }
            }
            currentWeight += weights[i];
        }

        return indexOf;
    }
}

[System.Serializable]
public class RoadElement
{
    public GameObject RoadPrefab;

    public int Weight;
}