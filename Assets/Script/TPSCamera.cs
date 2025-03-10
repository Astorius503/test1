using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCam : MonoBehaviour
{
    public Transform player; // �÷��̾� ��ġ
    public Vector3 offset = new Vector3(0, 2, -5); // �⺻ �Ÿ�
    public float sensitivity = 2f; // ���콺 ����

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ���
    }

    void LateUpdate()
    {
        if (player == null)
            return;

        // ���콺 �Է°� �ޱ�
        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, -30f, 60f); // ���� ȸ�� ���� ����

        // �÷��̾ �߽����� ī�޶� ȸ��
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        transform.position = player.position + rotation * offset;
        transform.LookAt(player.position);
    }
}

