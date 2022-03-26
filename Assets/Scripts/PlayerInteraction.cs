using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject weapon;

    private bool isAttacking;
    private List<GameObject> interactableObjects;

    private void Start()
    {
        isAttacking = false;
        weapon.SetActive(false);
        interactableObjects = new List<GameObject>();
    }

    public void AddInteractableObject(GameObject obj)
    {
        interactableObjects.Add(obj);
    }

    public void RemoveInteractableObject(GameObject obj)
    {
        if (interactableObjects.Contains(obj))
        {
            interactableObjects.Remove(obj);
        }
    }

    public void InteractFromInput()
    {
        foreach(GameObject obj in interactableObjects)
        {
            obj.GetComponent<InteractableCharacter>().Interact();
        }
    }

    public void AttackFromInput()
    {
        if (!isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        weapon.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        isAttacking = false;
        weapon.SetActive(false);
    }

}
