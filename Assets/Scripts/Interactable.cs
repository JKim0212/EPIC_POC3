using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected bool _canInteract = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && CheckInteractability())
        {
            ObjectHandleManager.Instance.CanInteract = true;
            ShowInteraction();
            _canInteract = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ObjectHandleManager.Instance.CanInteract = false;
            _canInteract = false;
            UIManager.Instance.ToggleInteractIndicator(false);
        }
    }

    protected abstract void Interact();
    protected abstract bool CheckInteractability();

    protected abstract void ShowInteraction();
}
