using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Text _killCount;
    [SerializeField] private Text _moneyCount;
    
    public void ShowInfo()
    {        
        _killCount.text = (Int32.Parse(_killCount.text) + 1).ToString();
        _moneyCount.text = _player.Money.ToString();        
    }
}
