using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _moveSpeed;

    Vector2 _moveDir;
    Vector2 _lastMoveDir;

    Rigidbody2D _rb;
    Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InGamePlayManager.instance.isPause)
            return;

        ProcessInput();
        ProcessAnimation();
    }
    private void FixedUpdate()
    {
        _rb.velocity = _moveSpeed * _moveDir;
    }
    void ProcessInput()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        if((inputX == 0 && inputY == 0) && (_moveDir.x != 0 || _moveDir.y != 0))
        {
            _lastMoveDir = _moveDir;
        }

        _moveDir.x = Input.GetAxisRaw("Horizontal");
        _moveDir.y = Input.GetAxisRaw("Vertical");

        _moveDir.Normalize();
    }
    void ProcessAnimation()
    {
        _anim.SetFloat("LastMoveX", _lastMoveDir.x);
        _anim.SetFloat("LastMoveY", _lastMoveDir.y);
        _anim.SetFloat("MoveX", _moveDir.x);
        _anim.SetFloat("MoveY", _moveDir.y);
        _anim.SetFloat("MoveMagnitude", _moveDir.magnitude);
    }
}
