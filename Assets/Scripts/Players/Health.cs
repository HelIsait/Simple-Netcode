using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;


namespace Stanislav.Network.From.Nick
{
    public class Health : NetworkBehaviour
    {
        public UnityEvent death = new();
        [SerializeField] private float amount;


        public bool Alive => amount > 0;


        public void Damage(float value)
        {
            amount -= value;
            if (amount <= 0)
            {
                death?.Invoke();
                gameObject.SetActive(false);
                GetComponent<NetworkObject>()!.Despawn();
            }
        }
    }
}