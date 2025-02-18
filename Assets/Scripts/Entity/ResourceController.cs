using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = 0.5f;

    private BaseController m_baseController;
    private StatHandler m_statHandler;
    private AnimationHandler m_animationHandler;

    private float timeSinceLastChange = float.MaxValue;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => m_statHandler.Health;

    private void Awake()
    {
        m_baseController = GetComponent<BaseController>();
        m_statHandler = GetComponent<StatHandler>();
        m_animationHandler = GetComponent<AnimationHandler>();
    }

    private void Start()
    {
        CurrentHealth = m_statHandler.Health;
    }

    private void Update()
    {
        // 데미지를 입을 시 일정 시간 동안 무적
        // 이후 다시 데미지를 입을 수 있도록 설정
        if (timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay)
            {
                // 
                m_animationHandler.InvincibilityEnd();
            }
        }
    }

    public bool ChangeHealth(float change)
    {
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
            return false;

        timeSinceLastChange = 0;
        CurrentHealth += change;
        CurrentHealth = Mathf.Min(CurrentHealth, MaxHealth);    // 체력의 최대값/최솟값을 넘지 않도록 설정
        CurrentHealth = Mathf.Max(CurrentHealth, 0);

        // 인자로 받은 change가 음수라면. 즉, 데미지를 입었다면 
        if (change < 0) 
        {
            // 애니메이션 상태를 데미지 입은 것으로 변경
            m_animationHandler.Damage();
        }

        // 체력이 0이하라면
        if(CurrentHealth <= 0)
        {
            // 관련 작업 실시
            Death();
        }

        return true;
    }

    private void Death()
    {

    }
}
