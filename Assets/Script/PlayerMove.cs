using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    public float turnSpeed = 10f; // 회전 속도
    public float jumpForce = 8f; // 일반 점프 힘
    public float highJumpForce = 15f; // 점프대에서의 높이 점프 힘
    public float gravity = 9.81f; // 중력 값
    public Transform cameraTransform; // TPS 카메라

    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // 플레이어가 넘어지지 않도록 회전 고정
    }

    void Update()
    {
        if (cameraTransform == null)
            return;

        // 입력값 받기
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 카메라 기준 이동 방향 설정
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();
        moveDirection = (forward * vertical + right * horizontal).normalized;

        // 이동 적용
        Vector3 move = moveDirection * moveSpeed;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

        // 캐릭터 회전 (움직이는 방향으로)
        if (moveDirection.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        // 점프 처리
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    // 땅에 닿았는지 감지
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("JumpPad"))
        {
            isGrounded = true;
        }

        // 점프대(JumpPad) 밟으면 강한 점프
        if (collision.gameObject.CompareTag("JumpPad"))
        {
            rb.AddForce(Vector3.up * highJumpForce, ForceMode.Impulse);
        }
    }
}




