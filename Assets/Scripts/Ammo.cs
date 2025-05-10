using UnityEngine;

public class Ammo : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ObjectHandleManager.Instance.interactAction += Interact;
    }

    protected override void Interact()
    {
        if (_canInteract)
        {
            ObjectHandleManager.Instance.PickupObject(this.gameObject);
            _canInteract = false;
            UIManager.Instance.ToggleInteractIndicator(false);
        }
    }

    private void OnDestroy()
    {
        ObjectHandleManager.Instance.interactAction -= Interact;
    }

    protected override bool CheckInteractability()
    {
        return (ObjectHandleManager.Instance.ObjectInHand == null);
    }

    protected override void ShowInteraction()
    {
        UIManager.Instance.ToggleInteractIndicator(true);
    }
}
