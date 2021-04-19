using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PressableButton : MonoBehaviour
{
    public Sprite pressedButton;

    private Image _image;
    private Sprite _unpressedButton;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _unpressedButton = _image.sprite;
    }

    public void PointerDown()
    {
        _image.sprite = pressedButton;
    }

    public void PointerUp()
    {
        _image.sprite = _unpressedButton;
    }
}
