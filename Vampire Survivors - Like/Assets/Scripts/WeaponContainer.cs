using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

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
        InfoPanel.transform.DOScale(Vector3.one, 0.2f).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InfoPanel.transform.localScale = Vector3.zero;
        InfoPanel.gameObject.SetActive(false);
    }
}
