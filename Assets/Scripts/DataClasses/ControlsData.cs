using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ControlsData
{
    public ActionKeys jumpKeys;
    public ActionKeys attackKeys;
    public ActionKeys interactKeys;
    public ActionKeys menuKeys;
    public ActionKeys inventoryKeys;

    public string moveLeftKeyboardKey;
    public string moveRightKeyboardKey;
    public string moveGamepadKey;

    public ControlsData()
    {
        jumpKeys = new ActionKeys();
        attackKeys = new ActionKeys();
        interactKeys = new ActionKeys();
        menuKeys = new ActionKeys();
        inventoryKeys = new ActionKeys();

        moveGamepadKey = "";
        moveLeftKeyboardKey = "";
        moveRightKeyboardKey = "";

    }
}

[Serializable]
public class ActionKeys
{
    public string keyboardKey;
    public string gamepadKey;

    public ActionKeys()
    {
        keyboardKey = "";
        gamepadKey = "";
    }
}
