using UnityEngine;
using UnityEngine.UI;

public class Switch : Interactable
{
    private void Start()
    {
        ObjectHandleManager.Instance.interactAction += Interact;
    }

    protected override void Interact()
    {
        if (_canInteract)
        {
            GunManager.Instance.Fire();
            _canInteract = false;
            UIManager.Instance.ToggleInteractIndicator(false);
        }
    }

    protected override bool CheckInteractability()
    {
        return GunManager.Instance.IsReadyToFire;
    }

    protected override void ShowInteraction()
    {
        UIManager.Instance.ToggleInteractIndicator(true);
    }
}
