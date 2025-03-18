using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dice : MonoBehaviour,IPointerEnterHandler,IDragHandler,IPointerDownHandler,IPointerExitHandler,IBeginDragHandler, IEndDragHandler
{
    public int i, j;
    public int type;
    public int damage;

    bool pointed;
    bool draging;
    public bool placed;
    CanvasGroup _cg;

    [SerializeField]
    Collider2D _collide;
    public void Init()
    {
        type = Random.Range(0, 3);
        damage = Random.Range(1, 7);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        placed = false;
        _collide.enabled = false;
        draging = true;
        GridManager.instance.UnHightLight(i, j, type);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 vector3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vector3.z = 0;
        this.transform.position = vector3;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        draging = false;
        _collide.enabled = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("22d");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_collide.enabled)
        {
            GridManager.instance.HightLight(i, j, type, Color.yellow);
            pointed = true;
        }
        Debug.Log("d");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        
        pointed = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        _cg = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pointed)
        {
            GridManager.instance.HightLight(i, j, type, Color.red);
        }
        if(!draging && !placed)
        {
            this.transform.position = new Vector3(i, j, 0);
        }
    }
}
