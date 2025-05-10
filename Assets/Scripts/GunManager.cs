using System;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public Action fireAction;
    public static GunManager Instance => _instance;
    public bool IsLoaded => _isLoaded;
    public bool IsReadyToFire => _isReadyToFire;

    [SerializeField] Gun _gun;
    static GunManager _instance;

    bool _isReadyToFire = false;
    bool _isLoaded = false;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ResetManager.Instance.resetAction += ResetAll;
    }

    public void Reload()
    {
        _isLoaded = true;
    }

    public void ReadyToFire()
    {
        _isReadyToFire = true;
    }
    public void Fire()
    {
        _gun.FireGunAnim();
        _isLoaded = false;
        _isReadyToFire = false;
        GameManager.Instance.IncreaseNumShot();
        fireAction?.Invoke();
    }

    void ResetAll()
    {
        _isLoaded = false;
        _isReadyToFire = false;
    }
}
