
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public Image healthBarFill; // ü�¹� UI �̹���
    private float maxHealth = 100f; // �ִ� ü��
    private float currentHealth; // ���� ü��
    public float damageAmount = 10f; // �� �� ���� ��������
    public float lerpSpeed = 5f; // ü�¹� �ִϸ��̼� �ӵ�

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar(); // �ʱ� ü�¹� ����
    }

    void Update()
    {
        // P Ű�� ������ �������� ����
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(damageAmount);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        StartCoroutine(AnimateHealthBar());
    }

    private void UpdateHealthBar()
    {
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }

    private IEnumerator AnimateHealthBar()
    {
        float startFill = healthBarFill.fillAmount;
        float targetFill = currentHealth / maxHealth;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * lerpSpeed;
            healthBarFill.fillAmount = Mathf.Lerp(startFill, targetFill, elapsedTime);
            yield return null;
        }

        healthBarFill.fillAmount = targetFill; // ���� �� ����
    }
}


