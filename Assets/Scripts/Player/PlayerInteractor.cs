using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private ResourceStorage resourceStorage;

    public ResourceStorage ResourceStorage => resourceStorage;
}
