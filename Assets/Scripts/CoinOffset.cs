using UnityEngine;

public class CoinOffset : MonoBehaviour
{
    private void Awake()
    {
        float offsetX = Random.Range(0, 101);

        offsetX -= 50;
        offsetX /= 100;

        float offsetY = Random.Range(0, 76);

        offsetY -= 50;
        offsetY /= 100;

        transform.position = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY, transform.position.z);
    }
}
