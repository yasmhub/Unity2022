using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FrontMenu : BaseMenu 
{

    public void SinglePlayerButton()
    {
        menuController.EnableMenu(1);
    }

    
    public void MultiplayerButton()
    {       
        // enable the Multiplayer Menu Manager, it will enable individual player Menu Controllers
        GameMain.Instance.MultiplayerMenuManager.enabled = true;

        // disable the main menu controller (there is no main menu open to control)
        menuController.enabled = false;
        menuController.gameObject.SetActive(false);
        
        Destroy(gameObject);
    }
}
