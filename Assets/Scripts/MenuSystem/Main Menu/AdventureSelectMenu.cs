using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This menu is unique to the single-player Adventure.
// It includes the same options the MultiplayerLobby quarter-menu does,
// but additionally constructs an initially-locked single player stage progression
public class AdventureSelectMenu : BaseMenu {

    // go to FrontMenu
    public void BackButton() {

        menuController.EnableMenu(0);
        Destroy(gameObject);
    }

    //start the selected stage on a countdown, if unlocked
    public void ReadyButton() {

    }

    // build the "garage" menu
    public void GarageButton() {

    }

    // input preferences, hud color etc
    public void PlayerOptions() {

    }
}
