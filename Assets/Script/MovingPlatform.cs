using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA; // 시작 지점
    public Transform pointB; // 도착 지점
    public float speed = 2f; // 이동 속도

    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = pointB.position; // 처음엔 pointB로 이동
    }

    void Update()
    {
        // 현재 위치에서 목표 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // 목표 위치에 도달하면 방향을 바꿈
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = (targetPosition == pointA.position) ? pointB.position : pointA.position;
        }
    }
}

