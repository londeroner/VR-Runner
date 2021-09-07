using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    private int _playerMoney;
    public int PlayerMoney
    {
        get => _playerMoney;
        set 
        {
            _playerMoney = value;
            instance.MoneyText.text = "Collected coins: " + _playerMoney;
        }
    }

    private float _metersCovered;
    public float MetersCovered
    {
        get => _metersCovered;
        set
        {
            _metersCovered = value;
            instance.MetersText.text = "Meters covered: " + _metersCovered.ToString("0") + "m";
        }
    }

    public Text MoneyText;
    public Text MetersText;

    private void Awake()
    {
        if (instance != null)
            throw new Exception();
        instance = this;

        PlayerMoney = 0;
        MoneyText.text = "Collected coins: " + PlayerMoney;
    }
}
