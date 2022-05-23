using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputListenerTest : MonoBehaviour, InputReceiver
{
    public int InputChannel = 1;

    void Start()
    {
        GameMain.Instance.Input.listeners[InputChannel].AddReciever(this);
    }

    // Update is called once per frame
    public void InputUpdate(InputListener InputListener)
    {
        Debug.Log("LookH: " + InputListener.lookH + " | LookV: " + InputListener.lookV);
        Debug.Log("moveH: " + InputListener.moveH + " | MoveV: " + InputListener.moveV);
        Debug.Log("jump: " + InputListener.jump);
    }

    public void OnDestroy()
    {
        GameMain.Instance.Input.listeners[InputChannel].RemoveReceiver(this);
    }
}
