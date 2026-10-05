using UnityEngine;

[CreateAssetMenu(fileName = "New Tool Data", menuName = "Items/Tool Data")]
public class ToolData : ScriptableObject
{
    [SerializeField] private float range;

    [SerializeField] private float damage;

    [SerializeField] private float useCooldown;

    [SerializeField] private GameObject toolPrefab;

    public float Range => range;
    public float Damage => damage;
    public float UseCooldown => useCooldown;
    public GameObject ToolPrefab => toolPrefab;

}
