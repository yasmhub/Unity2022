using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using System.Linq;

// list 2 input listeners
// 
public class InputMain : MonoBehaviour
{
    // flyweight: these ID's will be the same for each input listener
    public int MoveVertical = 0;
    public int MoveHorizontal = 1;
    public int LookVertical = 2;
    public int LookHorizontal = 3;
    public int Jump = 4;
    public int Back = 5;
    // one for each player
    public InputListener[] listeners;

    private void Awake()
    {
        // create a listener for each input channel
        listeners = new InputListener[3];
        for (int i = 0; i < 3; ++i)
        {
            listeners[i] = new InputListener(this, i);
        }
    }

    private void Update()
    {
        for (int i = 0; i < 3; ++i)
        {
            listeners[i].UpdateInput();
        }
    }
}

public class InputListener
{
    InputMain inMain;
    Player player;
    List<InputReceiver> receivers;

    public float moveV;
    public float moveH;
    public float lookV;
    public float lookH;
    public bool jump;
    public bool back;

    public InputListener(InputMain InMain, int RewiredID)
    {
        inMain = InMain;
        player = ReInput.players.GetPlayer(RewiredID);
        receivers = new List<InputReceiver>();
    }

    public void AddReciever(InputReceiver InputReceiver)
    {
        receivers.Add(InputReceiver);
    }
    public void RemoveReceiver(InputReceiver InputReceiver)
    {
        receivers.Remove(InputReceiver);
    }

    public void UpdateInput()
    {
        moveH = player.GetAxis(inMain.MoveHorizontal);
        moveV = player.GetAxis(inMain.MoveVertical);
        lookH = player.GetAxis(inMain.LookHorizontal);
        lookV = player.GetAxis(inMain.LookVertical);
        jump = player.GetButtonDown(inMain.Jump);
        back = player.GetButtonDown(inMain.Back);

        // update all scripts using this input
        for (int i = receivers.Count - 1; i >= 0; --i)
        {
            receivers[i].InputUpdate(this);
        }
    }
}