using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMousePlayerController : MonoBehaviour
{
    [SerializeField]
    float _moveSpeed;
    Vector3 _facingDir;
    Vector3 _moveDir;

    Vector3 _lastMoveDir;

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
        ProcessInput();
        ProcessAnim();
    }
    private void FixedUpdate()
    {
        _rb.velocity = _moveDir * _moveSpeed;
    }
    public void ProcessInput()
    {
        _facingDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position).normalized;

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        if((inputX == 0 && inputY == 0) && (_moveDir.x != 0 || _moveDir.y != 0))
        {
            _lastMoveDir = _moveDir;
        }

        /*_moveDir.x = inputX;
        _moveDir.y = inputY;*/

        _moveDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position).normalized;
    }
    public void ProcessAnim()
    {
        _anim.SetFloat("MoveX", _facingDir.x);
        _anim.SetFloat("MoveY", _facingDir.y);
        _anim.SetFloat("LastMoveX", _facingDir.x);
        _anim.SetFloat("LastMoveY", _facingDir.y);
        _anim.SetFloat("MoveMagnitude", _rb.velocity.magnitude);
    }
}
