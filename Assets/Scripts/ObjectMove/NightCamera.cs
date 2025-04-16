using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightCamera : MonoBehaviour
{
    public Transform target; // 추적할 대상 (플레이어)
    public float distance = 4.0f; // 카메라와 플레이어 사이의 거리
    public float height = 1.5f; // 카메라 높이 (플레이어 위쪽)
    public float rotationSpeed = 5.0f; // 마우스로 카메라 회전 속도
    public float smoothSpeed = 10.0f; // 카메라 이동 부드럽게 처리할 속도
    public float collisionBuffer = 0.2f; // 카메라와 장애물 간의 거리 버퍼 (충돌 방지)

    private Vector3 currentVelocity = Vector3.zero; // 카메라 위치 부드럽게 처리
    private float currentRotationAngle = 0.0f; // 카메라 회전 각도

    void Update()
    {
        // 마우스 입력에 따른 카메라 회전
        currentRotationAngle += Input.GetAxis("Mouse X") * rotationSpeed;
        float desiredHeight = target.position.y + height;

        // 카메라 위치 계산
        Vector3 desiredPosition = target.position - (Quaternion.Euler(0, currentRotationAngle, 0) * Vector3.forward * distance);
        desiredPosition.y = desiredHeight;

        // 카메라가 장애물에 막히지 않도록 충돌 검사
        RaycastHit hit;
        if (Physics.Raycast(target.position, desiredPosition - target.position, out hit, distance))
        {
            desiredPosition = hit.point + hit.normal * collisionBuffer; // 충돌 시 카메라 위치 조정
        }

        // 카메라 이동을 부드럽게 처리
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed * Time.deltaTime);

        // 카메라는 항상 플레이어를 바라보게
        transform.LookAt(target);
    }
}

