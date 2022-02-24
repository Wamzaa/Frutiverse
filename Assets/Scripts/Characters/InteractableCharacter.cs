using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCharacter : MonoBehaviour
{
    public float interactDistance;
    public GameObject interactIndication;

    private void Update()
    {
        if((MainManager.Instance.player.transform.position - this.transform.position).magnitude < interactDistance)
        {
            interactIndication.SetActive(true);
            if (Input.GetKeyDown(ControlsDictionary.Instance.interactButtonKey))
            {
                Interact();
            }
        }
        else
        {
            interactIndication.SetActive(false);
        }
    }

    public virtual void Interact()
    {

    }
}
