#define JAY_DEBUG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

// this menu instantiates a list of buttons for each save file found
// their text is set to match player save filenames
// when pressed, they read back the UI Text element and load the matching file
public class PlayerSelectDataMenu : BaseMenu {
    /*
    // This button is used as a "local prefab"
    // It's simply disabled and copied for each player save
    [SerializeField] GameObject PlayerDataButton;
    Vector3 buttonOffset;       // how far to move each button
    Selectable lastButton;      // used to tie navigation links together
    //
    Transform buttonParent;     // all the buttons are attached to this moving object

    new void Start() {
        // all BaseMenus reference a MenuController
        // baseMenu sets an initial focused Selectable
        base.Start();

        // subscribe to input events- this script additionally moves a transform on menu inputs
        menuController.MenuInputEvent += MenuInputMoveButtons;

        // get the button's height (use to determine positioning offset)
        float buttonHeight = PlayerDataButton.GetComponent<RectTransform>().rect.height;
        // the buttons will be attached to a common parent
        buttonParent = PlayerDataButton.transform.parent;
        // each time a button is added move this far
        buttonOffset = Vector3.down * buttonHeight;

        // if there are save files, clone a button for each one
        string[] fileNames;
        if(PlayerDataController.GetFileNames()) {

            fileNames = PlayerDataController.FileNames;

            int nameCount = fileNames.Length - 1;
            for (int i = 0; i <= nameCount; ++i) {

                Selectable newButton;
                Text buttonText;
                Navigation nav = new Navigation();

                // first button is edge case (already exists, and the initial loop would have a null lastButton for nav links)
                // (the first in the list can simply skip nav links)
                if (i == 0) {

                    // set the Text element of the button
                    buttonText = PlayerDataButton.transform.GetChild(0).GetComponent<Text>();
                    buttonText.text = fileNames[i];

                    lastButton = PlayerDataButton.GetComponent<Selectable>();
                }
                else {

                    // instantiate a new button for each player, positioned at the original
                    GameObject go = Instantiate(PlayerDataButton, buttonParent, false);
                    // move each player's button down in local UI space by the offset
                    go.transform.localPosition += (buttonOffset * i);

                    // get the Text element of the button
                    buttonText = go.transform.GetChild(0).GetComponent<Text>();
                    buttonText.text = fileNames[i];

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

    void OnDestroy() {
        menuController.MenuInputEvent -= MenuInputMoveButtons;
    }

    // input frequency is gated by the controller, whose events call this function
    void MenuInputMoveButtons(MenuInputData InputData) {

        if(InputData.focusMoved) {
            float vertical = InputData.vertical;
            if (vertical != 0f) {

                if (vertical > 0f) {

                    buttonParent.transform.localPosition += buttonOffset;
                }
                else {

                    buttonParent.transform.localPosition -= buttonOffset;
                }
            }
        }
    }
    */

    // enable save file creation menu
    public void NewPlayerButton() {

        menuController.EnableMenu(2);

        // don't forget to unsubscribe the additional input action
        //menuController.MenuInputEvent -= MenuInputMoveButtons;

        //Destroy(gameObject);
    }
}