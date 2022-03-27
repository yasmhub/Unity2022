using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// toolbox singleton, reference scripts found attached
public class GameMain : MonoBehaviour
{
    public static GameMain Game;
    public bool someBool = false;
    public bool otherBool;

    public InputMain Input;

    void Awake()
    {
        Game = this;
        Input = transform.GetComponentInChildren<InputMain>();
    }

    void Update()
    {

    }

    public bool DoSomething(bool Bool)
    {
        if (Bool) { return false; }
        else { return true; }
    }
}
