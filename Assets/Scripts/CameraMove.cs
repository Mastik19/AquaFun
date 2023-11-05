using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;
    Vector3 offset;
    public static float yPos;
    void Start()
    {
        offset = transform.position - player.position;
        yPos = transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = player.position + offset;
        transform.position = new Vector3(transform.position.x, yPos ,newPos.z);
    }
}
