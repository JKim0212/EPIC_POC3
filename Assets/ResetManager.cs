using System;
using UnityEngine;

public class ResetManager : MonoBehaviour
{
    public Action resetAction;

    public static ResetManager Instance => _instance;
    static ResetManager _instance;

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

    public void ResetAll()
    {
        resetAction?.Invoke();
    }
}
