using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this script runs a countdown timer and starts the multiplayer game
public class MultiplayerMenuLobby : MonoBehaviour {
    /*
    // used to decide when to start the multiplayer game
    [SerializeField] Text CountdownText;
    int activePlayers = 0;                  // number of players who are past "press start to join"
    int readyPlayers = 0;                   // number of players who are ready to play
    float countdownDuration = 5f;           // if all players are ready, how long till the game starts?
    float countdownRemaining = -1f;         // -1 means the countdown is not running

    // when the plast player leaves the lobby, return to the front menu
    public int GetActivePlayers {
        get { return activePlayers; }
    }
    public int AddActivePlayers {
        set { activePlayers += value; }
    }
    public int AddReadyPlayers {
        set { readyPlayers += value; }
    }

    // main state check-in, hide countdown-text
    void Awake() {
        MainState.Instance.MultiplayerMenuLobby = this;
        CountdownText.enabled = false;
    }

    // run a countdown and start the game if all players are ready
    void Update() {

        // at least two players are ready
        if(readyPlayers == activePlayers && activePlayers >= 2) {
            // countdown isn't running, start it
            if(countdownRemaining == -1f) {
                countdownRemaining = countdownDuration;
                CountdownText.enabled = true;
            }
            else {

                // the countdown is running
                if (countdownRemaining >= 0f) {
                    countdownRemaining -= Time.deltaTime;

                    int round = (int)countdownRemaining;
                    CountdownText.text = round.ToString();
                }
                else {

                    // time to start the game
                }
            }
        }
        else {
            // players aren't ready, stop any countdown
            if(countdownRemaining != -1f) {
                countdownRemaining = -1f;
                CountdownText.enabled = false;
            }
        }
    }
    */
}
