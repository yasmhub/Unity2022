using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class RewiredTest : MonoBehaviour
{
    void Start()
    {
        // Log assigned Joystick information for all joysticks regardless of whether or not they've been assigned
        Debug.Log("Rewired found " + ReInput.controllers.joystickCount + " joysticks attached.");
        for (int i = 0; i < ReInput.controllers.joystickCount; i++)
        {
            Joystick j = ReInput.controllers.Joysticks[i];
            Debug.Log(
                "[" + i + "] Joystick: " + j.name + "\n" +
                "Hardware Name: " + j.hardwareName + "\n" +
                "Is Recognized: " + (j.hardwareTypeGuid != System.Guid.Empty ? "Yes" : "No") + "\n" +
                "Is Assigned: " + (ReInput.controllers.IsControllerAssigned(j.type, j) ? "Yes" : "No")
            );
        }

        // Log assigned Joystick information for each Player
        foreach (var p in ReInput.players.Players)
        {
            Debug.Log("PlayerId = " + p.id + " is assigned " + p.controllers.joystickCount + " joysticks.");

            // Log information for each Joystick assigned to this Player
            foreach (var j in p.controllers.Joysticks)
            {
                Debug.Log(
                  "Joystick: " + j.name + "\n" +
                  "Is Recognized: " + (j.hardwareTypeGuid != System.Guid.Empty ? "Yes" : "No")
                );

                // Log information for each Controller Map for this Joystick
                foreach (var map in p.controllers.maps.GetMaps(j.type, j.id))
                {
                    Debug.Log("Controller Map:\n" +
                        "Category = " +
                        ReInput.mapping.GetMapCategory(map.categoryId).name + "\n" +
                        "Layout = " +
                        ReInput.mapping.GetJoystickLayout(map.layoutId).name + "\n" +
                        "enabled = " + map.enabled
                    );
                    foreach (var aem in map.GetElementMaps())
                    {
                        var action = ReInput.mapping.GetAction(aem.actionId);
                        if (action == null) continue; // invalid Action
                        Debug.Log("Action \"" + action.name + "\" is bound to \"" +
                            aem.elementIdentifierName + "\""
                        );
                    }
                }
            }
        }
    }

}
