using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 15f; // 점프 힘 조절 가능

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 점프대를 밟았는지 확인
        if (other.CompareTag("Player"))
        {
            Debug.Log("점프패드 접촉");

            Rigidbody playerRb = other.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                Debug.Log("점프패드 활성화");
                // 현재 속도를 고려하여 Y축으로 힘 가하기
                playerRb.velocity = new Vector3(playerRb.velocity.x, 0f, playerRb.velocity.z);
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            }
        }
    }
}

