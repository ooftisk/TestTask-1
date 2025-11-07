using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using R3.Triggers;
using Unity.VisualScripting;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private PickableItem item;
    [SerializeField] private int amount;

    private void Start()
    {
        var pickup = GetComponent<ObservableCollision2DTrigger>();
        pickup.OnTriggerEnter2DAsObservable().Where(collider => collider.CompareTag("Player"))
            .Subscribe(collider =>
            {
                collider.GetComponentInChildren<InventoryManager>().AddItem(item, amount);
                Destroy(gameObject);
            }).AddTo(this);
    }
}
