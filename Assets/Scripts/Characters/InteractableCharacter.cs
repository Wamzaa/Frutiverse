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
            MainManager.Instance.player.GetComponent<PlayerInteraction>().AddInteractableObject(this.gameObject);
        }
        else
        {
            interactIndication.SetActive(false);
            MainManager.Instance.player.GetComponent<PlayerInteraction>().RemoveInteractableObject(this.gameObject);
        }
    }

    public virtual void Interact()
    {

    }
}
