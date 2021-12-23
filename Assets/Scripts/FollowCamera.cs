using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;

    private Vector3 camVelocity = Vector3.zero;

    private void Update()
    {
        Vector3 targetPosition = player.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref camVelocity, 0.1f);
    }
}
