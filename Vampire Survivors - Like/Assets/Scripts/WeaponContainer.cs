using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image Placeholder;

    public WeaponInfoPanel InfoPanel;

    private void Start()
    {
        InfoPanel.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InfoPanel.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InfoPanel.gameObject.SetActive(false);
    }
}
