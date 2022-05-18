using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

// input file/name and create a new save file / player data
public class PlayerCreateDataMenu : BaseMenu {
    
    bool useKeyboard = false;  // check all keyboard keys in Update?

    // used to change text ui displaying characters for joystick file name input
    public void MenuInput(InputListener InputListener)
    {
        if (InputListener.back)
        {
            RemoveLetter();
            return;
        }

        float horizontal = InputListener.moveH;
        if (horizontal != 0f)
        {
            if (horizontal > 0f)
            {

                letterIndex += 1;
            }
            else
            {

                letterIndex -= 1;
            }
            UpdateWindow();
        }
    }

    [SerializeField] Text InputText;         // display the text input so far
    [SerializeField] Selectable[] LetterButtons;    // display chars on the text of these elements for controller selection
    //
    Text[] window;                          // 3 text elements (on buttons) display the selection
    char[] letters;                         // A-Z
    int letterIndex = 1;                    // index of the letter currently centered 
    int letterCount = 27;                   // how many letters? (+1, _ underscores for spaces)

    new void Start()
    {
        // baseMenu sets an initial focused Selectable chosen from the editor
        base.Start();

        // receive input from MenuController
        menuController.MenuInput += MenuInput;

        // create a char alphabet array, file names are A-Z and underscore
        letters = new char[letterCount];
        int nextASCII = 65;
        for (int i = 0; i <= 25; ++i)
        {
            letters[i] = (char)nextASCII;
            ++nextASCII;
        }
        letters[26] = '_';

        // 3 Text elements display some characters for selection
        window = new Text[3];
        for (int i = 0; i < 3; ++i)
        {
            window[i] = LetterButtons[i].transform.GetChild(0).GetComponent<Text>();
        }

        // if the menuController is Rewired player 1, the keyboard ...
        // ... also subscribe a "keyboard" function for callsign input
        if (menuController.RewiredID == 0) { useKeyboard = true; }

        // UpdateWindow is called on input to scroll the chars displayed
        // The first update centers the letter "B" displaying "A B C"
        UpdateWindow();
        // clear editor placeholder text
        InputText.text = "";
    }

    // the keyboard should be able to directly type in a name
    void Update()
    {
        if (useKeyboard)
        {
            string name = InputText.text;
            // see KeyCode definition, 97-122 are A-Z
            for (int i = 97; i <= 122; ++i)
            {

                KeyCode k = (KeyCode)i;
                if (Input.GetKeyDown(k))
                {
                    // offset Unity KeyCode enum to ASCII
                    name += (char)(i - 32);
                    InputText.text = name;
                    return;
                }
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                RemoveLetter();
            }
        }
    }

    // update the text elements in window[] to display chars for selection
    void UpdateWindow()
    {
        // wrap letterINdex scrolling around the char array
        if (letterIndex >= letterCount)
        {
            letterIndex = 0;
        }
        else if (letterIndex == -1)
        {
            letterIndex = letterCount - 1;
        }

        int a = (letterIndex - 1) % letterCount;
        if (a == -1)
        {
            a = letterCount - 1;
        }

        window[0].text = letters[a].ToString();
        window[1].text = letters[letterIndex].ToString();

        a = (letterIndex + 1) % letterCount;
        window[2].text = letters[a].ToString();
    }

    // add the focused letter to the file name
    public void AddLetter()
    {
        string name = InputText.text;

        name += letters[letterIndex].ToString();
        InputText.text = name;

        UpdateWindow();
    }

    // backspace - remove the last letter
    public void RemoveLetter()
    {
        string name = InputText.text;

        if (name.Length >= 1)
        {

            name = InputText.text;
            name = name.Substring(0, name.Length - 1);
            InputText.text = name;
            UpdateWindow();
        }
    }

    // the JOIN button creates a new player data and opens the next menu
    public void CreatePlayerDataButton()
    {
        var playerData = PlayerDataController.CreatePlayerData(InputText.text);

        // file already exists (flash red or somthing)
        if (playerData.Item2 == false)
        {
            return;
        }
        else
        {
            playerData.Item1.RewiredID = menuController.RewiredID;
            // menuController.EnableMenu(#);
            GameMain.Game.AddPlayer(playerData.Item1);
        }
    }

    public void BackButton()
    {
        // enabled the player data select menu
        menuController.EnableMenu(1);
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        menuController.MenuInput -= MenuInput;
    }
}