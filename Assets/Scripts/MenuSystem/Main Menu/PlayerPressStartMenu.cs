using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPressStartMenu : BaseMenu {

    new void Start() {
        // all BaseMenus reference a MenuController
        // baseMenu sets an initial focused Selectable
        base.Start();
    }

    public void StartButton() {

        // enabled the player data select menu
        menuController.EnableMenu(1);
        Destroy(gameObject);
    }
}
