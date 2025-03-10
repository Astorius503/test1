using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public Vector3 offset = new Vector3(0, 2, -5); // ī�޶� ������
    public float mouseSensitivity = 3f; // ���콺 ����
    public float smoothSpeed = 5f; // �ε巯�� �̵� �ӵ�
    public float minY = -40f, maxY = 80f; // X�� ȸ�� ����

    private float yaw = 0f; // �¿� ȸ�� (Y��)
    private float pitch = 0f; // ���� ȸ�� (X��)

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (player == null)
            return;

        // ���콺 �Է� �ޱ�
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // ȸ���� ������Ʈ
        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minY, maxY); // ���Ʒ� ȸ�� ����

        // ī�޶� ȸ�� ����
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 targetPosition = player.position + rotation * offset;

        // �ε巯�� �̵� ����
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(player.position + Vector3.up * 1.5f);

        // �÷��̾ ī�޶� ������ ���� ȸ��
        player.rotation = Quaternion.Euler(0, yaw, 0);
    }
}


