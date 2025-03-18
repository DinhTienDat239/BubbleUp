using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : Singleton<BubbleController>
{
    public List<Bubble> bubbles = new List<Bubble>();

    public Rigidbody2D _rb;

    public Vector2 _mainPos;

    public bool _isWind;

    [SerializeField]
    GameObject bubblePrefab;

    [Header("Sounds")]
    [SerializeField]
    public List<AudioClip> pop_sounds = new List<AudioClip>();
    [SerializeField]
    AudioClip attach_sound;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        foreach(Transform t in this.transform)
        {
            if (t.GetComponent<Bubble>())
            {
                bubbles.Add(t.GetComponent<Bubble>());
            }
        }
        _isWind = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (bubbles.Count == 0)
            InGamePlayManager.instance.over = true;
        CaculateMainPos();
    }
    public void AttachBubble(Bubble bub)
    {
        _rb.AddForce(InGamePlayManager.instance.bubCollideForce * bub._rb.velocity.magnitude/5 * bub._rb.velocity.normalized);
        bubbles.Add(bub);
        bub.transform.parent = this.transform;
        bub.beforeAttachedPos = bub.transform.localPosition;
        bub._isAttached = true;
        AudioManager.instance.PlayEffect(attach_sound, false);
    }
    private void FixedUpdate()
    {
        if(InGamePlayManager.instance.level > 24 || GameManager.instance.isShowAd)
        {
            _rb.velocity = Vector3.zero;
        }
        //_rb.velocity = InGamePlayManager.instance.bubMoveSpeed * Vector3.up;
    }
    public void CheckValidBubble()
    {
        if (bubbles.Count == 0)
        {
            InGamePlayManager.instance.over = true;

            
            /*foreach (var b in GameObject.FindGameObjectsWithTag("Bubble"))
            {
                if(b.transform.parent != this.transform)
                {
                    if (Vector2.Distance(this.transform.position, b.transform.position) < distance)
                    {
                        bub = b;
                        distance = Vector2.Distance(this.transform.position, b.transform.position);
                    }
                }
            }

            if (distance != 999999)
            {
                this._rb.velocity = bub.GetComponent<Bubble>()._rb.velocity;
                this.transform.position = bub.transform.position;
                AttachBubble(bub.GetComponent<Bubble>());
            }*/
        }
        bubbles.Clear();
        foreach (Transform t in this.transform)
        {
            if (t.gameObject.activeSelf)
            {
                bubbles.Add(t.GetComponent<Bubble>());
            }
        }

        CaculateMainPos();
    }
    public void RemoveBubble(Bubble bubble)
    {

        bubbles.Remove(bubble);
        bubble.Pop();
    }
    public void CaculateMainPos()
    {
        float x= 0;
        float y = 0;
        foreach(Bubble b in bubbles)
        {
            if(b == null)
                return;
            x += b.transform.position.x;
            y += b.transform.position.y;
        }
        x = x / bubbles.Count;
        y = y / bubbles.Count;

        _mainPos = new Vector2(x, y);
    }
    public void SpawnBubble()
    {
        foreach (Transform t in this.transform)
        {
            if (t.gameObject.activeSelf)
            {
                bubbles.Remove(t.GetComponent<Bubble>());
                t.gameObject.SetActive(false);
            }
        }
        GameObject obj = ObjectPooling.instance.GetObject(bubblePrefab);
        obj.transform.parent = this.transform;
        obj.transform.localPosition = Vector3.zero;
        obj.SetActive(true);
        obj.GetComponent<Bubble>().beforeAttachedPos = obj.GetComponent<Bubble>().transform.localPosition;
        obj.GetComponent<Bubble>()._isAttached = true;
        this._isWind = false;
        if(_rb != null)
            this._rb.velocity = Vector3.zero;
        else
        {
            this._rb = GetComponent<Rigidbody2D>();
            this._rb.velocity = Vector3.zero;
        }

        bubbles.Add(obj.GetComponent<Bubble>());
        obj.GetComponent<Bubble>().Init(); 
        obj.GetComponent<Bubble>().RandomBub();
    }
}
