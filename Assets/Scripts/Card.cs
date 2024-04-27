using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image _frontImage;
    public bool IsSelected { get; set; } = false;

    public void SetFrontImage(Sprite sprite, bool isBackground)
    {
        var rectTransform = _frontImage.GetComponent<RectTransform>();
        
        _frontImage.sprite = sprite;
        rectTransform.offsetMin = new Vector2(isBackground ? 0 : 20, isBackground ? 0 : 20);
        rectTransform.offsetMax = new Vector2(isBackground ? 0 : -20, isBackground ? 0 : -20);
    }

    public Image GetFrontImage() => _frontImage;
}
