using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCharacter : MonoBehaviour
{
    public float interactDistance;
    public GameObject interactIndication;

    private void Start()
    {
        CircleCollider2D collider = this.gameObject.AddComponent<CircleCollider2D>();
        collider.offset = Vector2.zero;
        collider.radius = interactDistance;
        collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactIndication.SetActive(true);
            MainManager.Instance.player.GetComponent<PlayerInteraction>().AddInteractableObject(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactIndication.SetActive(false);
            MainManager.Instance.player.GetComponent<PlayerInteraction>().RemoveInteractableObject(this.gameObject);
        }
    }

    public virtual void Interact()
    {

    }
}
