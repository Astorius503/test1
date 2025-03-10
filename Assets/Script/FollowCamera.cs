using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public Vector3 offset = new Vector3(0, 3, -5); // 카메라 위치 오프셋
    public float smoothSpeed = 5f; // 부드러운 이동 속도

    void LateUpdate()
    {
        if (player == null)
            return;

        // 목표 위치 설정 (플레이어 위치 + 오프셋)
        Vector3 targetPosition = player.position + player.TransformDirection(offset);

        // 부드러운 이동 적용
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // 플레이어 방향을 따라 회전
        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}

