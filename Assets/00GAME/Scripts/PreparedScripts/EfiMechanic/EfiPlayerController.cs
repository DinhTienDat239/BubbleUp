using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EfiPlayerController : MonoBehaviour
{
    [SerializeField]
    float _moveSpeed;
    Vector3 _moveDir;

    [SerializeField]
    float _rotateSpeed;
    float _rotation;

    [SerializeField]
    GameObject _playerEye;

    [SerializeField]
    float _minTraitPosDis;
    Vector3 _prevPos;
    [SerializeField]
    LineRenderer TraitDrawing;
    [SerializeField]
    EdgeCollider2D TraitCollider;
    List<Vector2> TraitPos = new List<Vector2>();

    Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Init();
    }
    public void Init()
    {
        _prevPos = this.transform.position;
        TraitDrawing.positionCount = 1;
        TraitDrawing.SetPosition(TraitDrawing.positionCount-1,this.transform.position);
        TraitPos.Add(this.transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        UpdateTraitPos();
    }
    public void UpdateTraitPos()
    {
        if (Vector3.Distance(_prevPos, this.transform.position) < _minTraitPosDis)
            return;

        List<Vector2> TraitColliderPos = new List<Vector2>(TraitPos);
        if(TraitColliderPos.Count > 5)
        {
            TraitColliderPos.RemoveRange(TraitColliderPos.Count - 5, 5);
            TraitCollider.SetPoints(TraitColliderPos);
        }
        
        TraitDrawing.positionCount++;
        TraitDrawing.SetPosition(TraitDrawing.positionCount - 1, this.transform.position);
        _prevPos = this.transform.position;
        TraitPos.Add(this.transform.position);
        
    }
    public void ProcessInput()
    {
        _moveDir = (_playerEye.transform.position - this.transform.position).normalized;
        _rotation = Input.GetAxisRaw("Horizontal") * -_rotateSpeed * Time.deltaTime;
        transform.Rotate(0, 0, _rotation);
        _rb.velocity = _moveSpeed * _moveDir.normalized;
    }
}
