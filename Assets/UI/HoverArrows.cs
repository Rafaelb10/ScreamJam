using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HoverArrows :MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _arrow;

    private Button _button;
    private Image _arrowImage;

    private void Awake()
    {
        _button = GetComponent<Button>();
        if(_arrow != null)
        {
            _arrowImage = _arrow.GetComponent<Image>();
            _arrow.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_arrow == null) return;

        // Ajusta a cor da seta para a cor de 'highlight' do botão
        if (_arrowImage != null && _button != null)
        {
            _arrowImage.color = _button.colors.highlightedColor;
        }

        _arrow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_arrow == null) return;
        _arrow.SetActive(false);
    }

}
