using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove2 : MonoBehaviour
{
    public float moveSpeed = 5f;        // 이동 속도
    public float rotationSpeed = 10f;   // 회전 보간 속도

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // GetAxisRaw 사용: 키 입력이 바로 -1, 0, 1로 반영됨
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // 입력값으로 이동 방향 계산 (y축은 제외)
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput);

        // 입력값의 크기가 충분한 경우에만 이동 및 회전 처리
        if (moveDirection.sqrMagnitude > 0.1f)
        {
            // 이동 방향을 정규화합니다.
            moveDirection.Normalize();

            // 이동 방향으로 바라보는 회전값 계산 (Quaternion.LookRotation은 주어진 벡터가 바라보는 방향의 회전값을 만듭니다)
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            // 현재 회전값에서 targetRotation까지 부드럽게 보간하여 회전합니다.
            Quaternion newRotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(newRotation);

            // 이동 처리: 이동 벡터에 이동속도와 FixedDeltaTime을 곱해 위치 업데이트
            Vector3 movement = moveDirection * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
        else
        {
            // 입력이 없으면 바로 멈추도록 물리 속도를 0으로 설정합니다.
            rb.velocity = Vector3.zero;
        }
    }
}
