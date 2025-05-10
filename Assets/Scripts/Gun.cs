using UnityEngine;
using UnityEngine.UI;

public class Gun : Interactable
{
    [SerializeField] bool _hasDoor;
    [SerializeField] float _reloadTime;
    [SerializeField] float _doorOpenTime;
    [SerializeField] GameObject _door;
    bool _isDoorOpen = false;
    float _reloadGage = 0;
    float _doorGage = 0;

    Animator _anim;
    private void Start()
    {
        _anim = GetComponent<Animator>();
        ObjectHandleManager.Instance.interactAction += Interact;
        ResetManager.Instance.resetAction += ResetAll;
    }

    protected override void Interact()
    {
        if (!_hasDoor)
        {
            NoDoorInteract();
        }
        else
        {
            HasDoorInteract();
        }
    }

    void NoDoorInteract()
    {
        if (_canInteract)
        {
            LoadAmmo();
        }
    }

    void HasDoorInteract()
    {
        if (_canInteract)
        {
            if (!GunManager.Instance.IsLoaded)
            {
                if (!_isDoorOpen)
                {
                    OpenDoor();
                }
                else
                {
                    LoadAmmo();
                }
            }
            else
            {
                CloseDoor();
            }
        }
    }

    void LoadAmmo()
    {
        _reloadGage += Time.deltaTime;
        UIManager.Instance.UpdateIndicatorGage(_reloadGage, _reloadTime);
        if (_reloadGage > _reloadTime)
        {
            ObjectHandleManager.Instance.DropObject();
            _reloadGage = 0;
            ObjectHandleManager.Instance.CanInteract = false;
            _canInteract = false;
            UIManager.Instance.ToggleInteractIndicator(false);
            if (!_hasDoor)
            {
                GunManager.Instance.Reload();
                GunManager.Instance.ReadyToFire();
            }
            else
            {
                GunManager.Instance.Reload();
            }
        }
    }

    void OpenDoor()
    {
        Quaternion startRotation = Quaternion.Euler(0f, 0f, 0f);
        Quaternion endRotation = Quaternion.Euler(0f, 0f, -90f);
        _doorGage += Time.deltaTime;
        UIManager.Instance.UpdateIndicatorGage(_doorGage, _doorOpenTime);
        float amount = _doorGage / _doorOpenTime;
        _door.transform.rotation = Quaternion.Lerp(startRotation, endRotation, amount);
        if (_doorGage > _doorOpenTime)
        {
            _door.transform.rotation = endRotation;
            _doorGage = 0;
            _isDoorOpen = true;
            _canInteract = false;
            UIManager.Instance.ToggleInteractIndicator(false);
        }
    }

    void CloseDoor()
    {
        Quaternion startRotation = Quaternion.Euler(0f, 0f, -90f);
        Quaternion endRotation = Quaternion.Euler(0f, 0f, 0f);
        _doorGage += Time.deltaTime;
        UIManager.Instance.UpdateIndicatorGage(_doorGage, _doorOpenTime);
        float amount = _doorGage / _doorOpenTime;
        _door.transform.rotation = Quaternion.Lerp(startRotation, endRotation, amount);
        if (_doorGage > _doorOpenTime)
        {
            _door.transform.rotation = endRotation;
            _doorGage = 0;
            _isDoorOpen = false;
            _canInteract = false;
            UIManager.Instance.ToggleInteractIndicator(false);
            GunManager.Instance.ReadyToFire();
        }
    }

    protected override bool CheckInteractability()
    {
        if (!_hasDoor)
        {
            return ObjectHandleManager.Instance.ObjectInHand != null && ObjectHandleManager.Instance.ObjectInHand.CompareTag("Ammo");
        }
        else
        {
            if (!GunManager.Instance.IsLoaded)
            {
                if (!_isDoorOpen)
                {
                    return ObjectHandleManager.Instance.ObjectInHand == null;
                }
                else
                {
                    return ObjectHandleManager.Instance.ObjectInHand != null && ObjectHandleManager.Instance.ObjectInHand.CompareTag("Ammo");
                }

            }
            else
            {
                return ObjectHandleManager.Instance.ObjectInHand == null && _isDoorOpen;
            }

        }

    }

    protected override void ShowInteraction()
    {
        if (!_hasDoor)
        {
            UIManager.Instance.ToggleInteractIndicator(true);
        }
        else
        {
            if (!GunManager.Instance.IsLoaded)
            {
                if (!_isDoorOpen)
                {
                    UIManager.Instance.ToggleInteractIndicator(true, _doorGage, _doorOpenTime);
                }
                else
                {
                    UIManager.Instance.ToggleInteractIndicator(true, _reloadGage, _reloadTime);
                }

            }
            else
            {
                UIManager.Instance.ToggleInteractIndicator(true);
            }

        }
    }

    public void FireGunAnim()
    {
        _anim.SetTrigger("FireGun");
    }

    void ResetAll()
    {
        _isDoorOpen = false;
        _doorGage = 0;
        _reloadGage = 0;
        if (_hasDoor)
        {
            _door.transform.rotation = Quaternion.identity;
        }
    }
}
