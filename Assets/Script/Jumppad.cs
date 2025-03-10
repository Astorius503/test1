using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 15f; // ���� �� ���� ����

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾ �����븦 ��Ҵ��� Ȯ��
        if (other.CompareTag("Player"))
        {
            Debug.Log("�����е� ����");

            Rigidbody playerRb = other.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                Debug.Log("�����е� Ȱ��ȭ");
                // ���� �ӵ��� ����Ͽ� Y������ �� ���ϱ�
                playerRb.velocity = new Vector3(playerRb.velocity.x, 0f, playerRb.velocity.z);
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            }
        }
    }
}

