using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

// a common base class for all generic menus
public class BaseMenu : MonoBehaviour {

    [SerializeField] Selectable InitialFocus;       // inform the menu controller which Selectable is first
    [SerializeField] Selectable BackButton;         // most menus have a "back" button
    protected MenuController menuController;        // the MenuController which instantiated this script

    protected void Start()
    {
        // reliable because BaseMenus are instantiated by and attached to MenuControllers
        menuController = transform.GetComponentInParent<MenuController>();
        menuController.SetFocus(InitialFocus);        
    }
}