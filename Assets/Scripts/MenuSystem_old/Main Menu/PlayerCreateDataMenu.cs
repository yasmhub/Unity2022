using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

// scroll through the letters and input a filename
public class PlayerCreateDataMenu : BaseMenu {
    
    [SerializeField] Text InputText;         // display the text input so far
    [SerializeField] Selectable[] LetterButtons;    // display chars on the text of these elements for controller selection
    //
    Text[] window;                          // 3 text elements (on buttons) display the selection
    char[] letters;                         // A-Z
    int letterIndex = 1;                    // index of the letter currently centered 
    int letterCount = 27;                   // how many letters? (+1, _ underscores for spaces)
    //
    bool useKeyboard = false;               // check all keyboard keys in Update?
    /*
	new void Start () {
        // baseMenu sets an initial focused Selectable chosen from the editor
        base.Start();

        // create a char alphabet array, file names are A-Z and underscore
        letters = new char[letterCount];
        int nextASCII = 65;
        for(int i = 0; i <= 25; ++i) {
            letters[i] = (char)nextASCII;
            ++nextASCII;
        }
        letters[26] = '_';

        // 3 Text elements display some characters for selection
        window = new Text[3];
        for(int i = 0; i < 3; ++i) {
            window[i] = LetterButtons[i].transform.GetChild(0).GetComponent<Text>();
        }

        // if the menuController is Rewired player 1, the keyboard ...
        // ... also subscribe a "keyboard" function for callsign input
        if(menuController.RewiredID == 1 || menuController.RewiredID == 0) { useKeyboard = true; }

        // subscribe to input events- this script additionally moves a transform on menu inputs
        // menuController.MenuInputEvent += MenuInputUpdate; // ----------------------------------------------------- replace this
        MenuController mc = 
        GameMain.Game.Input.listeners[menuController.RewiredID].AddReciever(this);

        // UpdateWindow is called on input to scroll the chars displayed
        // The first update centers the letter "B" displaying "A B C"
        UpdateWindow();
        // clear editor placeholder text
        InputText.text = "";
    }
    
    // displays a scrolling window over a char array
    void MenuInputUpdate(MenuInputData InputData) {

        // if the player presses "A" input the character
        if(InputData.jump) {
            AddLetter();
            // menuController.InvokeFocusButton(); also acessible by external UI call
            return;
        }

        if (InputData.back) {
            RemoveLetter();
            return;
        }

        // while the focus is on the input letter, scroll the input char window left and right
        if (menuController.GetFocus.name == "Input Letter Button") {
            float horizontal = InputData.horizontal;

            if (horizontal != 0f) {

                if (horizontal > 0f) {

                    letterIndex += 1;
                }
                else {

                    letterIndex -= 1;
                }
                UpdateWindow();
            }
        }
    }

    void Update() {

        // the keyboard player uses this method to directly type into the field
        if (useKeyboard) {
            string name = InputText.text;
            // see KeyCode definition, 97-122 are A-Z
            for (int i = 97; i <= 122; ++i) {

                KeyCode k = (KeyCode)i;
                if (Input.GetKeyDown(k)) {
                    // offset Unity KeyCode enum to ASCII
                    name += (char)(i - 32);
                    InputText.text = name;
                    return;
                }
            }

            if(Input.GetKeyDown(KeyCode.Backspace)) {
                RemoveLetter();
            }
        }
    }

    // update the text elements in window[] to display chars for selection
    void UpdateWindow() {

        // wrap letterINdex scrolling around the char array
        if (letterIndex >= letterCount) {
            letterIndex = 0;
        }
        else if (letterIndex == -1) {
            letterIndex = letterCount - 1;
        }

        int a = (letterIndex - 1) % letterCount;
        if(a == -1) {
            a = letterCount - 1;
        }

        window[0].text = letters[a].ToString();
        window[1].text = letters[letterIndex].ToString();

        a = (letterIndex + 1) % letterCount;
        window[2].text = letters[a].ToString();
    }

    // button function called from UI by controller presses
    // add the focused letter the the new name
    public void AddLetter() {
        string name = InputText.text;

        name += letters[letterIndex].ToString();
        InputText.text = name;

        UpdateWindow();
    }

    // backspace - remove the last letter
    public void RemoveLetter() {
        string name = InputText.text;

        if (name.Length >= 1) {

            name = InputText.text;
            name = name.Substring(0, name.Length - 1);
            InputText.text = name;
            UpdateWindow();
        }
    }

    // the JOIN button creates a new player data and opens the next menu
    public void CreatePlayerDataButton() {

        // PlayerDataController will attempt to write a new PlayerData. 
        if (PlayerDataController.WriteNewPlayerData(InputText.text)) {

            //menuController.EnableMenu(3);
            Destroy(gameObject);
        }
        else {
            // False if the file already exists (flash red or somth)
        }
    }

    public void BackButton() {

        // enabled the player data select menu
        menuController.EnableMenu(1);
        Destroy(gameObject);
    }
    */
}