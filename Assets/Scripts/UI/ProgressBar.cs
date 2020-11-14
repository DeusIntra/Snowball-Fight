using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float minFill = 0f;
    public float maxFill = 1f;

    public float currentFill { get; private set; } = 0f;

    [SerializeField] private Image _mask = null;

    public void SetFill(float amount)
    {
        currentFill = Mathf.Lerp(minFill, maxFill, amount);
        _mask.fillAmount = currentFill;
    }
}
