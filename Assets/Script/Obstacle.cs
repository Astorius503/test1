using UnityEngine;
using System.Collections.Generic;

public class TransparentObstacle : MonoBehaviour
{
    public Transform player;  // 플레이어의 Transform
    public LayerMask obstacleLayer;  // 장애물 레이어 지정

    private List<Renderer> hiddenObjects = new List<Renderer>();

    void Update()
    {
        HandleTransparency();
    }

    void HandleTransparency()
    {
        // 기존에 투명하게 만든 객체 복구
        ResetObjects();

        Vector3 direction = (player.position - transform.position).normalized; // 카메라 → 플레이어 방향
        float distance = Vector3.Distance(transform.position, player.position); // 카메라와 플레이어 사이 거리

        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, distance, obstacleLayer);

        foreach (RaycastHit hit in hits)
        {
            Renderer objRenderer = hit.collider.GetComponent<Renderer>();

            if (objRenderer != null)
            {
                SetTransparent(objRenderer);
                hiddenObjects.Add(objRenderer);
            }
        }
    }

    void SetTransparent(Renderer renderer)
    {
        if (renderer.material.HasProperty("_Color"))
        {
            Color color = renderer.material.color;
            renderer.material.color = new Color(color.r, color.g, color.b, 0.3f); // 반투명 설정
        }
    }

    void ResetObjects()
    {
        foreach (Renderer objRenderer in hiddenObjects)
        {
            if (objRenderer != null && objRenderer.material.HasProperty("_Color"))
            {
                Color color = objRenderer.material.color;
                objRenderer.material.color = new Color(color.r, color.g, color.b, 1f); // 원래 색상 복구
            }
        }
        hiddenObjects.Clear();
    }
}

