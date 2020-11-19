using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform chase;
    public Transform player;
    private float camHeightOffset = 5;

    void LateUpdate()
    {
        transform.position = chase.position;
        Vector3 lookPosition = new Vector3(player.position.x, player.position.y + camHeightOffset, player.position.z);
        transform.LookAt(lookPosition);
    }


}
