using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ControlsManager : MonoBehaviour
{
    public static ControlsManager Instance;

    private PlayerControls playerControls;

    private ControlsData controlsData;
    private string jsonPath;
    private InputActionRebindingExtensions.RebindingOperation operation;

    private UnityEvent controlsChangedEvent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Already Instanciated");
        }

        controlsChangedEvent = new UnityEvent();
    }

    #region Getter-Setter

    public void AddListener(UnityAction callback)
    {
        controlsChangedEvent.AddListener(callback);
        controlsChangedEvent.Invoke();
    }

    public string GetActionPath(string actionDefinition)
    {
        if (actionDefinition == "jump-keyboard")
        {
            return (controlsData.jumpKeys.keyboardKey);
        }
        if (actionDefinition == "jump-gamepad")
        {
            return (controlsData.jumpKeys.gamepadKey);
        }

        if (actionDefinition == "attack-keyboard")
        {
            return (controlsData.attackKeys.keyboardKey);
        }
        if (actionDefinition == "attack-gamepad")
        {
            return (controlsData.attackKeys.gamepadKey);
        }

        if (actionDefinition == "interact-keyboard")
        {
            return (controlsData.interactKeys.keyboardKey);
        }
        if (actionDefinition == "interact-gamepad")
        {
            return (controlsData.interactKeys.gamepadKey);
        }

        if (actionDefinition == "menu-keyboard")
        {
            return (controlsData.menuKeys.keyboardKey);
        }
        if (actionDefinition == "menu-gamepad")
        {
            return (controlsData.menuKeys.gamepadKey);
        }

        if (actionDefinition == "inventory-keyboard")
        {
            return (controlsData.inventoryKeys.keyboardKey);
        }
        if (actionDefinition == "inventory-gamepad")
        {
            return (controlsData.inventoryKeys.gamepadKey);
        }

        if (actionDefinition == "moveLeft-keyboard")
        {
            return (controlsData.moveLeftKeyboardKey);
        }
        if (actionDefinition == "moveRight-keyboard")
        {
            return (controlsData.moveRightKeyboardKey);
        }
        if (actionDefinition == "move-gamepad")
        {
            return (controlsData.moveGamepadKey);
        }
        return ("");
    }

    public void SetActionPath(string actionDefinition, string newBinding)
    {
        if (actionDefinition == "jump-keyboard")
        {
            controlsData.jumpKeys.keyboardKey = newBinding;
        }
        if (actionDefinition == "jump-gamepad")
        {
            controlsData.jumpKeys.gamepadKey = newBinding;
        }

        if (actionDefinition == "attack-keyboard")
        {
            controlsData.attackKeys.keyboardKey = newBinding;
        }
        if (actionDefinition == "attack-gamepad")
        {
            controlsData.attackKeys.gamepadKey = newBinding;
        }

        if (actionDefinition == "interact-keyboard")
        {
            controlsData.interactKeys.keyboardKey = newBinding;
        }
        if (actionDefinition == "interact-gamepad")
        {
            controlsData.interactKeys.gamepadKey = newBinding;
        }

        if (actionDefinition == "menu-keyboard")
        {
            controlsData.menuKeys.keyboardKey = newBinding;
        }
        if (actionDefinition == "menu-gamepad")
        {
            controlsData.menuKeys.gamepadKey = newBinding;
        }

        if (actionDefinition == "inventory-keyboard")
        {
            controlsData.inventoryKeys.keyboardKey = newBinding;
        }
        if (actionDefinition == "inventory-gamepad")
        {
            controlsData.inventoryKeys.gamepadKey = newBinding;
        }

        if (actionDefinition == "moveLeft-keyboard")
        {
            controlsData.moveLeftKeyboardKey = newBinding;
        }
        if (actionDefinition == "moveRight-keyboard")
        {
            controlsData.moveRightKeyboardKey = newBinding;
        }
        if (actionDefinition == "move-gamepad")
        {
            controlsData.moveGamepadKey = newBinding;
        }
    }

    #endregion

    #region Dictionary Initialization

    private void Start()
    {
        Debug.Log("Init ControlsManager");

        playerControls = new PlayerControls();
        playerControls.Enable();

        jsonPath = Application.persistentDataPath + "/controls.json";
        controlsData = new ControlsData();

        FillKeysFromJson();

        playerControls.Gameplay.MoveLeftKeyboard.performed += (ctx => Move(1.0f));
        playerControls.Gameplay.MoveRightKeyboard.performed += (ctx => Move(-1.0f));
        playerControls.Gameplay.MoveGamepad.performed += (ctx => Move(ctx.ReadValue<Vector2>().x));

        playerControls.Gameplay.Jump.performed += (ctx => Jump(ctx.control.path));

        playerControls.Gameplay.Interact.performed += (ctx => Interact());

        playerControls.Gameplay.Attack.performed += (ctx => Attack());

        playerControls.Gameplay.OpenInventory.performed += (ctx => OpenInventory());

        playerControls.Gameplay.OpenMenu.performed += (ctx => OpenMenu());
    }

    public void FillKeysFromJson()
    {
        ControlsData savedControls = new ControlsData();

        //Setting up a correct ControlsData object

        if (File.Exists(jsonPath))
        {
            string file = File.ReadAllText(jsonPath);
            savedControls = JsonUtility.FromJson<ControlsData>(file);
        }
        //jump

        if (savedControls.jumpKeys.keyboardKey == "")
        {
            savedControls.jumpKeys.keyboardKey = playerControls.Gameplay.Jump.bindings[0].path;
        }
        if (savedControls.jumpKeys.gamepadKey == "")
        {
            savedControls.jumpKeys.gamepadKey = playerControls.Gameplay.Jump.bindings[1].path;
        }
        //attack
        if (savedControls.attackKeys.keyboardKey == "")
        {
            savedControls.attackKeys.keyboardKey = playerControls.Gameplay.Attack.bindings[0].path;
        }
        if (savedControls.attackKeys.gamepadKey == "")
        {
            savedControls.attackKeys.gamepadKey = playerControls.Gameplay.Attack.bindings[1].path;
        }
        //interact
        if (savedControls.interactKeys.keyboardKey == "")
        {
            savedControls.interactKeys.keyboardKey = playerControls.Gameplay.Interact.bindings[0].path;
        }
        if (savedControls.interactKeys.gamepadKey == "")
        {
            savedControls.interactKeys.gamepadKey = playerControls.Gameplay.Interact.bindings[1].path;
        }
        //menu
        if (savedControls.menuKeys.keyboardKey == "")
        {
            savedControls.menuKeys.keyboardKey = playerControls.Gameplay.OpenMenu.bindings[0].path;
        }
        if (savedControls.menuKeys.gamepadKey == "")
        {
            savedControls.menuKeys.gamepadKey = playerControls.Gameplay.OpenMenu.bindings[1].path;
        }
        //inventory
        if (savedControls.inventoryKeys.keyboardKey == "")
        {
            savedControls.inventoryKeys.keyboardKey = playerControls.Gameplay.OpenInventory.bindings[0].path;
        }
        if (savedControls.inventoryKeys.gamepadKey == "")
        {
            savedControls.inventoryKeys.gamepadKey = playerControls.Gameplay.OpenInventory.bindings[1].path;
        }
        //movement
        if (savedControls.moveLeftKeyboardKey == "")
        {
            savedControls.moveLeftKeyboardKey = playerControls.Gameplay.MoveLeftKeyboard.bindings[0].path;
        }
        if (savedControls.moveRightKeyboardKey == "")
        {
            savedControls.moveRightKeyboardKey = playerControls.Gameplay.MoveRightKeyboard.bindings[0].path;
        }
        if (savedControls.moveGamepadKey == "")
        {
            savedControls.moveGamepadKey = playerControls.Gameplay.MoveGamepad.bindings[0].path;
        }

        //Update controlsData from savedControls

        controlsData = savedControls;
        controlsChangedEvent.Invoke();

        //Update InputActions from controlsData

        // we could use if(...){} to avoid changing all bindings ... but who cares ?

        playerControls.Gameplay.Jump.ChangeBinding(0).WithPath(controlsData.jumpKeys.keyboardKey);
        playerControls.Gameplay.Jump.ChangeBinding(1).WithPath(controlsData.jumpKeys.gamepadKey);
        //playerControls.Gameplay.Jump.ChangeBinding(0).Erase();
        //playerControls.Gameplay.JumpKeyboard.AddBinding(jumpButtonKeyboardKey);

        playerControls.Gameplay.Attack.ChangeBinding(0).WithPath(controlsData.attackKeys.keyboardKey);
        playerControls.Gameplay.Attack.ChangeBinding(1).WithPath(controlsData.attackKeys.gamepadKey);

        playerControls.Gameplay.Interact.ChangeBinding(0).WithPath(controlsData.interactKeys.keyboardKey);
        playerControls.Gameplay.Interact.ChangeBinding(1).WithPath(controlsData.interactKeys.gamepadKey);

        playerControls.Gameplay.OpenMenu.ChangeBinding(0).WithPath(controlsData.menuKeys.keyboardKey);
        playerControls.Gameplay.OpenMenu.ChangeBinding(1).WithPath(controlsData.menuKeys.gamepadKey);

        playerControls.Gameplay.OpenInventory.ChangeBinding(0).WithPath(controlsData.inventoryKeys.keyboardKey);
        playerControls.Gameplay.OpenInventory.ChangeBinding(1).WithPath(controlsData.inventoryKeys.gamepadKey);

        playerControls.Gameplay.MoveLeftKeyboard.ChangeBinding(0).WithPath(controlsData.moveLeftKeyboardKey);
        playerControls.Gameplay.MoveRightKeyboard.ChangeBinding(0).WithPath(controlsData.moveRightKeyboardKey);
        playerControls.Gameplay.MoveGamepad.ChangeBinding(0).WithPath(controlsData.moveGamepadKey);

        //Update json

        string json = JsonUtility.ToJson(controlsData);
        Debug.Log(jsonPath);
        File.WriteAllText(jsonPath, json);
    }

    #endregion

    #region Rebinding Operations

    public InputAction GetCurrentAction(string def)
    {
        if (def == "jump")
        {
            return playerControls.Gameplay.Jump;
        }
        if (def == "attack")
        {
            return playerControls.Gameplay.Attack;
        }
        if (def == "interact")
        {
            return playerControls.Gameplay.Interact;
        }
        if (def == "menu")
        {
            return playerControls.Gameplay.OpenMenu;
        }
        if (def == "inventory")
        {
            return playerControls.Gameplay.OpenMenu;
        }
        if (def == "moveLeft")
        {
            return playerControls.Gameplay.MoveLeftKeyboard;
        }
        if (def == "moveRight")
        {
            return playerControls.Gameplay.MoveRightKeyboard;
        }
        if (def == "move")
        {
            return playerControls.Gameplay.MoveGamepad;
        }
        return null;
    }

    public void StartRebindingOperation(string actionDefinition)
    {
        string[] actionDef = actionDefinition.Split('-');

        playerControls.Disable();
        InputAction currentAction = GetCurrentAction(actionDef[0]);

        int bindingIndex = 0;
        if (actionDef[1] == "gamepad" && actionDefinition != "move-gamepad")
        {
            bindingIndex = 1;
        }

        operation = currentAction.PerformInteractiveRebinding(bindingIndex).WithControlsExcluding("Mouse").OnComplete(op => this.RebindingCompleted(actionDefinition)).Start();
    }

    public void RebindingCompleted(string actionDefinition)
    {
        string[] actionDef = actionDefinition.Split('-');
        InputAction currentAction = GetCurrentAction(actionDef[0]);
        int bindingIndex = 0;
        if (actionDef[1] == "gamepad" && actionDefinition != "move-gamepad")
        {
            bindingIndex = 1;
        }

        Debug.Log("Rebind completed : " + currentAction.bindings[bindingIndex].overridePath);
        operation.Dispose();
        playerControls.Enable();

        string[] overridePath = currentAction.bindings[bindingIndex].overridePath.Split('/');
        string controllerType = overridePath[0];
        if((controllerType == "<Keyboard>" && actionDef[1] != "keyboard") || (controllerType == "<Gamepad>" && actionDef[1] != "gamepad"))
        {
            string oldPath = currentAction.bindings[bindingIndex].path;
            currentAction.ChangeBinding(bindingIndex).Erase();
            currentAction.AddBinding(GetActionPath(actionDefinition));
            if(bindingIndex == 0 && currentAction.bindings.Count > 1)
            {
                string oldOtherPath = currentAction.bindings[0].path;
                currentAction.ChangeBinding(0).Erase();
                currentAction.AddBinding(oldOtherPath);
            }
            Debug.Log("WARNING :: Binding invalid : " + controllerType + " binding can't be used here. Use " + actionDef[1] + "instead");
        }

        Debug.Log(" hsdgshdg    :   " + currentAction.bindings[bindingIndex].path + " //// " + currentAction.bindings[bindingIndex].overridePath);
        SetActionPath(actionDefinition, currentAction.bindings[bindingIndex].effectivePath);
        controlsChangedEvent.Invoke();

        string json = JsonUtility.ToJson(controlsData);
        File.WriteAllText(jsonPath, json);
    }

    #endregion

    #region Callbacks

    public void Move(float value)
    {
        Debug.Log("Move : " + value);
    }

    public void Jump(string value)
    {
        Debug.Log("Jump !!! " + value);
    }

    public void Interact()
    {
        Debug.Log("Interact");
    }

    public void Attack()
    {
        Debug.Log("Attack !!! ");
    }

    public void OpenInventory()
    {
        Debug.Log("OpenInventory");
    }

    public void OpenMenu()
    {
        Debug.Log("OpenMenu");
    }

    #endregion
}
