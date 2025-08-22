using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingObjectController : MonoBehaviour
{
    [SerializeField]
    Vector2 _startPoint;
    [SerializeField]
    Vector2 _leftPoint;
    [SerializeField]
    Vector2 _rightPoint;

    Vector2 _worldStartPoint;
    Vector2 _worldLeftPoint;
    Vector2 _worldRightPoint;

    [SerializeField]
    float moveTime;
    [SerializeField]
    float standTime;

    float timer;

    [SerializeField]
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        timer = standTime;
        this.transform.localPosition = _leftPoint;
        _worldLeftPoint = this.transform.position;
        this.transform.localPosition = _rightPoint;
        _worldRightPoint = this.transform.position;
        this.transform.localPosition = _startPoint;
        _worldStartPoint = this.transform.position;

        if (_startPoint == _leftPoint)
        {
            spriteRenderer.flipX = true;
        }
        if (_startPoint == _rightPoint)
        {
            spriteRenderer.flipX = false;
        }
    }
    private void Update()
    {
        if (InGamePlayManager.instance.blacked)
        {
            DOTween.Kill(this.gameObject);
            timer = standTime;
            Init();
            return;

        }
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Moving();
            timer = moveTime + standTime;
        }
    }
    public void Init()
    {
        timer = standTime;
        this.transform.localPosition = _startPoint;
        if (_startPoint == _leftPoint)
        {
            spriteRenderer.flipX = true;
        }
        if (_startPoint == _rightPoint)
        {
            spriteRenderer.flipX = false;
        }
    }
    public void Moving()
    {
        if ((Vector2)this.transform.localPosition == _rightPoint)
        {
            spriteRenderer.flipX = false;
            this.transform.DOMove(_worldLeftPoint, moveTime);
        }
        if ((Vector2)this.transform.localPosition == _leftPoint)
        {
            spriteRenderer.flipX = true;
            this.transform.DOMove(_worldRightPoint, moveTime);
        }
    }
}