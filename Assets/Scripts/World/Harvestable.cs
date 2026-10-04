using UnityEngine;

public class Harvestable : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 15f;
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private Vector3 dropOffset = new Vector3(0f, 0.5f, 0f);

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            Harvest();
        }
    }

    private void Harvest()
    {
        Instantiate(dropPrefab, transform.position + dropOffset, Quaternion.identity);
        Destroy(gameObject);
    }

}
