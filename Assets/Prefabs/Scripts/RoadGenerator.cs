using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public GameObject RoadPrefab;
    private List<GameObject> roadElements = new List<GameObject>();

    public float startSpeed = 10f;
    public float speedMultiplayer = 0.0000001f;
    private float speed = 0;

    public int maxRoadCount = 5;

    void Start()
    {
        ResetLevel();
        StartLevel();
    }

    void Update()
    {
        if (speed == 0) return;   

        foreach (var element in roadElements)
        {
            element.transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        }

        if (roadElements[0].transform.position.z < -15)
        {
            Destroy(roadElements[0]);
            roadElements.RemoveAt(0);

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
            Destroy(roadElements[0]);
            roadElements.RemoveAt(0);
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
            pos = roadElements[roadElements.Count - 1].transform.position 
                + new Vector3(0,0, RoadPrefab.GetComponent<BoxCollider>().size.z * RoadPrefab.transform.localScale.z);

        GameObject go = Instantiate(RoadPrefab, pos, Quaternion.identity);
        go.transform.SetParent(transform);
        roadElements.Add(go);
    }
}
