using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PAA.JB.Core
{
    public enum Orientation
    {
        Portrait,
        Landscape
    }

    [AddComponentMenu("PAA JB SDK/Core/ResponsiveManager")]
    public class ResponsiveManager : MonoBehaviour
    {
        [Header("Current Orientation (ReadOnly)")]
        [SerializeField] private Orientation currentOrientation = Orientation.Portrait;
        public Orientation CurrentOrientation => currentOrientation;

        [Header("Events")]
        public UnityEvent OnPortrait;
        public UnityEvent OnLandscape;
        public UnityEvent<Orientation> OnOrientationChanged;

        [Header("Auto Toggle Objects")]
        [Tooltip("Những GameObject này sẽ được bật khi ở chế độ Portrait và tắt khi ở Landscape")]
        [SerializeField] private List<GameObject> portraitOnlyObjects = new List<GameObject>();

        [Tooltip("Những GameObject này sẽ được bật khi ở chế độ Landscape và tắt khi ở Portrait")]
        [SerializeField] private List<GameObject> landscapeOnlyObjects = new List<GameObject>();

        [Header("Canvas Scaler (Optional)")]
        [Tooltip("Nếu được set, CanvasScaler sẽ đổi resolution theo hướng màn hình")]
        [SerializeField] private CanvasScaler targetCanvasScaler;

        [Tooltip("Reference Resolution khi Portrait (width x height)")]
        [SerializeField] private Vector2 portraitResolution = new Vector2(1080, 1920);

        [Tooltip("Reference Resolution khi Landscape (width x height)")]
        [SerializeField] private Vector2 landscapeResolution = new Vector2(1920, 1080);

        private float lastAspectRatio = -1;

        void Start()
        {
            EvaluateOrientation(forceInvoke: true);
        }

        void LateUpdate() // đảm bảo Canvas đã layout xong
        {
            EvaluateOrientation();
        }

        private void EvaluateOrientation(bool forceInvoke = false)
        {
            float aspect = (float)Screen.width / Screen.height;

            if (!forceInvoke && Mathf.Abs(aspect - lastAspectRatio) < 0.01f)
                return;

            lastAspectRatio = aspect;
            Orientation newOrientation = aspect >= 1f ? Orientation.Landscape : Orientation.Portrait;

            if (newOrientation != currentOrientation || forceInvoke)
            {
                currentOrientation = newOrientation;


                ToggleObjectsByOrientation(currentOrientation);
                UpdateCanvasScaler(currentOrientation);

                OnOrientationChanged?.Invoke(currentOrientation);
                if (currentOrientation == Orientation.Landscape)
                    OnLandscape?.Invoke();
                else
                    OnPortrait?.Invoke();
            }
        }

        private void ToggleObjectsByOrientation(Orientation orientation)
        {
            bool isPortrait = orientation == Orientation.Portrait;

            foreach (var obj in portraitOnlyObjects)
                if (obj != null) obj.SetActive(isPortrait);

            foreach (var obj in landscapeOnlyObjects)
                if (obj != null) obj.SetActive(!isPortrait);
        }

        private void UpdateCanvasScaler(Orientation orientation)
        {
            if (targetCanvasScaler == null) return;

            targetCanvasScaler.referenceResolution =
                orientation == Orientation.Landscape ? landscapeResolution : portraitResolution;

        }
    }
}
