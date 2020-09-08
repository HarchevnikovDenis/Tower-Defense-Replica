using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 70.0f;               //скорость пули
    [SerializeField] private GameObject effectImpact;           //эффект попадания пули
    [SerializeField] private float explotionRaius = 0.0f;       //Радиус взрыва
    [SerializeField] private int damage = 50;

    private Transform target;                                   //цель пули
    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        transform.LookAt(target);
    }

    private void HitTarget()
    {
        GameObject effect = Instantiate(effectImpact, transform.position, transform.rotation);
        Destroy(effect, 5.0f);

        if(explotionRaius > 0.0f)           //Если летит ракета
        {
            Explode();
        }
        else
        {                                   //Если летит обычная пуля
            Damage(target);
        }

        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explotionRaius);
        foreach (Collider collider in colliders)
        {
            if(collider.CompareTag("Enemy"))
            {
                Damage(collider.transform);
            }
        }
    }

    private void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if(e != null)
            e.TakeDamage(damage);
    }
}
