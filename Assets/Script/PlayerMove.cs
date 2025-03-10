using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // �̵� �ӵ�
    public float turnSpeed = 10f; // ȸ�� �ӵ�
    public float jumpForce = 8f; // �Ϲ� ���� ��
    public float highJumpForce = 15f; // �����뿡���� ���� ���� ��
    public float gravity = 9.81f; // �߷� ��
    public Transform cameraTransform; // TPS ī�޶�

    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // �÷��̾ �Ѿ����� �ʵ��� ȸ�� ����
    }

    void Update()
    {
        if (cameraTransform == null)
            return;

        // �Է°� �ޱ�
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // ī�޶� ���� �̵� ���� ����
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();
        moveDirection = (forward * vertical + right * horizontal).normalized;

        // �̵� ����
        Vector3 move = moveDirection * moveSpeed;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

        // ĳ���� ȸ�� (�����̴� ��������)
        if (moveDirection.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        // ���� ó��
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    // ���� ��Ҵ��� ����
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("JumpPad"))
        {
            isGrounded = true;
        }

        // ������(JumpPad) ������ ���� ����
        if (collision.gameObject.CompareTag("JumpPad"))
        {
            rb.AddForce(Vector3.up * highJumpForce, ForceMode.Impulse);
        }
    }
}




