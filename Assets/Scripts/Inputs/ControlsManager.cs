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
        if(actionDefinition == "jump-keyboard")
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
        Debug.Log("INVOKE !!!");
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

    /*public void StartRebindingOperation()
    {
        playerControls.Disable();
        operation = playerControls.Gameplay.JumpKeyboard.PerformInteractiveRebinding().WithControlsExcluding("Mouse").OnComplete(op => this.RebindingCompleted()).Start();
    }

    public void RebindingCompleted()
    {
        Debug.Log("Rebind completed : " + playerControls.Gameplay.JumpKeyboard.bindings[0].overridePath);
        operation.Dispose();
        playerControls.Enable();
    }*/

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
