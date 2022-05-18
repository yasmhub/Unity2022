using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    public static GameMain Game;
    public bool someBool = false;
    public bool otherBool;

    public InputMain Input;

    PlayerData[] playerData;
    int playerCount = 0;

    void Awake()
    {
        Game = this;
        Input = transform.GetComponentInChildren<InputMain>();
        playerData = new PlayerData[2];
    }

    void Update()
    {

    }

    public void AddPlayer(PlayerData PlayerData)
    {
        playerData[playerCount] = PlayerData;
        ++playerCount;
    }

    public bool DoSomething(bool Bool)
    {
        if (Bool) { return false; }
        else { return true; }
    }
}
