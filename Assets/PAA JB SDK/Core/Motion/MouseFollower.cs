using DG.Tweening;
using PAA.JB.Core.Enums;
using PAA.JB.UI.FX;
using UnityEngine;
using UnityEngine.UI;

namespace PAA.JB.Core.Motion
{
    [AddComponentMenu("PAA JB SDK/Core/MouseFollower")]
    public class MouseFollower : MonoBehaviour
    {
        [Header("Motion Settings")]
        [Tooltip("Áp dụng với vật thể UI")]
        public bool useAnchoredPosition = true;
        [Tooltip("Di chuyển theo chuột dựa trên vận tốc")]
        public bool smoothFollow = false;
        [Tooltip("Tốc độ di chuyển theo chuột")]
        public float moveSpeed = 200;
        [Tooltip("Offset vị trí so với chuột (X, Y)")]
        public Vector2 mouseOffset = Vector2.zero;

        [Header("Bounding Settings")]
        [Tooltip("Giới hạn vùng vật thể đi theo chuột")]
        public bool limitToBounds = false;
        [Tooltip("Giới hạn vùng vật thể đi theo chuột")]
        public Rect followBounds = new Rect(-200, -200, 400, 400);
        [Tooltip("Tự động bắt đầu khi vật thể xuất hiện (OnEnable).")]
        public bool autoStart = true;

        [Header("Click Settings")]
        public bool holdOnClick;

        [Header("Sprite Change Settings")]
        [Tooltip("Thay đổi sprite khi nhấn chuột (chỉ áp dụng cho UI Image)")]
        public bool changeSpriteOnClick = false;
        [Tooltip("Sprite mới khi nhấn chuột")]
        public Sprite clickSprite;
        private Image _image;
        private Sprite _originalSprite;

        [Tooltip("Hiệu ứng phóng khi nhấp chuột")]
        public bool pulseEffectOnClick;
        PulseEffect pulseEffect;
        public PulseMode pulseMode = PulseMode.PulseIn;
        [Tooltip("Lựa chọn hiệu ứng")]
        public Ease pulseEase = Ease.InOutSine;
        [Tooltip("Độ phóng lớn nhất (PulseIn) hoặc nhỏ nhất (PulseOut)")]
        public float pulseScale = 1.1f;
        [Tooltip("Thời gian cho toàn bộ 1 chu kỳ")]
        public float pulseDuration = 0.6f;

        [Tooltip("Hiệu ứng xoay khi nhấp chuột")]
        public bool rotationEffectOnClick;
        RotationEffect rotationEffect;
        public RotationMode rotationMode = RotationMode.LeftRight;
        [Tooltip("Lựa chọn hiệu ứng")]
        public Ease rotationEase = Ease.Linear;
        [Tooltip("Góc cần xoay (theo trục Z)")]
        public float rotationAngle = 30f;
        [Tooltip("Thời gian cho toàn bộ 1 chu kỳ (1 nửa hoặc 1/3 khi snap)")]
        public float rotationDuration = 0.6f;
        [Tooltip("Giật về trạng thái ban đầu thay vì chuyển động đảo ngược")]
        public bool rotationSnap = false;

        private RectTransform _rectTransform;
        private Camera _uiCamera;
        private bool _isFollowing = true;
        private Tween _currentTween;

        void Awake()
        {
            if (useAnchoredPosition)
            {
                _rectTransform = GetComponent<RectTransform>();
                _uiCamera = _rectTransform.GetComponentInParent<Canvas>().worldCamera;
            }
            
            // Khởi tạo Image component cho chức năng thay đổi sprite
            if (changeSpriteOnClick)
            {
                if (!gameObject.TryGetComponent<Image>(out _image))
                {
                    Debug.LogWarning("MouseFollower: changeSpriteOnClick được bật nhưng không tìm thấy Image component");
                    changeSpriteOnClick = false;
                }
                else
                {
                    _originalSprite = _image.sprite;
                }
            }
            
            if (pulseEffectOnClick)
            {
                if (!gameObject.TryGetComponent<PulseEffect>(out pulseEffect))
                {
                    pulseEffect = gameObject.AddComponent<PulseEffect>();
                }
                pulseEffect.pulseMode = this.pulseMode;
                pulseEffect.ease = this.pulseEase;
                pulseEffect.pulseScale = this.pulseScale;
                pulseEffect.duration = this.pulseDuration;
                pulseEffect.loop = false;
                pulseEffect.delayBetween = 0;
                pulseEffect.startDelay = 0;
                pulseEffect.autoStart = false;
                pulseEffect.StopPulse();
            }
            if (rotationEffectOnClick)
            {
                if (!gameObject.TryGetComponent<RotationEffect>(out rotationEffect))
                {
                    rotationEffect = gameObject.AddComponent<RotationEffect>();
                }
                rotationEffect.rotationMode = this.rotationMode;
                rotationEffect.ease = this.rotationEase;
                rotationEffect.rotationAngle = this.rotationAngle;
                rotationEffect.duration = this.rotationDuration;
                rotationEffect.loop = false;
                rotationEffect.delayBetween = 0;
                rotationEffect.startDelay = 0;
                rotationEffect.autoStart = false;
                rotationEffect.StopRotation();
            }
        }

