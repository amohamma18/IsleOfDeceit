using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private PlayerInputController playerInput;
    [SerializeField] private PlayerInteractor interactor;

    private void OnEnable()
    {
        if (playerInput != null)
        {
            playerInput.InteractPressed += DetectInteraction;
        }
    }

    private void OnDisable()
    {
        if (playerInput != null)
        {
            playerInput.InteractPressed -= DetectInteraction;
        }
    }

    private void DetectInteraction()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponentInParent<IInteractable>();
            interactable?.Interact(interactor);
        }
    }
}
