using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject player;
    public Vector3 offsetGoal;
    public float offsetGap;

    private void Update()
    {
        Vector3 playerVelocity = player.GetComponent<Rigidbody2D>().velocity;
        playerVelocity = playerVelocity.normalized;

        Vector3 currentOffsetVec = player.transform.position + offsetGoal - this.transform.position;
        currentOffsetVec.z = 0.0f;
        float currentOffset = currentOffsetVec.magnitude;

        if(Vector3.Dot(currentOffsetVec, playerVelocity) > 0 && currentOffset > offsetGap)
        {
            this.transform.position = player.transform.position + offsetGoal - offsetGap * currentOffsetVec.normalized;
            Debug.Log("aaa");
        }
    }
}
