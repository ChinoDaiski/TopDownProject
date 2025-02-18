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
        // �������� ���� �� ���� �ð� ���� ����
        // ���� �ٽ� �������� ���� �� �ֵ��� ����
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
        CurrentHealth = Mathf.Min(CurrentHealth, MaxHealth);    // ü���� �ִ밪/�ּڰ��� ���� �ʵ��� ����
        CurrentHealth = Mathf.Max(CurrentHealth, 0);

        // ���ڷ� ���� change�� �������. ��, �������� �Ծ��ٸ� 
        if (change < 0) 
        {
            // �ִϸ��̼� ���¸� ������ ���� ������ ����
            m_animationHandler.Damage();
        }

        // ü���� 0���϶��
        if(CurrentHealth <= 0)
        {
            // ���� �۾� �ǽ�
            Death();
        }

        return true;
    }

    private void Death()
    {

    }
}
