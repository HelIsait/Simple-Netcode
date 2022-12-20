#nullable enable
using Players;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;


[RequireComponent(typeof(NetworkObject))]
[RequireComponent(typeof(NetworkTransform))]
public class Bullet : NetworkBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;


    private void Update()
    {
        transform.Translate(direction * (Time.deltaTime * speed));
    }


    public void Spawn(Vector3 direct)
    {
        direction = direct.normalized;
        GetComponent<NetworkObject>()!.Spawn(true);
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (IsServer)
        {
            if (col.collider!.TryGetComponent(out Health health))
            {
                health!.Damage(damage);
                GetComponent<NetworkObject>()!.Despawn();
            }
        }
    }
}