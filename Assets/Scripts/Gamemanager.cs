using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text winText;
    [SerializeField]
    Player player;

    void Awake()
    {
        winText.text = "";
        setScore();
    }

    public void setScore()
    {
        scoreText.text = "Coins: " + player.Coins;
        if(player.Coins >= 25)
        {
            winText.text = "¡You Win!";
        }
    }
}
