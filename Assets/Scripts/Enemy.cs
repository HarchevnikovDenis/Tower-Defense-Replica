using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10.0f;
    [SerializeField] private int worth = 50;                      //цена за уничтожение врага
    [SerializeField] private GameObject deathEffect;              //эффект после уничтожения врага

    [SerializeField] private float startHealth = 100.0f;
    private float health;

    [Header("Unity Stuff")]
    public Image healthBar;

    public float speed { get; set; }                              //скорость передвижения

    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if(health <= 0.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        PlayerStats.Money += worth;

        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5.0f);

        WaveSpawner.EnemiesAlive--;

        Destroy(gameObject);
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1 - pct);
    }
}
