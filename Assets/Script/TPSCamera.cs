using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCam : MonoBehaviour
{
    public Transform player; // 플레이어 위치
    public Vector3 offset = new Vector3(0, 2, -5); // 기본 거리
    public float sensitivity = 2f; // 마우스 감도

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 잠금
    }

    void LateUpdate()
    {
        if (player == null)
            return;

        // 마우스 입력값 받기
        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, -30f, 60f); // 상하 회전 각도 제한

        // 플레이어를 중심으로 카메라 회전
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        transform.position = player.position + rotation * offset;
        transform.LookAt(player.position);
    }
}

