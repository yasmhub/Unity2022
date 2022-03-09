﻿// #define JAY_DEBUG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

// some menus may subscribe to input events
public delegate void MenuInputEventHandler(MenuInputData InputData);

// they recieve the inputs polled here in a struct
public struct MenuInputData {

    public MenuInputData(float Vertical, float Horizontal, bool Jump, bool Back, bool FocusMoved) {
        vertical = Vertical;
        horizontal = Horizontal;
        jump = Jump;
        back = Back;
        focusMoved = FocusMoved;
    }

    public float vertical;
    public float horizontal;
    public bool jump;
    public bool back;
    // did the focus move to a valid neighbor on this input?
    // makes it easy to stop menus that move at the bounds of their elements- without otherwise programming limits
    public bool focusMoved;
}

// instantiate menu prefabs & poll navigation for one player 
public class MenuController : MonoBehaviour {

    // by sending events, all individual BaseMenu scripts are gated by the same rate limiter here
    // -it is simple for them to subscribe and unsubscribe for any additional behaviour necessary
    public MenuInputEventHandler MenuInputEvent;
    MenuInputData inputData;

    [Header("Channel driving this menu.")]
    [SerializeField] int rewiredID;
    [Header("Screen position of menu.")]
    [SerializeField] int screenPosition = 0;    // 0...3 top-left, clockwise
    [Header("First listed is initial menu.")]
    [SerializeField] GameObject[] MenuPrefabs;  // one instantiated at a time
    // 
    Player rewiredPlayer;                       // the player controlling this menu
    Selectable focus;                           // current menu selection: new menus use SetFocus on Start          
    // button components tiny the focused graphic with these colorblocks
    [Header("Menu Focus Color")]
    [SerializeField] Color FocusedColor = Color.cyan;
    [SerializeField] Color DefaultColor = Color.white;
    ColorBlock focusedColors;
    ColorBlock defaultColors;
    // input is rate-limited
    [SerializeField] float InputFrequency = 0.3f;
    float nextInputTime;
    // NultiplayerMenuManager sets these 0...3 on instantiated menus
    public int ScreenPosition {
        get { return screenPosition; }
        set { screenPosition = value; }
    }

    // the multiplayer lobby has a disabled MenuController for each player.
    // it listens for input, and sets the player who will control the menus here ...
    public int RewiredID {
        get {
            return rewiredID;
        }
        set {
            rewiredID = value;
            rewiredPlayer = ReInput.players.GetPlayer(rewiredID);
        }
    }
    // PlasyerSelectDataMenu polls and performs additional actions while open
    public Player RewiredPlayer {
        get { return rewiredPlayer; }
    }
    // player create data menu checks focus
    public Selectable GetFocus {
        get { return focus; }
    }

    void Awake() {

        // default to prevent nulls when event is called while no menu is subscribed
        MenuInputEvent += DefaultInputEvent;

        // destroy any child objects (menu prefabs being worked on in the editor)
        foreach (Transform t in transform) {
            Destroy(t.gameObject);
        }

        // create the ColorBlock for each color
        focusedColors = new ColorBlock();
        focusedColors.normalColor = FocusedColor;
        focusedColors.highlightedColor = FocusedColor;
        focusedColors.pressedColor = FocusedColor;
        focusedColors.colorMultiplier = 1;
        defaultColors = new ColorBlock();
        defaultColors.normalColor = DefaultColor;
        defaultColors.highlightedColor = FocusedColor;
        defaultColors.pressedColor = FocusedColor;
        defaultColors.colorMultiplier = 1;

        nextInputTime = Time.time + InputFrequency;
        EnableMenu(0);
    }

    void Start() {
        rewiredPlayer = ReInput.players.GetPlayer(rewiredID);
    }

    void OnEnable() {
        nextInputTime = Time.time + InputFrequency;
    }
    
    // rate-limit input and fire input events menus can respond to
    void Update() {
        
        // if previous input hasn't locked us out
        if(Time.time >= nextInputTime) {

            bool hasInput = false;          // was any input used? If so, fire an event
            float vertical = rewiredPlayer.GetAxis("MoveVertical");
            float horizontal = rewiredPlayer.GetAxis("MoveHorizontal");
            bool jump = rewiredPlayer.GetButtonDown("Jump");
            //bool back = rewiredPlayer.GetButtonDown("Back");
            bool back = false;
            bool focusMoved = false;        // did this input move the focused element?
            Selectable newFocus = null;     // where did it move to?


            if(back) {
                Debug.Log(back);
                hasInput = true;
                nextInputTime = Time.time + InputFrequency;
            }

            // jump is "select"
            if (jump) {
                hasInput = true;
                nextInputTime = Time.time + InputFrequency;

                // if there's a button, press it
                InvokeFocusButton();
            }

            // menu movement via editor's explicit navigation links
            if(Mathf.Abs(vertical) >= 0.2f) {
                hasInput = true;
                nextInputTime = Time.time + InputFrequency;

                if (vertical > 0) {

                    newFocus = focus.FindSelectableOnUp();
                }
                else {

                    newFocus = focus.FindSelectableOnDown();
                }
            }

            if (Mathf.Abs(horizontal) >= 0.2f) {
                hasInput = true;
                nextInputTime = Time.time + InputFrequency;

                if (horizontal > 0f) {

                    newFocus = focus.FindSelectableOnRight();
                }
                else {

                    newFocus = focus.FindSelectableOnLeft();
                }
            }

            // if the UI element had a Selectable neighbor, move focus
            if (newFocus != null) {
                focusMoved = true;
                SetFocus(newFocus);
                // also, tell listening menus if focus moved or not
            }

            // send input to any other menus that might have additional arbitrary responses
            if (hasInput) {

                MenuInputData inputData = new MenuInputData(vertical, horizontal, jump, back, focusMoved);
                MenuInputEvent.Invoke(inputData);
            }
        }
    }

    // try to invoke the focused menu button's function
    public void InvokeFocusButton() {

        if (focus.GetComponent<Button>()) {

            Button b = focus.GetComponent<Button>();
            b.onClick.Invoke();
            return;
        }
    }

    // instantiate the selected menu
    public void EnableMenu(int Index) {

        GameObject go = Instantiate(MenuPrefabs[Index], transform);

        // before activating, set the color of any buttons to this controller's default
        for(int i = go.transform.childCount-1; i >=0; --i) {
            // for each transform child
            Transform child = go.transform.GetChild(i);
            // if the child has a Selectable
            if(child.GetComponent<Selectable>()) {
                // set the Selectable's color block to match controller's default
                Selectable s = child.GetComponent<Selectable>();
                s.colors = defaultColors;
            }
        }

        go.SetActive(true);
    }
    // (menus typically destroy themselves when enabling another menu)

    // Update() polling tries to move the focused element
    public void SetFocus(Selectable NewFocus) {

        if (focus != null) {

            focus.colors = defaultColors;
            focus = NewFocus;
            focus.colors = focusedColors;
        }
        else {

            // initial case
            focus = NewFocus;
            focus.colors = focusedColors;
        }
    }

    // default subscriber to stop null check in case no menu is using the input
    public void DefaultInputEvent(MenuInputData m) { }
}