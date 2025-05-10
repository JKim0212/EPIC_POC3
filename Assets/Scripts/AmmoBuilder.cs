using UnityEngine;

public class AmmoBuilder : Interactable
{
    [SerializeField] GameObject _ammoPrefab;
    [SerializeField] Transform _shellPosition;
    [SerializeField] Transform _projectilePosition;
    [SerializeField] Transform _ammoLocation;
    [SerializeField] float _buildTime;
    bool _canBuild = false;
    bool _hasAmmo = false;
    float _buildGage = 0;
    GameObject _builtAmmo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ObjectHandleManager.Instance.interactAction += Interact;
        ResetManager.Instance.resetAction += ResetAll;
    }

    protected override void Interact()
    {
        if (_canInteract)
        {
            if (_hasAmmo)
            {
                PickupAmmo();
            }
            else if (!_canBuild)
            {
                PutDownPart();
            }
            else
            {
                BuildAmmo();
            }
        }
    }

    void PutDownPart()
    {
        switch (ObjectHandleManager.Instance.ObjectInHand.tag)
        {
            case "Shell":
                PutDownPartAux(_shellPosition);
                break;
            case "Projectile":
                PutDownPartAux(_projectilePosition);
                break;
            default:
                break;
        }

        _canBuild = _shellPosition.childCount == 1 && _projectilePosition.childCount == 1;
        _canInteract = false;
        UIManager.Instance.ToggleInteractIndicator(false);
    }

    void PutDownPartAux(Transform position)
    {
        ObjectHandleManager.Instance.ObjectInHand.transform.SetParent(position);
        ObjectHandleManager.Instance.ObjectInHand.GetComponent<Collider2D>().enabled = false;
        ObjectHandleManager.Instance.ObjectInHand.transform.localPosition = Vector3.zero;
        ObjectHandleManager.Instance.ObjectInHand = null;
    }


    void BuildAmmo()
    {
        _buildGage += Time.deltaTime;
        UIManager.Instance.UpdateIndicatorGage(_buildGage, _buildTime);
        if (_buildGage > _buildTime)
        {
            _builtAmmo = Instantiate(_ammoPrefab, _ammoLocation);
            _builtAmmo.GetComponent<Collider2D>().enabled = false;
            _buildGage = 0;
            Destroy(_shellPosition.GetChild(0).gameObject);
            Destroy(_projectilePosition.GetChild(0).gameObject);
            _hasAmmo =true;
            _canBuild = false;
            _canInteract = false;
            UIManager.Instance.ToggleInteractIndicator(false);
        }
    }

    void PickupAmmo()
    {
        ObjectHandleManager.Instance.PickupObject(_builtAmmo);
        _hasAmmo = false;
        _builtAmmo = null;
        _canInteract = false;
        UIManager.Instance.ToggleInteractIndicator(false);
    }

    protected override bool CheckInteractability()
    {
        if (_hasAmmo)
        {
            return ObjectHandleManager.Instance.ObjectInHand == null;
        }
        else if (!_canBuild)
        {
            return ObjectHandleManager.Instance.ObjectInHand != null && (ObjectHandleManager.Instance.ObjectInHand.CompareTag("Shell") || ObjectHandleManager.Instance.ObjectInHand.CompareTag("Projectile"));
        }
        else
        {
            return ObjectHandleManager.Instance.ObjectInHand == null;
        }
    }

    void ResetAll()
    {
        if (_hasAmmo)
        {
            Destroy(_builtAmmo);
            _builtAmmo = null;
            _hasAmmo = false;
        } else
        {
            _buildGage = 0;
            if(_shellPosition.childCount > 0)
            {
                Destroy(_shellPosition.GetChild(0).gameObject);
            } 
            if(_projectilePosition.childCount > 0)
            {
                Destroy(_projectilePosition.GetChild(0).gameObject);
            }
            _canBuild = false;
        }
    }

    protected override void ShowInteraction()
    {
        if (_hasAmmo)
        {
            UIManager.Instance.ToggleInteractIndicator(true);
        }
        else if (!_canBuild)
        {
            UIManager.Instance.ToggleInteractIndicator(true);
        }
        else
        {
            UIManager.Instance.ToggleInteractIndicator(true, _buildGage, _buildTime);
        }
    }
}

