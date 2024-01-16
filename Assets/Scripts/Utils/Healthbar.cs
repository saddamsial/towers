using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    [RequireComponent(typeof(Health))]
    public class Healthbar : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private Image fillImage;

        private void Start()
        {
            SetFillImage(health.Ratio);
        }

        private void OnEnable()
        {
            health.OnValueChanged += SetFillImage;
        }

        private void OnDisable()
        {
            health.OnValueChanged -= SetFillImage;
        }

        private void SetFillImage(float ratio)
        {
            fillImage.fillAmount = ratio;
        }
    }
}