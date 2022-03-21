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
}

[Serializable]
public class ActionKeys
{
    public string keyboardKey;
    public string gamepadKey;
}
