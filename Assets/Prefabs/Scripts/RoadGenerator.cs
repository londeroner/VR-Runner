using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public GameObject RoadPrefab;
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
        if (currElem.transform.position.z < -15)
        {
            Destroy(currElem);
            roadElements.RemoveFirst();

            CreateNextRoad();
        }

        speed += speed * speedMultiplayer * Time.deltaTime;
        Debug.Log(speed);
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

        if (roadElements.Count > 0)
        {
            GameObject lastElement = roadElements.Last.Value;
            pos = lastElement.transform.position + new Vector3(0, 0, RoadPrefab.GetComponent<BoxCollider>().size.z * RoadPrefab.transform.localScale.z);
        }
        GameObject go = Instantiate(RoadPrefab, pos, Quaternion.identity);
        go.transform.SetParent(transform);
        roadElements.AddLast(go);
    }
}
