using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class BtnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    Button button;

    public void OnPointerEnter(PointerEventData eventData)
    {
        button.gameObject.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.25f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.gameObject.transform.DOScale(Vector3.one, 0.25f);
    }


}
