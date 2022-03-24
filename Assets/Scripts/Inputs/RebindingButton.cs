using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RebindingButton : MonoBehaviour
{
    public Text buttonText;

    public string actionPath;

    private void Start()
    {
        ControlsManager.Instance.AddListener(UpdateButtonText);
        UpdateButtonText();
    }

    public void UpdateButtonText()
    {
        string controlPath = ControlsManager.Instance.GetActionPath(actionPath);
        if(controlPath != "")
        {
            buttonText.text = controlPath;
        }
        else
        {
            Debug.LogError("Bad ActionPath --- no bindings");
        }
    }

    public void RebindingClicked()
    {
        ControlsManager.Instance.StartRebindingOperation(actionPath);
    }
}
