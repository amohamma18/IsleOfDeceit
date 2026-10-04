using UnityEngine;

public interface IInteractable
{
    string InteractionText { get; }
    void Interact(PlayerContext playerContext);
}
