#define JAY_DEBUG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

// instantiate four small menus for players
// listens for an initial input, then enables a menu for each player
public class MultiplayerMenuManager : MonoBehaviour {
    /*
    [Header("Player Menu Prefab")]
    [SerializeField] GameObject PlayerMenuPrefab;               // the player menu prefab
    MenuController[] menuControllers = new MenuController[4];   // 4 player menu instances

    [Header("'Press to join' Rewired Actions")]
    [SerializeField] int JoinActionID_1 = 5;                    // a couple Rewired actions,
    [SerializeField] int JoinActionID_2 = 6;                    // by ID, (press to join game)

    // instantiate 4 player menu prefabs, position each (in array, starting top-left moving clockwise)
    // disable all player menus and this manager
    // a Front Menu button opens multiplayer by enabling this object.
    void Awake() {
        MainState.Instance.MultiplayerMenuManager = this;

        // destroy any unwanted child object prefabs present in the editor
        foreach (Transform t in transform) {
            Destroy(t.gameObject);
        }

        // moves each player menu instantiated ... each clone occupies 1/4 of the screen
        for (int i = 0; i < 4; ++i) {

            GameObject go = Instantiate(PlayerMenuPrefab, transform);
            MenuController mc = go.GetComponent<MenuController>();
            menuControllers[i] = mc;

            // menu controller remembers a unique array position (PlayerData, in MainState)
            mc.ScreenPosition = i;
            // menu control is disabled
            mc.enabled = false;
            // menu is hidden
            go.SetActive(false);
            // position player menus into UI quadrants
            switch (i) {
                case 1:
                    go.transform.localPosition += Vector3.right * 960; // 960x540
                    break;
                case 2:
                    go.transform.localPosition -= Vector3.up * 540;
                    break;
                case 3:
                    go.transform.localPosition += Vector3.right * 960;
                    go.transform.localPosition -= Vector3.up * 540;
                    break;
            }
        }
        // this script waits to be enabled by a root menu
        enabled = false;
    }

    // make menus visible, but do not enable their inputs just yet
    // register all player channels to listen for join presses
    void OnEnable() {

        IList<Rewired.Player> players = ReInput.players.GetPlayers();

        // skips player 0 (this utility player always uses all controllers)
        for (int i = players.Count - 1; i >= 1; --i) {

            players[i].AddInputEventDelegate(OnInputUpdate, UpdateLoopType.Update);

            JayDebug.Log("RewiredID " + players[i].id + " is being polled for multiplayer input hook.", this);
        }

        foreach (MenuController mc in menuControllers) {
            mc.gameObject.SetActive(true);
        }
    }

    void OnDisable() {
        IList<Rewired.Player> players = ReInput.players.GetPlayers();
        for (int i = players.Count-1; i >= 1; --i) {
            players[i].ClearInputEventDelegates();
        }
    }

    void OnDestroy() {
        IList<Rewired.Player> players = ReInput.players.GetPlayers();
        for (int i = players.Count-1; i >= 1; --i) {
            players[i].ClearInputEventDelegates();
        }
    }

    // listen for all connected players,
    void OnInputUpdate(InputActionEventData Data) {

        // has this player already joined?
        if (Data.player.isPlaying == false) {

            // a new player pressed the join buttons ...
            if (Data.player.GetButtonDown(JoinActionID_1) ||
                Data.player.GetButtonDown(JoinActionID_2)) {

                // ... a player can only join once.
                Data.player.isPlaying = true;

                // find the first disabled menu, enable it for this player
                for (int i = 0; i < 4; ++i) {

                    if(menuControllers[i].enabled) {
                        continue;
                    }
                    else {
                        
                        // set the MenuController to poll this player
                        menuControllers[i].RewiredID = Data.playerId;
                        menuControllers[i].enabled = true;
                        // now the MenuController can press buttons ...
                        // ... press the first and only join button for MenuController now.
                        menuControllers[i].InvokeFocusButton();
                        break;
                    }
                }            
            }
        }
    }
    */
}