using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using R3.Triggers;

public class BulletController : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _lifetime; 

    private void Start()
    {
        Observable.Timer(TimeSpan.FromSeconds(_lifetime))
            .Subscribe(_ =>
            {
                Destroy(gameObject);
            })
            .AddTo(this);
        
        var trigger = GetComponent<ObservableCollision2DTrigger>();
        trigger.OnTriggerEnter2DAsObservable()
            .Where(collider => collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("Wall"))
            .Subscribe(collider =>
            {
                Debug.Log("Hit");
                CharacterHealth health = collider.GetComponent<CharacterHealth>();
                if (health != null)
                {
                    
                    health.TakeDamage(_damage);
                }

                // Уничтожаем пулю
                Destroy(gameObject);
            })
            .AddTo(this);
    }
}
