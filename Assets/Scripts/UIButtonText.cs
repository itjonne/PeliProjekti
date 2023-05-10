using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIButtonText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buttonText;
    public Image buttonImage;
    public Color highlightedTextColor;
    public Color highlightedButtonColor;

    private Color normalTextColor;
    private Color normalButtonColor;

    void Start()
    {
        normalTextColor = buttonText.color;
        normalButtonColor = buttonImage.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = highlightedTextColor;
        buttonImage.color = highlightedButtonColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = normalTextColor;
        buttonImage.color = normalButtonColor;
    }
}
