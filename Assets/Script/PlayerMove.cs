using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    public float turnSpeed = 700f; // 회전 속도
    public float jumpForce = 8f; // 점프 힘
    public float gravity = 9.81f; // 중력 값

    private CharacterController characterController;
    private Vector3 moveDirection;
    private float verticalVelocity;

    void Start()
    {
        // CharacterController 컴포넌트 가져오기
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 입력값을 기반으로 캐릭터 이동 방향 설정
        float horizontal = Input.GetAxis("Horizontal"); // A, D 또는 좌, 우 화살표
        float vertical = Input.GetAxis("Vertical"); // W, S 또는 상, 하 화살표

        // 이동 방향 계산
        moveDirection = new Vector3(horizontal, 0f, vertical);
        moveDirection.Normalize(); // 방향 벡터 정규화

        // 이동 처리
        if (moveDirection.magnitude >= 0.1f)
        {
            // 캐릭터의 이동 방향을 회전
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        // 중력 및 점프 처리
        if (characterController.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetButtonDown("Jump")) // 스페이스바 점프
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        // 최종 이동 벡터 계산
        Vector3 move = transform.forward * moveSpeed * Time.deltaTime;
        move.y = verticalVelocity * Time.deltaTime;
        characterController.Move(move);
    }
}


