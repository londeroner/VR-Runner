using UnityEngine;

public class KeyBoardManipulation : MonoBehaviour
{
    [SerializeField]
    public float height = 0;

    [SerializeField]
    public float delta_x = 0.02f;

    [SerializeField]
    public GameObject camera_object;

    
    void Start()
    {
        Vector3 last = camera_object.transform.position;
        camera_object.transform.position = new Vector3(last.x, last.y + height, last.z);
    }


    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        //Debug.LogError(delta_x);

        if (Input.GetKey(KeyCode.A))
        {
            Vector3 last = this.transform.position;
            this.transform.position = new Vector3(last.x - delta_x, last.y, last.z);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Vector3 last = this.transform.position;
            this.transform.position = new Vector3(last.x + delta_x, last.y, last.z);
        }
    }
}
