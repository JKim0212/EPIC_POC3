using UnityEngine;

public class Reset : Interactable
{
    private void Start()
    {
        ObjectHandleManager.Instance.interactAction += Interact;
    }

    protected override void Interact()
    {
        if (_canInteract)
        {
            ResetManager.Instance.ResetAll();
            _canInteract = false;
            UIManager.Instance.ToggleInteractIndicator(false);
            GameManager.Instance.IncreaseMistake();
        }
    }

    protected override bool CheckInteractability()
    {
        return true;
    }

    protected override void ShowInteraction()
    {
        UIManager.Instance.ToggleInteractIndicator(true);
    }
}