        void OnEnable()
        {
            if (autoStart)
                StartFollow();
        }

        void OnDisable()
        {
            StopFollow();
        }

        public void StartFollow()
        {
            _isFollowing = true;
        }

        public void StopFollow()
        {
            _isFollowing = false;
            _currentTween?.Kill();
        }

        void Update()
        {
            if (!_isFollowing) return;

            #region Motion
            if (useAnchoredPosition)
            {
                if (_rectTransform == null || _rectTransform.parent == null)
                {
                    Debug.LogError("MouseFollower: This object is not an UI element or is not in Canvas");
                    return;
                }
                RectTransform parentRect = _rectTransform.parent as RectTransform;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, Input.mousePosition, _uiCamera, out Vector2 localPoint);
                
                // Áp dụng offset
                localPoint += mouseOffset;
                
                if (limitToBounds)
                    localPoint = ClampToRect(localPoint, followBounds);

                if (smoothFollow)
                {
                    _rectTransform.anchoredPosition = Vector2.Lerp(_rectTransform.anchoredPosition, localPoint, moveSpeed * Time.unscaledDeltaTime);
                }
                else
                {
                    _rectTransform.anchoredPosition = localPoint;
                }

            }
            else
            {
                Vector3 targetPos = Vector3.zero;
                targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPos.z = transform.position.z;
                
                // Áp dụng offset cho world position
                targetPos += new Vector3(mouseOffset.x, mouseOffset.y, 0);

                if (limitToBounds)
                    targetPos = ClampToRect(targetPos, followBounds);

                if (smoothFollow)
                {
                    transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
                }
                else
                {
                    transform.position = targetPos;
                }
            }
            #endregion

            #region Click
            if (Input.GetMouseButtonDown(0))
            {
                // Thay đổi sprite khi nhấn chuột
                if (changeSpriteOnClick && _image != null && clickSprite != null)
                {
                    _image.sprite = clickSprite;
                }
                
                if (pulseEffectOnClick)
                {
                    if (holdOnClick)
                    {
                        switch (pulseMode)
                        {
                            case PulseMode.PulseIn:
                                this._rectTransform.DOScale(pulseEffect.GetOriginalScale() * pulseScale, pulseDuration / 2).SetEase(pulseEase);
                                break;

                            case PulseMode.PulseOut:
                                this._rectTransform.DOScale(pulseEffect.GetOriginalScale() * (1f / pulseScale), pulseDuration / 2).SetEase(pulseEase);
                                break;

                            case PulseMode.PulseInThenOut:
                                this._rectTransform.DOScale(pulseEffect.GetOriginalScale() * pulseScale, pulseDuration / 2).SetEase(pulseEase).WaitForCompletion();
                                this._rectTransform.DOScale(pulseEffect.GetOriginalScale() * (1f / pulseScale / 2), pulseDuration).SetEase(pulseEase);
                                break;
                        }
                    }
                    else
                        pulseEffect.StartPulse();
                }
                if (rotationEffectOnClick)
                {
                    if (holdOnClick)
                    {
                        switch (rotationMode)
                        {
                            case RotationMode.Left:
                                transform.DORotate(transform.eulerAngles + new Vector3(0, 0, Mathf.Abs(rotationAngle)), rotationDuration / 2).SetEase(rotationEase);
                                break;

                            case RotationMode.Right:
                                transform.DORotate(transform.eulerAngles - new Vector3(0, 0, Mathf.Abs(rotationAngle)), rotationDuration / 2).SetEase(rotationEase);
                                break;

                            case RotationMode.LeftRight:
                                transform.DORotate(transform.eulerAngles + new Vector3(0, 0, Mathf.Abs(rotationAngle)), rotationDuration / 3).SetEase(rotationEase).WaitForCompletion();
                                transform.DORotate(transform.eulerAngles - new Vector3(0, 0, Mathf.Abs(rotationAngle)), rotationDuration / 3).SetEase(rotationEase);
                                break;

                            case RotationMode.RightLeft:
                                transform.DORotate(transform.eulerAngles - new Vector3(0, 0, Mathf.Abs(rotationAngle)), rotationDuration / 3).SetEase(rotationEase).WaitForCompletion();
                                transform.DORotate(transform.eulerAngles + new Vector3(0, 0, Mathf.Abs(rotationAngle)), rotationDuration / 3).SetEase(rotationEase);
                                break;

                            case RotationMode.ContinuousLeft:
                                transform.DORotate(new Vector3(0, 0, 360f), rotationDuration / 2, RotateMode.FastBeyond360)
                                    .SetEase(rotationEase)
                                    .SetLoops(0)
                                    .SetUpdate(true);
                                break;

                            case RotationMode.ContinuousRight:
                                transform.DORotate(new Vector3(0, 0, -360f), rotationDuration / 2, RotateMode.FastBeyond360)
                                   .SetEase(rotationEase)
                                   .SetLoops(0)
                                   .SetUpdate(true);
                                break;
                        }
                    }
                    else
                        rotationEffect.StartRotation();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                // Khôi phục sprite gốc khi thả chuột
                if (changeSpriteOnClick && _image != null && _originalSprite != null)
                {
                    _image.sprite = _originalSprite;
                }
                
                if (pulseEffectOnClick)
                {
                    if (holdOnClick)
                    {
                        switch (pulseMode)
                        {
                            case PulseMode.PulseIn:
                                this._rectTransform.DOScale(pulseEffect.GetOriginalScale(), pulseDuration / 2).SetEase(pulseEase);
                                break;

                            case PulseMode.PulseOut:
                                this._rectTransform.DOScale(pulseEffect.GetOriginalScale(), pulseDuration / 2).SetEase(pulseEase);
                                break;

                            case PulseMode.PulseInThenOut:

                                this._rectTransform.DOScale(pulseEffect.GetOriginalScale(), pulseDuration / 3).SetEase(pulseEase);
                                break;
                        }
                    }
                }
                if (rotationEffectOnClick)
                {
                    if (holdOnClick)
                    {
                        switch (rotationMode)
                        {
                            case RotationMode.Left:
                                transform.DORotate(rotationEffect.GetOriginalRotation().eulerAngles, rotationDuration / 2).SetEase(rotationEase);
                                break;

                            case RotationMode.Right:
                                transform.DORotate(rotationEffect.GetOriginalRotation().eulerAngles, rotationDuration / 2).SetEase(rotationEase);
                                break;

                            case RotationMode.LeftRight:
                                transform.DORotate(rotationEffect.GetOriginalRotation().eulerAngles, rotationDuration / 3).SetEase(rotationEase);
                                break;

                            case RotationMode.RightLeft:
                                transform.DORotate(rotationEffect.GetOriginalRotation().eulerAngles, rotationDuration / 3).SetEase(rotationEase);
                                break;

                            case RotationMode.ContinuousLeft:
                                transform.DORotate(rotationEffect.GetOriginalRotation().eulerAngles, rotationDuration / 2).SetEase(rotationEase);
                                break;

                            case RotationMode.ContinuousRight:
                                transform.DORotate(rotationEffect.GetOriginalRotation().eulerAngles, rotationDuration / 2).SetEase(rotationEase);
                                break;
                        }
                    }
                }
            }
            #endregion
        }

