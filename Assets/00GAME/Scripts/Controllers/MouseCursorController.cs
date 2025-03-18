using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorController : Singleton<MouseCursorController>
{
    public int bullet;
    Vector2 _shootDir;

    [SerializeField]
    GameObject bubblePrefab;

    [SerializeField]
    GameObject shootDirObj;
    // Start is called before the first frame update
    void Start()
    {
        ObjectPooling.instance.CreatePool(bubblePrefab, 10);
        bullet = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = new Vector3(mousePos.x,mousePos.y,0);

        _shootDir = BubbleController.instance._mainPos - (Vector2)this.transform.position;
        _shootDir.Normalize();
        
        if(BubbleController.instance.bubbles.Count!= 0)
        {
            float angle = Mathf.Atan2(_shootDir.y, _shootDir.x) * Mathf.Rad2Deg - 90f;
            shootDirObj.transform.rotation = Quaternion.Lerp(shootDirObj.transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), 100 *
            Time.deltaTime);
        }
        else
        {
            shootDirObj.transform.rotation = new Quaternion(0,0,0,0);
        }

        if (Input.GetMouseButtonDown(0) && bullet > 0)
        {
            GameObject obj = ObjectPooling.instance.GetObject(bubblePrefab);
            obj.GetComponent<Bubble>()._isAttached = false;
            obj.transform.position = this.transform.position;
            obj.SetActive(true);
            obj.GetComponent<Bubble>().Init();
            obj.GetComponent<Bubble>().Shoot(_shootDir);
            obj.GetComponent<Bubble>().Ble();
            bullet--;
        }
    }
}
