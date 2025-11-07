using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _shootRadius;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private InventoryManager _inventory;
    [SerializeField] private PickableItem _item;

    
    public void Shoot()
    {
        Observable.Return(Unit.Default)
            .Select(_ => FindClosestEnemy())
            .Where(enemyPos => enemyPos != null)  
            .Subscribe(enemyPos =>
            {
                SpawnBullet(enemyPos);
            })
            .AddTo(this);
    }
    
    private Transform FindClosestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _shootRadius, _enemyLayer);

        Transform closest = null;
        float minDistance = float.MaxValue;

        foreach (var hit in hits)
        {
            float distance = Vector2.Distance(transform.position, hit.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = hit.transform;
            }
        }

        return closest;
    }
    
    private void SpawnBullet(Transform target)
    {
        if (_inventory.RemoveItem(_item))
        {
            GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
            
            Vector2 direction = (target.position - transform.position).normalized;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * _bulletSpeed;
            }
        }
    }
}
