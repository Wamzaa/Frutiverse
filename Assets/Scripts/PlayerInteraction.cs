using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject weapon;

    private bool isAttacking;

    private void Start()
    {
        isAttacking = false;
        weapon.SetActive(false);
    }

    private void Update()
    {
        bool attackInput = Input.GetKeyDown(ControlsDictionary.Instance.attackButtonKey);
        if(!isAttacking && attackInput)
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
