using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float currentFill { get; private set; } = 0f;

    [SerializeField] private Image _mask = null;

    public void SetFill(float amount)
    {
        currentFill = amount;
        _mask.fillAmount = currentFill;
    }
}
