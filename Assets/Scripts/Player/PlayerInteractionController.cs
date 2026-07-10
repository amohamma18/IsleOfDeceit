using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private PlayerInputController playerInput;
    [SerializeField] private PlayerContext playerContext;
    [SerializeField] private InteractionPromptUI promptUI;

    private IInteractable currentInteractable;
    private IInteractable previousInteractable;

    private void Update()
    {
        DetectInteractable();
    }

    private void OnEnable()
    {
        if (playerInput != null)
        {
            playerInput.InteractPressed += Interact;
        }
    }

    private void OnDisable()
    {
        if (playerInput != null)
        {
            playerInput.InteractPressed -= Interact;
        }
    }

    private void Interact()
    {
        currentInteractable?.Interact(playerContext);
    }

    private void DetectInteractable()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
        {
            currentInteractable = hit.collider.GetComponentInParent<IInteractable>();
        }
        else
        {
            currentInteractable = null;
        }

        if (currentInteractable != previousInteractable)
        {
            if (currentInteractable != null)
            {
                promptUI.ShowPrompt(currentInteractable.InteractionText);
            }
            else
            {
                promptUI.HidePrompt();
            }
        }
        previousInteractable = currentInteractable;
    }
}