        private Vector2 ClampToRect(Vector2 pos, Rect bounds)
        {
            float clampedX = Mathf.Clamp(pos.x, bounds.xMin, bounds.xMax);
            float clampedY = Mathf.Clamp(pos.y, bounds.yMin, bounds.yMax);
            return new Vector2(clampedX, clampedY);
        }

        private Vector3 ClampToRect(Vector3 pos, Rect bounds)
        {
            float clampedX = Mathf.Clamp(pos.x, bounds.xMin, bounds.xMax);
            float clampedY = Mathf.Clamp(pos.y, bounds.yMin, bounds.yMax);
            return new Vector3(clampedX, clampedY, pos.z);
        }

        /// <summary>
        /// Thay đổi sprite click từ code
        /// </summary>
        /// <param name="newClickSprite">Sprite mới khi click</param>
        public void SetClickSprite(Sprite newClickSprite)
        {
            clickSprite = newClickSprite;
        }

        /// <summary>
        /// Lấy sprite gốc hiện tại
        /// </summary>
        /// <returns>Sprite gốc</returns>
        public Sprite GetOriginalSprite()
        {
            return _originalSprite;
        }

        /// <summary>
        /// Lấy sprite click hiện tại
        /// </summary>
        /// <returns>Sprite click</returns>
        public Sprite GetClickSprite()
        {
            return clickSprite;
        }

        /// <summary>
        /// Thiết lập offset vị trí so với chuột
        /// </summary>
        /// <param name="offset">Offset mới (X, Y)</param>
        public void SetMouseOffset(Vector2 offset)
        {
            mouseOffset = offset;
        }

        /// <summary>
        /// Thiết lập offset X
        /// </summary>
        /// <param name="offsetX">Offset theo trục X</param>
        public void SetMouseOffsetX(float offsetX)
        {
            mouseOffset.x = offsetX;
        }

        /// <summary>
        /// Thiết lập offset Y
        /// </summary>
        /// <param name="offsetY">Offset theo trục Y</param>
        public void SetMouseOffsetY(float offsetY)
        {
            mouseOffset.y = offsetY;
        }

        /// <summary>
        /// Lấy offset hiện tại
        /// </summary>
        /// <returns>Offset hiện tại</returns>
        public Vector2 GetMouseOffset()
        {
            return mouseOffset;
        }

        /// <summary>
        /// Reset offset về (0, 0)
        /// </summary>
        public void ResetMouseOffset()
        {
            mouseOffset = Vector2.zero;
        }
    }
}
