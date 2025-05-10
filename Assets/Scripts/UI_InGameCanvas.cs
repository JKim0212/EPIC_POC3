using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGameCanvas : MonoBehaviour
{
    [SerializeField] RectTransform _manual;
    [SerializeField] float moveSpeed;
    Vector3 closePos = new Vector3(-250, 300, 0);
    Vector3 openPos = new Vector3(-250, -300, 0);
    Button _manualButton;
    bool _isOpen = true;
    
    void Start()
    {
        _manualButton = GetComponentInChildren<Button>();
        _manualButton.onClick.AddListener(OnOpenManualParts);
    }
    public void OnOpenManualParts()
    {
        if (!_isOpen)
        {
            _manualButton.interactable = false;
            _isOpen = true;
            StartCoroutine(MoveCoroutine(openPos));
        }
        else
        {
            _manualButton.interactable = false;
            _isOpen = false;
            StartCoroutine(MoveCoroutine(closePos));
        }
    }

    IEnumerator MoveCoroutine(Vector3 targetPos)
    {
        while (Vector3.Distance(_manual.anchoredPosition, targetPos) > 0.01f)
        {
            _manual.anchoredPosition = Vector3.MoveTowards(_manual.anchoredPosition, targetPos, moveSpeed);
            yield return null;
        }
        _manual.anchoredPosition = targetPos;
        _manualButton.interactable = true;
    }

}
