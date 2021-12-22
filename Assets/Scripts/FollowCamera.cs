using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject player;

    private void Update()
    {
        transform.position = player.transform.position;
    }
}
