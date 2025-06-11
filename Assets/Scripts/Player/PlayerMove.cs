using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float moveSpeedBonusPercent = 0f;
    public Camera mainCamera;

    private Rigidbody rb;
    private Vector3 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // �Է� ó��
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        movement = new Vector3(h, 0, v).normalized;

        // ���콺 ���� ȸ�� ó��
        RotateToMouse();
    }

    void FixedUpdate()
    {
        // �̵�
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void RotateToMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            Vector3 lookDir = hitPoint - transform.position;
            lookDir.y = 0; // Y�� ȸ�� ����

            if (lookDir != Vector3.zero)
            {
                Quaternion rot = Quaternion.LookRotation(lookDir);
                rb.MoveRotation(rot);
            }
        }
    }

    public float GetCurrentMoveSpeed()
    {
        return moveSpeed * (1f + moveSpeedBonusPercent / 100f);
    }
}
