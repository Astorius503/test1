using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA; // ���� ����
    public Transform pointB; // ���� ����
    public float speed = 2f; // �̵� �ӵ�

    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = pointB.position; // ó���� pointB�� �̵�
    }

    void Update()
    {
        // ���� ��ġ���� ��ǥ ��ġ�� �̵�
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // ��ǥ ��ġ�� �����ϸ� ������ �ٲ�
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = (targetPosition == pointA.position) ? pointB.position : pointA.position;
        }
    }
}

