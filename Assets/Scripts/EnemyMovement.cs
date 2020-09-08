using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;         //индекс контрольной точки
    private Enemy enemy;

    private void Start()
    {
        target = Waypoint.points[0];        //изначально враг двигается к 1ой точке
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.6f)
        {
            GetNextWayPoint();
        }

        enemy.speed = enemy.startSpeed;
    }
    private void GetNextWayPoint()
    {
        if (wavepointIndex >= Waypoint.points.Length - 1)
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = Waypoint.points[wavepointIndex];
    }

    private void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
