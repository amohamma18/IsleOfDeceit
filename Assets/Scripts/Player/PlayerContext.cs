using UnityEngine;

public class PlayerContext : MonoBehaviour
{
    [SerializeField] private ResourceStorage resourceStorage;

    public ResourceStorage ResourceStorage => resourceStorage;
}
