using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPressStartMenu : BaseMenu {

    new void Start()
    {
        base.Start();
    }

    public void StartButton()
    {
        // enabled player select menu
        menuController.EnableMenu(1);
        Destroy(gameObject);
    }
}
