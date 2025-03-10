using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // �̵� �ӵ�
    public float turnSpeed = 700f; // ȸ�� �ӵ�
    public float jumpForce = 8f; // ���� ��
    public float gravity = 9.81f; // �߷� ��

    private CharacterController characterController;
    private Vector3 moveDirection;
    private float verticalVelocity;

    void Start()
    {
        // CharacterController ������Ʈ ��������
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // �Է°��� ������� ĳ���� �̵� ���� ����
        float horizontal = Input.GetAxis("Horizontal"); // A, D �Ǵ� ��, �� ȭ��ǥ
        float vertical = Input.GetAxis("Vertical"); // W, S �Ǵ� ��, �� ȭ��ǥ

        // �̵� ���� ���
        moveDirection = new Vector3(horizontal, 0f, vertical);
        moveDirection.Normalize(); // ���� ���� ����ȭ

        // �̵� ó��
        if (moveDirection.magnitude >= 0.1f)
        {
            // ĳ������ �̵� ������ ȸ��
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        // �߷� �� ���� ó��
        if (characterController.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetButtonDown("Jump")) // �����̽��� ����
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        // ���� �̵� ���� ���
        Vector3 move = transform.forward * moveSpeed * Time.deltaTime;
        move.y = verticalVelocity * Time.deltaTime;
        characterController.Move(move);
    }
}


