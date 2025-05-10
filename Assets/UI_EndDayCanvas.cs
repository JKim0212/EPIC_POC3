using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_EndDayCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _successText;
    [SerializeField] TextMeshProUGUI _mistakeText;
    [SerializeField] TextMeshProUGUI _earningText;
    [SerializeField] TextMeshProUGUI _lossText;
    [SerializeField] TextMeshProUGUI _remainderText;

    Button _nextDayButton;
    public void Start()
    {
        _nextDayButton = GetComponentInChildren<Button>();
        _nextDayButton.onClick.AddListener(OnClickNextDay);
    }

    public void UpdateText(int success, int mistake, float earning, float loss, float remainder)
    {
        _successText.SetText($"발사 횟수: {success}");
        _mistakeText.SetText($"실수 횟수: {mistake}");
        _earningText.SetText($"보상금: {earning}");
        _lossText.SetText($"생활비: {loss}");
        _remainderText.SetText($"남은 돈: {remainder}");
    }

    void OnClickNextDay()
    {
        if(GameManager.Instance.RemainingMoney < 0)
        {
            GameManager.Instance.GameOver();
        } else
        {
            GameManager.Instance.StartNextStage();
        }
    }
}
