using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;           //цель в которую будет стрелять турель
    private Enemy targetEnemy;

    [Header("General")]
    [SerializeField] private float range = 15.0f;           //радиус поиска врага

    [Header("Use bullets(default)")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 1.0f;         //кол-во выстрелов в секунду
    private float fireCountDown = 0.0f;                     //счетчик времени

    [Header("Use laser")]
    [SerializeField] private bool useLaser = false;

    [SerializeField] private int damageOverTime = 30;
    [SerializeField] private float amount = 0.5f;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private ParticleSystem impactEffect;

    [Header("Unity Setup Fields")]
    [SerializeField] private string enemyTag = "Enemy";
    [SerializeField] private float turnSpeed = 10.0f;

    [SerializeField] private Transform firePoint;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);        //каждые 0.5 сек турель ищет врага
    }

    private void UpdateTarget()                             //ф-ия ищет ближайшего врага
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);     //поиск всех объектов врага
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    private void Update()
    {
        if (target == null)
        {
            if(fireCountDown > 0.0f)
            {
                fireCountDown -= Time.deltaTime;
            }

            if(useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                }
            }
            return;
        }

        LockOnTarget();

        if(useLaser)                                    //Выстрел из Лазера
        {
            Laser();
        }
        else
        {                                               //Выстрел из турели или пушки
            if (fireCountDown <= 0.0f)
            {
                Shoot();
                fireCountDown = 1.0f / fireRate;
            }

            fireCountDown -= Time.deltaTime;
        }
    }

    private void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;                 //турель следит за врагом, которого обнаружила
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0.0f, rotation.y, 0.0f);
    }

    private void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(amount);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position + dir.normalized * 0.5f;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    private void OnDrawGizmosSelected()                     //Debug
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
