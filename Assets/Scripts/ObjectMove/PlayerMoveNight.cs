using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveNight : MonoBehaviour
{
    public float moveSpeed = 5f;        // �̵� �ӵ�
    public float slowDownFactor = 0.4f;
    public float rotationSpeed = 10f;   // ȸ�� ���� �ӵ�

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // GetAxisRaw ���: Ű �Է��� �ٷ� -1, 0, 1�� �ݿ���
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        float currentMoveSpeed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftControl))
        {
            currentMoveSpeed *= slowDownFactor;
        }

        // �Է°����� �̵� ���� ��� (y���� ����)
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput);

        // �Է°��� ũ�Ⱑ ����� ��쿡�� �̵� �� ȸ�� ó��
        if (moveDirection.sqrMagnitude > 0.1f)
        {
            // �̵� ������ ����ȭ�մϴ�.
            moveDirection.Normalize();

            // �̵� �������� �ٶ󺸴� ȸ���� ��� (Quaternion.LookRotation�� �־��� ���Ͱ� �ٶ󺸴� ������ ȸ������ ����ϴ�)
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            // ���� ȸ�������� targetRotation���� �ε巴�� �����Ͽ� ȸ���մϴ�.
            Quaternion newRotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(newRotation);

            // �̵� ó��: �̵� ���Ϳ� �̵��ӵ��� FixedDeltaTime�� ���� ��ġ ������Ʈ
            Vector3 movement = moveDirection * currentMoveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
        else
        {
            // �Է��� ������ �ٷ� ���ߵ��� ���� �ӵ��� 0���� �����մϴ�.
            rb.velocity = Vector3.zero;
        }
    }
}
