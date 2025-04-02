using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 3, -6);
    public float mouseSensitivity = 3f;
    private float yaw = 0f;

    void LateUpdate()
    {
        if (target == null) return;

        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        Quaternion rotation = Quaternion.Euler(0f, yaw, 0f);

        Vector3 desiredPosition = target.position + rotation * offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 10f);

        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}