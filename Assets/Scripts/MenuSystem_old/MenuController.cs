// #define JAY_DEBUG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

// instantiate menu prefabs & poll navigation for one player 
public class MenuController : MonoBehaviour, InputReceiver {

    public void InputUpdate(InputListener InputListener)
    {
        Debug.Log("x");
        // if previous input hasn't locked us out
        if (Time.time >= nextInputTime)
        {
            // jump is "select"
            if (InputListener.jump)
            {
                InvokeFocusButton();
                nextInputTime = Time.time + InputFrequency;
            }
            // 
            if (InputListener.back)
            {

                nextInputTime = Time.time + InputFrequency;
            }

            Debug.Log("x");


            // input moves selectable
            Selectable newFocus = null;
            if (Mathf.Abs(InputListener.moveV) >= 0.2f)
            {
                if (InputListener.moveV > 0)
                {

                    newFocus = focus.FindSelectableOnUp();
                }
                else
                {
                    newFocus = focus.FindSelectableOnDown();
                }
            }
            if (Mathf.Abs(InputListener.moveV) >= 0.2f)
            {
                if (InputListener.moveV > 0)
                {

                    newFocus = focus.FindSelectableOnUp();
                }
                else
                {

                    newFocus = focus.FindSelectableOnDown();
                }
            }
            if (newFocus != null)
            {
                SetFocus(newFocus);
                nextInputTime = Time.time + InputFrequency;
            }
        }
    }

    [Header("Channel driving this menu.")]
    [SerializeField] int rewiredID;
    [Header("Screen position of menu.")]
    [SerializeField] int screenPosition = 0;
    [Header("First listed is initial menu.")]
    [SerializeField] GameObject[] MenuPrefabs;  // one instantiated at a time
    // 
    Player rewiredPlayer;                       // the player controlling this menu

    BaseMenu currentMenu;
    Selectable focus;

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
    public int RewiredID
    {
        get {
            return rewiredID;
        }
        set {
            rewiredID = value;
            GameMain.Game.Input.listeners[rewiredID].AddReciever(this);
        }
    }

    // player create data menu checks focus
    // public Selectable GetFocus {
    //    get { return focus; }
    //}

    void Awake() {

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

    void Start()
    {
        rewiredPlayer = ReInput.players.GetPlayer(rewiredID);

        // get input singleton
        GameMain.Game.Input.listeners[rewiredID].AddReciever(this);
    }

    void OnEnable() {
        nextInputTime = Time.time + InputFrequency;
    }

    // try to invoke the focused menu button's function
    public void InvokeFocusButton()
    {
        if (focus.GetComponent<Button>())
        {

            Button b = focus.GetComponent<Button>();
            b.onClick.Invoke();
            return;
        }
    }

    public void InvokeBackButton()
    {

        if (focus.GetComponent<Button>())
        {

            Button b = focus.GetComponent<Button>();
            b.onClick.Invoke();
            return;
        }
    }

    // instantiate the selected menu
    public void EnableMenu(int Index)
    {
        DestroyMenu();
        GameObject go = Instantiate(MenuPrefabs[Index], transform);

        // before activating, set the color of any buttons to this controller's default
        for (int i = go.transform.childCount - 1; i >= 0; --i)
        {
            // for each transform child
            Transform child = go.transform.GetChild(i);
            // if the child has a Selectable
            if (child.GetComponent<Selectable>())
            {
                // set the Selectable's color block to match controller's default
                Selectable s = child.GetComponent<Selectable>();
                s.colors = defaultColors;
            }
        }

        go.SetActive(true);
    }

    public void DestroyMenu()
    {
        Destroy(transform.GetChild(0).gameObject);
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
}