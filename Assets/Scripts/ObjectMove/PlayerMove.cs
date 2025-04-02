using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public Transform cameraTarget; 

    public Transform rightArm;
    public Transform leftArm;
    public Transform rightLeg;
    public Transform leftLeg;

    private CharacterController controller;

    private float swingSpeed = 5f;
    private float swingAmount = 30f;
    private float timer = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal") * 0.7f;
        float v = Input.GetAxis("Vertical");
        Vector3 input = new Vector3(h, 0, v).normalized;

        if (input.magnitude >= 0.1f)
        {
            //카메라 기준 방향 계산
            Vector3 camForward = cameraTarget.forward;
            Vector3 camRight = cameraTarget.right;
            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDir = (camForward * v + camRight * h).normalized;

            // 부드럽게 회전
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            // 이동
            controller.Move(moveDir * moveSpeed * Time.deltaTime);

            // 팔 다리 흔들기
            timer += Time.deltaTime * swingSpeed;
            float swing = Mathf.Sin(timer) * swingAmount;

            rightArm.localRotation = Quaternion.Euler(swing, 0, 0);
            leftArm.localRotation = Quaternion.Euler(-swing, 0, 0);
            rightLeg.localRotation = Quaternion.Euler(-swing, 0, 0);
            leftLeg.localRotation = Quaternion.Euler(swing, 0, 0);
        }
        else
        {
            // 정지자세
            rightArm.localRotation = Quaternion.Lerp(rightArm.localRotation, Quaternion.identity, Time.deltaTime * 5f);
            leftArm.localRotation = Quaternion.Lerp(leftArm.localRotation, Quaternion.identity, Time.deltaTime * 5f);
            rightLeg.localRotation = Quaternion.Lerp(rightLeg.localRotation, Quaternion.identity, Time.deltaTime * 5f);
            leftLeg.localRotation = Quaternion.Lerp(leftLeg.localRotation, Quaternion.identity, Time.deltaTime * 5f);
            timer = 0f;
        }
    }
}
