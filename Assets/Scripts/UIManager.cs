using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance => _instance;

    static UIManager _instance;

    UI_InGameCanvas uI_InGameCanvas;
    UI_InteractionIndicator uI_InteractionIndicator;
    UI_EndDayCanvas uI_EndDayCanvas;

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
        uI_InGameCanvas = FindAnyObjectByType<UI_InGameCanvas>();
        uI_InteractionIndicator = FindAnyObjectByType<UI_InteractionIndicator>();
        uI_EndDayCanvas = FindAnyObjectByType<UI_EndDayCanvas>();
    }

    public void ToggleInteractIndicator(bool status, float gage = 1, float total = 1)
    {
        if (status)
        {
            uI_InteractionIndicator.GetComponent<Canvas>().enabled = true;
            UpdateIndicatorGage(gage, total);
        } else
        {
            uI_InteractionIndicator.GetComponent<Canvas>().enabled = false;
        }
    }

    public void UpdateIndicatorGage(float gage = 1, float total = 1)
    {
        uI_InteractionIndicator.UpdateGage(gage, total);
    }

    public void ToggleEndDayCanvas(bool status)
    {
        if (status)
        {
            uI_EndDayCanvas.GetComponent<Canvas>(). enabled = true;
            UpdateStats();
        }
        else
        {
            uI_EndDayCanvas.GetComponent<Canvas>().enabled = false;
        }
    }

    public void UpdateStats()
    {
        float earning = GameManager.Instance.PayPerShot * (GameManager.Instance.NumSuccess - GameManager.Instance.NumFailed);
        GameManager.Instance.EarnMoney(earning);
        uI_EndDayCanvas.UpdateText(GameManager.Instance.NumSuccess, GameManager.Instance.NumFailed, earning, GameManager.Instance.LossPerDay, GameManager.Instance.RemainingMoney);
    }
}
