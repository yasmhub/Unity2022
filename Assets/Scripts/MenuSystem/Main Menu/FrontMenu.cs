using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FrontMenu : BaseMenu
{
    [SerializeField] GameObject PlayerMenuPrefab;

    public void SinglePlayerButton()
    {
        GameObject go = Instantiate(PlayerMenuPrefab, transform.parent.parent);
        MenuController mc = go.GetComponent<MenuController>();

        // single player menu starts out listening to all input channel
        mc.RewiredID = 0;

        // center single player menu
        go.transform.localPosition += Vector3.right * 480;

        DisableFrontMenu();
        Destroy(gameObject);
    }


    public void MultiplayerButton()
    {
        // enable the Multiplayer Menu Manager, it will enable individual player Menu Controllers
        GameMain.Instance.MultiplayerMenuManager.enabled = true;

        DisableFrontMenu();
        Destroy(gameObject);
    }

    public void DisableFrontMenu()
    {
        // disable the main menu controller (there is no main menu open to control)
        menuController.enabled = false;
        menuController.gameObject.SetActive(false);
    }
}
