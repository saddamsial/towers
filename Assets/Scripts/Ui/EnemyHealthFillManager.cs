using Managers;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthFillManager : MonoBehaviour
{
    public Image fillBar;
    public float hp;
    float val;
    public void OnEnable()
    {
        GameController.onEnemyDamaged += Damaged;
    }
    public void OnDisable()
    {
        GameController.onEnemyDamaged -= Damaged;
    }
    public void Init(float hp)
    {
        fillBar.fillAmount = 1.0f;
        this.hp = hp;
        val = 1.0f / hp;
    }
    public void Damaged(float v)
    {
        hp -= v;
        fillBar.fillAmount = hp * val;
    }
}
