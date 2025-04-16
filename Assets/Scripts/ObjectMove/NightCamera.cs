using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightCamera : MonoBehaviour
{
    public Transform target; // ������ ��� (�÷��̾�)
    public float distance = 4.0f; // ī�޶�� �÷��̾� ������ �Ÿ�
    public float height = 1.5f; // ī�޶� ���� (�÷��̾� ����)
    public float rotationSpeed = 5.0f; // ���콺�� ī�޶� ȸ�� �ӵ�
    public float smoothSpeed = 10.0f; // ī�޶� �̵� �ε巴�� ó���� �ӵ�
    public float collisionBuffer = 0.2f; // ī�޶�� ��ֹ� ���� �Ÿ� ���� (�浹 ����)

    private Vector3 currentVelocity = Vector3.zero; // ī�޶� ��ġ �ε巴�� ó��
    private float currentRotationAngle = 0.0f; // ī�޶� ȸ�� ����

    void Update()
    {
        // ���콺 �Է¿� ���� ī�޶� ȸ��
        currentRotationAngle += Input.GetAxis("Mouse X") * rotationSpeed;
        float desiredHeight = target.position.y + height;

        // ī�޶� ��ġ ���
        Vector3 desiredPosition = target.position - (Quaternion.Euler(0, currentRotationAngle, 0) * Vector3.forward * distance);
        desiredPosition.y = desiredHeight;

        // ī�޶� ��ֹ��� ������ �ʵ��� �浹 �˻�
        RaycastHit hit;
        if (Physics.Raycast(target.position, desiredPosition - target.position, out hit, distance))
        {
            desiredPosition = hit.point + hit.normal * collisionBuffer; // �浹 �� ī�޶� ��ġ ����
        }

        // ī�޶� �̵��� �ε巴�� ó��
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed * Time.deltaTime);

        // ī�޶�� �׻� �÷��̾ �ٶ󺸰�
        transform.LookAt(target);
    }
}

