using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ControlsDictionary : MonoBehaviour
{
    public static ControlsDictionary Instance;

    public string jumpButtonKey;
    public string attackButtonKey;
    public string menuButtonKey;
    public string inventoryButtonKey;
    public string interactButtonKey;

    private string jsonPath;

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
    }

    public void Init()
    {
        jsonPath = Application.persistentDataPath + "/controls.json";

        InitKeys();
        FillKeysFromJson();
    }

    public void InitKeys()
    {
        jumpButtonKey = "space";
        attackButtonKey = "f";
        menuButtonKey = "p";
        inventoryButtonKey = "i";
        interactButtonKey = "e";
    }

    public void FillKeysFromJson()
    {
        if (File.Exists(jsonPath))
        {
            string file = File.ReadAllText(jsonPath);
            ControlsData controls = JsonUtility.FromJson<ControlsData>(file);

            if(controls.jumpButtonKey != null)
            {
                jumpButtonKey = controls.jumpButtonKey;
            }

            if (controls.attackButtonKey != null)
            {
                attackButtonKey = controls.attackButtonKey;
            }

            if (controls.menuButtonKey != null)
            {
                menuButtonKey = controls.menuButtonKey;
            }

            if (controls.inventoryButtonKey != null)
            {
                inventoryButtonKey = controls.inventoryButtonKey;
            }
            
            if (controls.interactButtonKey != null)
            {
                interactButtonKey = controls.interactButtonKey;
            }
        }

        ControlsData newControls = new ControlsData();
        newControls.jumpButtonKey = jumpButtonKey;
        newControls.attackButtonKey = attackButtonKey;
        newControls.menuButtonKey = menuButtonKey;
        newControls.inventoryButtonKey = inventoryButtonKey;
        newControls.interactButtonKey = interactButtonKey;

        string json = JsonUtility.ToJson(newControls);
        Debug.Log(jsonPath);
        File.WriteAllText(jsonPath, json);
    }

}
