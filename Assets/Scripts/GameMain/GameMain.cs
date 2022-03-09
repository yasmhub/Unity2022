using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    public bool someBool = false;
    public bool otherBool;

    void Start()
    {

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
