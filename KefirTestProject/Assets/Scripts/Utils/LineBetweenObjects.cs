using UnityEngine;
using UnityEngine.UI;

namespace KefirTestProject.Utils
{
    [RequireComponent(typeof(Image))]
    public class LineBetweenObjects : MonoBehaviour
    {
        private RectTransform _firstTransform;
        private RectTransform _secondTransform;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void Draw(GameObject one, GameObject two)
        {
            _firstTransform = one.GetComponent<RectTransform>();
            _secondTransform = two.GetComponent<RectTransform>();

            if (_firstTransform.localPosition.x > _secondTransform.localPosition.x)
            {
                var temp = _firstTransform;
                _firstTransform = _secondTransform;
                _secondTransform = temp;
            }

            UpdatePosition();
        }

        private void UpdatePosition()
        {
            var firstLocalPosition = _firstTransform.localPosition;
            var secondLocalPosition = _secondTransform.localPosition;
            _rectTransform.localPosition = (firstLocalPosition + secondLocalPosition) / 2;
            Vector3 dif = secondLocalPosition - firstLocalPosition;
            _rectTransform.sizeDelta = new Vector2(dif.magnitude, 5);
            _rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 180 * Mathf.Atan(dif.y / dif.x) / Mathf.PI));
        }
    }
}
