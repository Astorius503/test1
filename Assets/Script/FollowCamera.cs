using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public Vector3 offset = new Vector3(0, 2, -5); // 카메라 오프셋
    public float mouseSensitivity = 3f; // 마우스 감도
    public float smoothSpeed = 5f; // 부드러운 이동 속도
    public float minY = -40f, maxY = 80f; // X축 회전 제한

    private float yaw = 0f; // 좌우 회전 (Y축)
    private float pitch = 0f; // 상하 회전 (X축)

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (player == null)
            return;

        // 마우스 입력 받기
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // 회전값 업데이트
        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minY, maxY); // 위아래 회전 제한

        // 카메라 회전 적용
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 targetPosition = player.position + rotation * offset;

        // 부드러운 이동 적용
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(player.position + Vector3.up * 1.5f);

        // 플레이어도 카메라 방향을 따라 회전
        player.rotation = Quaternion.Euler(0, yaw, 0);
    }
}


