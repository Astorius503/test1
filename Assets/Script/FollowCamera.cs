using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public Vector3 offset = new Vector3(0, 3, -5); // ī�޶� ��ġ ������
    public float smoothSpeed = 5f; // �ε巯�� �̵� �ӵ�

    void LateUpdate()
    {
        if (player == null)
            return;

        // ��ǥ ��ġ ���� (�÷��̾� ��ġ + ������)
        Vector3 targetPosition = player.position + player.TransformDirection(offset);

        // �ε巯�� �̵� ����
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // �÷��̾� ������ ���� ȸ��
        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}

