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

    public string GetActionPath(string actionDefinition)
    {
        if (actionDefinition == "jump-keyboard") return (jumpKeys.keyboardKey);
        if (actionDefinition == "jump-gamepad") return (jumpKeys.gamepadKey);

        if (actionDefinition == "attack-keyboard") return (attackKeys.keyboardKey);
        if (actionDefinition == "attack-gamepad") return (attackKeys.gamepadKey);

        if (actionDefinition == "interact-keyboard") return (interactKeys.keyboardKey);
        if (actionDefinition == "interact-gamepad")return (interactKeys.gamepadKey);

        if (actionDefinition == "menu-keyboard") return (menuKeys.keyboardKey);
        if (actionDefinition == "menu-gamepad") return (menuKeys.gamepadKey);

        if (actionDefinition == "inventory-keyboard") return (inventoryKeys.keyboardKey);
        if (actionDefinition == "inventory-gamepad") return (inventoryKeys.gamepadKey);

        if (actionDefinition == "moveLeft-keyboard") return (moveLeftKeyboardKey);
        if (actionDefinition == "moveRight-keyboard") return (moveRightKeyboardKey);
        if (actionDefinition == "move-gamepad") return (moveGamepadKey);

        return ("");
    }

    public void SetActionPath(string actionDefinition, string newBinding)
    {
        if (actionDefinition == "jump-keyboard") jumpKeys.keyboardKey = newBinding;
        if (actionDefinition == "jump-gamepad") jumpKeys.gamepadKey = newBinding;

        if (actionDefinition == "attack-keyboard") attackKeys.keyboardKey = newBinding;
        if (actionDefinition == "attack-gamepad") attackKeys.gamepadKey = newBinding;

        if (actionDefinition == "interact-keyboard") interactKeys.keyboardKey = newBinding;
        if (actionDefinition == "interact-gamepad") interactKeys.gamepadKey = newBinding;

        if (actionDefinition == "menu-keyboard") menuKeys.keyboardKey = newBinding;
        if (actionDefinition == "menu-gamepad") menuKeys.gamepadKey = newBinding;

        if (actionDefinition == "inventory-keyboard") inventoryKeys.keyboardKey = newBinding;
        if (actionDefinition == "inventory-gamepad") inventoryKeys.gamepadKey = newBinding;

        if (actionDefinition == "moveLeft-keyboard") moveLeftKeyboardKey = newBinding;
        if (actionDefinition == "moveRight-keyboard") moveRightKeyboardKey = newBinding;
        if (actionDefinition == "move-gamepad") moveGamepadKey = newBinding;
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
