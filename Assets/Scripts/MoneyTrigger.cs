using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyTrigger : MonoBehaviour
{
    public AudioSource audio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerStats.instance.PlayerMoney++;
            audio.Play();
        }
    }
}
