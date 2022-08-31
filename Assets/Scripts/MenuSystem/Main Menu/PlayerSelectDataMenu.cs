#define JAY_DEBUG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

// this menu instantiates a list of buttons for each save file found
// when pressed, they read back the UI Text element and load the matching file
public class PlayerSelectDataMenu : BaseMenu
{

    [SerializeField] GameObject TemplateButton;
    Vector3 buttonOffset;       // how far to move each button
    Selectable lastButton;      // used to tie navigation links together
    //
    Transform buttonParent;     // all the buttons are attached to this moving object

    public void MenuInput(InputListener InputListener)
    {
        if (menuController.focusMoved)
        {
            if (InputListener.moveV > 0)
            {

                buttonParent.transform.localPosition += buttonOffset;
            }
            else
            {

                buttonParent.transform.localPosition -= buttonOffset;
            }
        }
    }

    new void Start()
    {
        base.Start();

        // receive input from MenuController
        menuController.MenuInput += MenuInput;

        // get a template button to clone for load player buttons
        TemplateButton = transform.GetChild(0).GetChild(0).Find("Player Data Button").gameObject;
        // get the button's height (use to determine positioning offset)
        float buttonHeight = TemplateButton.GetComponent<RectTransform>().rect.height;
        // the buttons will be attached to a common parent
        buttonParent = TemplateButton.transform.parent;
        // each time a button is added move this far
        buttonOffset = Vector3.down * buttonHeight;

        // if there are save files, clone a button for each one
        var fileNames = PlayerDataController.GetFileNames();
        if (fileNames.Item2)
        {

            int nameCount = fileNames.Item1.Length - 1;
            for (int i = 0; i <= nameCount; ++i)
            {

                Selectable newButton;
                Text buttonText;
                Navigation nav = new Navigation();

                // first button is edge case (already exists, and the initial loop would have a null lastButton for nav links)
                // (the first in the list can simply skip nav links)
                if (i == 0)
                {

                    // set the Text element of the button
                    buttonText = TemplateButton.transform.GetChild(0).GetComponent<Text>();
                    buttonText.text = fileNames.Item1[i];

                    lastButton = TemplateButton.GetComponent<Selectable>();
                }
                else
                {

                    // instantiate a new button for each player, positioned at the original
                    GameObject go = Instantiate(TemplateButton, buttonParent, false);
                    // move each player's button down in local UI space by the offset
                    go.transform.localPosition += (buttonOffset * i);

                    // get the Text element of the button
                    buttonText = go.transform.GetChild(0).GetComponent<Text>();
                    buttonText.text = fileNames.Item1[i];

                    // get the Button element    
                    newButton = go.GetComponent<Selectable>();
                    // create navigation mode & links for buttons
                    nav.mode = Navigation.Mode.Explicit;
                    nav.selectOnUp = lastButton;
                    // set "select on up" to be the previous button
                    newButton.navigation = nav;
                    // set this button as "select on down" of the last button
                    nav = lastButton.navigation;
                    nav.selectOnDown = newButton;
                    lastButton.navigation = nav;

                    // remember the last button for nav linking
                    lastButton = newButton;
                }
            }
        }

        // this button was a template object, we're done copying it
        // Destroy(PlayerDataButton);
    }

    void OnDestroy()
    {
        // receive input from MenuController
        menuController.MenuInput -= MenuInput;
    }

    // open PlayerCreateDataMenu
    public void NewPlayerButton()
    {
        menuController.EnableMenu(2);
        //Destroy(gameObject);
    }

    // load selected PlayerData
    public void LoadPlayerButton(Transform t)
    {
        // buttons were generated with text matching filenames
        string n = t.GetChild(0).GetComponent<Text>().text;
        PlayerData d = PlayerDataController.LoadPlayerData(n);
        GameMain.Instance.AddPlayer(d);


    }
}