using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable
{
    public string InteractionText => "Interact";

    public void Interact(PlayerContext playerContext)
    {
        Debug.Log("Interacted with " + gameObject.name);
    }
}
