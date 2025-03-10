using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    public float turnSpeed = 10f; // 회전 속도
    public float jumpForce = 8f; // 점프 힘
    public float gravity = 8f; // 중력 값
    public Transform cameraTransform; // 카메라 Transform (TPS 카메라 연결)

    private CharacterController characterController;
    private Vector3 moveDirection;
    private float verticalVelocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (cameraTransform == null)
            return;

        // 입력값 받기
        float horizontal = Input.GetAxis("Horizontal"); // A, D 이동
        float vertical = Input.GetAxis("Vertical"); // W, S 이동

        // 카메라의 앞뒤/좌우 벡터 계산
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Y축 방향 제거 (수평 이동만)
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // 이동 방향 계산 (카메라 기준)
        moveDirection = (forward * vertical + right * horizontal).normalized;

        // 플레이어 회전 (움직이는 방향으로)
        if (moveDirection.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        // 중력 및 점프 처리
        if (characterController.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        // 최종 이동 적용
        Vector3 move = moveDirection * moveSpeed + Vector3.up * verticalVelocity;
        characterController.Move(move * Time.deltaTime);
    }
}



