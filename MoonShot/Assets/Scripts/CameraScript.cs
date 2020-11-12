using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform chase;
    public Transform player;

    void LateUpdate()
    {
        transform.position = chase.position;
        transform.LookAt(player.position);
    }


}
