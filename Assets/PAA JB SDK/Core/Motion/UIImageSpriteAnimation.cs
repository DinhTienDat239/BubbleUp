using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImageSpriteAnimation : MonoBehaviour
{
    [SerializeField]
    Image _UIImageObject;
    [SerializeField]
    bool _autoStart = false;
    [SerializeField]
    bool _loop = true;
    [SerializeField]
    bool _caculateByDuration;
    [SerializeField]
    bool _caculateByFPS;
    [SerializeField]
    int _duration;
    [SerializeField]
    int _animationFPS;
    [SerializeField]
    List<Sprite> _spriteList = new List<Sprite>();

    float _timePerFrame;
    // Start is called before the first frame update
    private void Awake()
    {
        if (_caculateByDuration)
        {
            _timePerFrame =  _duration/ _spriteList.Count;
        }else if (_caculateByFPS)
        {
            _timePerFrame = 1/_animationFPS;
        }
    }
    void Start()
    {
        _UIImageObject.sprite = _spriteList[0];
        if (_autoStart)
        {
            PlayAnimation();
        }
    }

    public void PlayAnimation()
    {
        StartCoroutine(PlayAnimationIE());
    }
    IEnumerator PlayAnimationIE()
    {
        foreach (var sprite in _spriteList)
        {
            _UIImageObject.sprite = sprite;
            yield return new WaitForSeconds(_timePerFrame);
        }
        if (_loop)
            StartCoroutine(PlayAnimationIE());
    }
}
