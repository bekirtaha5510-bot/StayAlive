using UnityEngine;

public class ZombieDamage : MonoBehaviour
{
    public int damage = 10;
    public float damageRate = 1f;
    float nextDamageTime = 0f;

    void OnTriggerStay(Collider other)
    {
        Debug.Log("Trigger temas: " + other.name);

        if (other.CompareTag("Player"))
        {
            if (Time.time >= nextDamageTime)
            {
                var ph = other.GetComponent<PlayerHealth>();
                if (ph != null)
                {
                    ph.TakeDamage(damage);
                    nextDamageTime = Time.time + damageRate;
                }
            }
        }
    }
}
