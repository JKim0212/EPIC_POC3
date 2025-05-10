using UnityEngine;
using UnityEngine.UI;

public class UI_InteractionIndicator : MonoBehaviour
{
    Slider _gage;

    void Start()
    {
        _gage = GetComponentInChildren<Slider>();

    }

    public void UpdateGage(float current, float total)
    {
        _gage.maxValue = total;
        if(current >= total)
        {
            _gage.value = total;
        } else
        {
            _gage.value = current;
        }
            
    }
}
