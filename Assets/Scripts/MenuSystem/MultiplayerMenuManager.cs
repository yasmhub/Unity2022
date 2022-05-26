#define JAY_DEBUG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

// instantiate four small menus for players
// listens for an initial input, then enables a menu for each player
public class MultiplayerMenuManager : MonoBehaviour {

    [Header("Player Menu Prefab")]
    [SerializeField] GameObject PlayerMenuPrefab;               // the player menu prefab
    MenuController[] menuControllers = new MenuController[2];   // 4 player menu instances

    int JoinActionID_1 = 4;                                     // rewired action that joins game

    // instantiate a menu for each player, position on screen
    void Awake()
    {
        GameMain.Instance.MultiplayerMenuManager = this;

        // destroy any unwanted child object prefabs present in the editor
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        //instantiate, position coop menus
        for (int i = 0; i < 2; ++i)
        {
            GameObject go = Instantiate(PlayerMenuPrefab, transform);
            MenuController mc = go.GetComponent<MenuController>();
            menuControllers[i] = mc;

            // menus start out disabled until being activated by input
            mc.enabled = false;
            go.SetActive(false);

            // position player menu
            if (i == 1)
            {
                go.transform.localPosition += Vector3.right * 960;
            }
        }
        // this script waits to be enabled by a root menu
        enabled = false;
    }

    // make menus visible, but do not enable their inputs just yet
    // register all player channels to listen for join presses
    void OnEnable()
    {

        IList<Rewired.Player> players = ReInput.players.GetPlayers();

        // skips player 0 (this utility player always uses all controllers)
        for (int i = players.Count - 1; i >= 1; --i)
        {

            players[i].AddInputEventDelegate(OnInputUpdate, UpdateLoopType.Update);

            Debug.Log("RewiredID " + players[i].id + " is being polled for multiplayer input hook.", this);
        }

        foreach (MenuController mc in menuControllers)
        {
            mc.gameObject.SetActive(true);
        }
    }

    // listen for all connected players,
    void OnInputUpdate(InputActionEventData Data)
    {

        // has this player already joined?
        if (Data.player.isPlaying == false)
        {
            // a new player pressed the join buttons ...
            if (Data.player.GetButtonDown(JoinActionID_1))
            {
                // ... a player can only join once.
                Data.player.isPlaying = true;

                // find the first disabled menu, enable it for this player
                for (int i = 0; i < 2; ++i)
                {
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
}