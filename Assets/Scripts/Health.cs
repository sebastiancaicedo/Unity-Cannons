using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public float health = 100;
    public float currentHealth;

    [SerializeField]
    GameObject healthCanvas;
    [SerializeField]
    Slider healthSlider;
    [SerializeField]
    ParticleSystem deathFX;

    private void Awake()
    {
        healthCanvas.SetActive(true);
    }

    private void Start()
    {
        currentHealth = health;
        healthSlider.value = 1;
    }

    public void TakeDamage(float damage)
    {
        if (GameManager.Instance.GameFinished) return;

        currentHealth -= damage;

        healthSlider.value = currentHealth / health;

        if(currentHealth <= 0)
        {
            print(name + " muerto");
            GameManager.Instance.FinishGameImDead(GetComponent<CannonController>());
            deathFX.transform.SetParent(null);
            deathFX.Play();
            Destroy(gameObject);
        }
    }
}
