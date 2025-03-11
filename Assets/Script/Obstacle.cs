using UnityEngine;
using System.Collections.Generic;

public class TransparentObstacle : MonoBehaviour
{
    public Transform player;  // �÷��̾��� Transform
    public LayerMask obstacleLayer;  // ��ֹ� ���̾� ����

    private List<Renderer> hiddenObjects = new List<Renderer>();

    void Update()
    {
        HandleTransparency();
    }

    void HandleTransparency()
    {
        // ������ �����ϰ� ���� ��ü ����
        ResetObjects();

        Vector3 direction = (player.position - transform.position).normalized; // ī�޶� �� �÷��̾� ����
        float distance = Vector3.Distance(transform.position, player.position); // ī�޶�� �÷��̾� ���� �Ÿ�

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
            renderer.material.color = new Color(color.r, color.g, color.b, 0.3f); // ������ ����
        }
    }

    void ResetObjects()
    {
        foreach (Renderer objRenderer in hiddenObjects)
        {
            if (objRenderer != null && objRenderer.material.HasProperty("_Color"))
            {
                Color color = objRenderer.material.color;
                objRenderer.material.color = new Color(color.r, color.g, color.b, 1f); // ���� ���� ����
            }
        }
        hiddenObjects.Clear();
    }
}

