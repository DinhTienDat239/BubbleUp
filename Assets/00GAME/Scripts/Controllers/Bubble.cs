using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public bool _isAttached;
    public Vector3 beforeAttachedPos;

    public Rigidbody2D _rb;

    public bool isWind;

    [SerializeField]
    Sprite ble;
    [SerializeField]
    List<Sprite> bubs = new List<Sprite>();
    [SerializeField]
    List<Sprite> pop = new List<Sprite>();

    SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        beforeAttachedPos = this.transform.localPosition;
        isWind = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(this.transform.position,BubbleController.instance._mainPos) > 10)
        {
            this.gameObject.SetActive(false);
        }
    }
    public void RandomBub()
    {
        if(_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        _spriteRenderer.sprite = bubs[Random.Range(0, bubs.Count)];
    }
    public void Ble()
    {
        if (_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        _spriteRenderer.sprite = ble;
    }
    private void FixedUpdate()
    {
        if (_isAttached)
        {
            this.transform.localPosition = beforeAttachedPos;
            return;
        }
        //_rb.velocity = InGamePlayManager.instance.bubMoveSpeed * Vector3.up;
    }
    public void Init()
    {
        this.transform.rotation = new Quaternion(0,0,0,0);
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = Vector3.zero;
        isWind=false;
    }
    public void Shoot(Vector2 _shootDir)
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(InGamePlayManager.instance.bubShootForce * _shootDir);
    }
    public void Pop()
    {
        StartCoroutine(PopIE());
    }
    IEnumerator PopIE()
    {
        int count = 0;
        while(count < pop.Count)
        {
            this._spriteRenderer.sprite = pop[count];
            yield return new WaitForSeconds(0.01f);
            count++;
        }
        this.gameObject.SetActive(false);
        BubbleController.instance.CheckValidBubble();
        if (_isAttached && BubbleController.instance.bubbles.Count != 0)
        {
            Vector2 dir = (BubbleController.instance._mainPos - (Vector2)this.transform.position).normalized;
            BubbleController.instance._rb.AddForce(InGamePlayManager.instance.bubPopForce * dir);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bubble")
        {
            if (collision.gameObject.transform.parent && collision.gameObject.GetComponent<Bubble>()._isAttached)
            {
                GameObject parent = collision.gameObject.transform.parent.gameObject;
                if (parent.GetComponent<BubbleController>())
                {
                    BubbleController.instance.AttachBubble(this);
                    this.RandomBub();
                }
                BubbleController.instance.CheckValidBubble();
            }
        }
        if (collision.gameObject.tag == "Object")
        {
            AudioManager.instance.PlayEffect(BubbleController.instance.pop_sounds[Random.Range(0, BubbleController.instance.pop_sounds.Count)],false);
            BubbleController.instance.RemoveBubble(this);

            

            
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal" && _isAttached && !InGamePlayManager.instance.isSwitching)
        {

            InGamePlayManager.instance.level++;
            GameManager.instance.savedLevel = InGamePlayManager.instance.level;
            if(InGamePlayManager.instance.level % 2 == 0)
            {
                GameDistribution.Instance.ShowAd();
            }
            InGamePlayManager.instance.over = true;
        }
        if (collision.gameObject.tag == "Wind")
        {
            this.isWind = true;
            Vector2 windDir = collision.gameObject.GetComponent<WindController>().windDir;
            windDir.Normalize();
            if (_isAttached && !BubbleController.instance._isWind)
            {
                BubbleController.instance._isWind = true;
                BubbleController.instance._rb.AddForce(windDir * InGamePlayManager.instance.windForce);
            }else
                this._rb.AddForce(windDir * InGamePlayManager.instance.windForce);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        this.isWind = false;
        if (collision.gameObject.tag == "Wind")
        {
            int count = 0;
            foreach(Bubble b in BubbleController.instance.bubbles)
            {
                if (b.isWind)
                    count++;
            }
            if(count == 0)
            {
                BubbleController.instance._isWind = false;
            }
        }
    }
}
