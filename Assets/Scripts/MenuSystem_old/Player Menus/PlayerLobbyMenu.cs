using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

// this menu allows players to select their starting ship and ready up
public class PlayerLobbyMenu : BaseMenu {
    /*
    [SerializeField] Text PlayerNameText;   // this text is set to the name in the player data file
    PlayerData playerData;                  // data for the player who is getting ready

    new void Start() {
        base.Start();

        playerData = MainState.Instance.PlayerData[menuController.MenuPosition];
        PlayerNameText.text = playerData.GetPlayerName;
    }

    public void BackButton() {

        // count the (in)active player for the ready-up countdown script
        MainState.Instance.MultiplayerMenuLobby.AddActivePlayers = -1;

        // ditch the player data that was loaded or created in this menu's position
        MainState.Instance.PlayerData[menuController.MenuPosition] = null;

        // go to the "PRESS START" menu 
        menuController.EnableMenu(0);

        // flag this Rewired player as not playing so it can be hooked again
        Player rewiredPlayer = ReInput.players.GetPlayer(menuController.RewiredID);
        rewiredPlayer.isPlaying = false;

        // reset the menu controller
        menuController.RewiredID = 0;
        menuController.enabled = false;

        // if this was the last player to leave, go back to using the Front Menu
        if (MainState.Instance.MultiplayerMenuLobby.GetActivePlayers == 0) {

            // disable the multiplayer menu manager, its multiplayer menu controllers, and the menus themselves
            MultiplayerMenuManager multiplayerManager = MainState.Instance.MultiplayerMenuManager;
            multiplayerManager.enabled = false;

            // re-enable the front menus
            MenuController frontMenuController = multiplayerManager.GetFrontMenuController;
            frontMenuController.enabled = true;
            frontMenuController.EnableMenu(0);
        }

        // finally, destroy this lobby menu
        Destroy(gameObject);
    }
    */
}