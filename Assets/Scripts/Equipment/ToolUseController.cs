using UnityEngine;

public class ToolUseController : MonoBehaviour
{
    [SerializeField] private PlayerInputController playerInputController;
    [SerializeField] private EquipmentSystem equipmentSystem;

    [SerializeField] private Camera playerCamera;

    private float nextUseTime = 0f;

    private void OnEnable()
    {
        playerInputController.AttackPressed += UseEquippedTool;
    }

    private void UseEquippedTool()
    {
        ToolData toolData = equipmentSystem.EquippedToolData;
        if (toolData == null) { return; }
        if (Time.time < nextUseTime) { return; }
        nextUseTime = Time.time + toolData.UseCooldown;
        
        TryHitTarget(toolData);
    }

    private void TryHitTarget(ToolData toolData)
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (!Physics.Raycast(ray, out RaycastHit hitInfo, toolData.Range)) { return; }

        IDamageable target = hitInfo.collider.GetComponentInParent<IDamageable>();
        if (target == null) { return; }

        target.TakeDamage(toolData.Damage);
    }
}
