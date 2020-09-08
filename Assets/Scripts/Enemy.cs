using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10.0f;
    [HideInInspector]
    public float speed;                     //скорость передвижения
    public int worth = 50;                  //цена за уничтожение врага
    public GameObject deathEffect;          //эффект после уничтожения врага

    public float startHealth = 100.0f;
    private float health;

    [Header("Unity Stuff")]
    public Image healthBar;

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
