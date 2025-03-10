using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // �̵� �ӵ�
    public float turnSpeed = 10f; // ȸ�� �ӵ�
    public float jumpForce = 8f; // ���� ��
    public float gravity = 8f; // �߷� ��
    public Transform cameraTransform; // ī�޶� Transform (TPS ī�޶� ����)

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

        // �Է°� �ޱ�
        float horizontal = Input.GetAxis("Horizontal"); // A, D �̵�
        float vertical = Input.GetAxis("Vertical"); // W, S �̵�

        // ī�޶��� �յ�/�¿� ���� ���
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Y�� ���� ���� (���� �̵���)
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // �̵� ���� ��� (ī�޶� ����)
        moveDirection = (forward * vertical + right * horizontal).normalized;

        // �÷��̾� ȸ�� (�����̴� ��������)
        if (moveDirection.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        // �߷� �� ���� ó��
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

        // ���� �̵� ����
        Vector3 move = moveDirection * moveSpeed + Vector3.up * verticalVelocity;
        characterController.Move(move * Time.deltaTime);
    }
}



