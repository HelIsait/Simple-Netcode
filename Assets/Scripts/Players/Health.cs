using UnityEngine;
using UnityEngine.Events;


namespace Players
{
    public class Health : MonoBehaviour
    {
        public UnityEvent death = new();
        [SerializeField] private float amount;


        public void Damage(float value)
        {
            amount -= value;
            if (amount <= 0)
            {
                death?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}