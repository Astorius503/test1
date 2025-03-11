
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public Image healthBarFill; // 체력바 UI 이미지
    private float maxHealth = 100f; // 최대 체력
    private float currentHealth; // 현재 체력
    public float damageAmount = 10f; // 한 번 받을 데미지량
    public float lerpSpeed = 5f; // 체력바 애니메이션 속도

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar(); // 초기 체력바 설정
    }

    void Update()
    {
        // P 키를 누르면 데미지를 받음
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

        healthBarFill.fillAmount = targetFill; // 최종 값 보정
    }
}


