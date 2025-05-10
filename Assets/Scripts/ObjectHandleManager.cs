using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectHandleManager : MonoBehaviour
{
    public GameObject ObjectInHand
    {
        get { return _objectInHand; }
        set
        {
            if(value == null)
            {
                _objectInHand = null;
            } else
            {
                return;
            }
        }
    }
    public bool CanInteract { get;set; }
    public Action interactAction;
    public static ObjectHandleManager Instance => _instance;
    
    static ObjectHandleManager _instance;

    [SerializeField] Transform objectInHandLocation;
    GameObject _objectInHand;
    
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ResetManager.Instance.resetAction += ResetAll;
        _objectInHand = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanInteract && Input.GetKey(KeyCode.E))
        {
            interactAction?.Invoke();
        }
    }

    public void PickupObject(GameObject objectToPickup)
    {
        if (_objectInHand == null)
        {
            objectToPickup.transform.parent = objectInHandLocation.transform;
            objectToPickup.transform.position = objectInHandLocation.transform.position;
            _objectInHand = objectToPickup;
        }
    }

    public void DropObject()
    {
        if (_objectInHand != null) 
        {
            Destroy(_objectInHand);
            _objectInHand = null;
        }
    }

    void ResetAll()
    {
        if(_objectInHand != null)
        {
            DropObject();
        }
    }
}
