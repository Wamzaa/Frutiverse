using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerCharacter : InteractableCharacter
{
    [Header("Speaker")]
    public string speech;

    public override void Interact()
    {
        if (!MainManager.Instance.uiManager.GetSpeechVisible())
        {
            MainManager.Instance.uiManager.UpdateSpeech(true, speech);
        }
        else
        {
            MainManager.Instance.uiManager.UpdateSpeech(false, "");
        }
    }
}
