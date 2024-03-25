using UnityEngine;
using UnityEngine.UI;

namespace JStudios.QuestSystemExample.Scripts.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private RectTransform BackgroundImage;
        [SerializeField] private RectTransform ForegroundImage;

        private Image _image;
        
        private float _fullWidth;
        
        private void Awake()
        {
            _fullWidth = BackgroundImage.rect.width;
            _image = ForegroundImage.GetComponent<Image>();
            
            SetPercentage(0);
        }

        public void SetPercentage(float percentage)
        {
            var progressWidth = _fullWidth * percentage;

            var rect = ForegroundImage.rect;
            
            ForegroundImage.rect.Set(rect.x, rect.y, progressWidth, rect.height);
            ForegroundImage.sizeDelta = new Vector2(progressWidth, ForegroundImage.sizeDelta.y);
        }

        public void SetColor(Color color)
        {
            _image.color = color;
        }
    }
}