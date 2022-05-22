using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FrontMenu : BaseMenu {
    /*
    // eventually the adventure mode will also ahve some data selection, but for now just load the scene
    public void AdventureButton() {

        MainState.Instance.SceneController.LoadScene("Learning ProBuilder", LoadSceneMode.Single);

        // disable the main menu controller (there is no main menu open to control)
        menuController.enabled = false;

        Destroy(gameObject);
    }
    */
    public void SinglePlayerButton() {

        menuController.EnableMenu(1);
    }

    /*
    public void MultiplayerButton() {
        
        // enable the Multiplayer Menu Manager, which will enable up to 4 Player Menu Controllers
        MainState.Instance.MultiplayerMenuManager.enabled = true;

        // disable the main menu controller (there is no main menu open to control)
        menuController.enabled = false;

        Destroy(gameObject);
    }
    */
}
