using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [Header("Attack Info")]
    [SerializeField] private float delay = 1f;
    public float Delay { get => delay; set => delay = value; }

    [SerializeField] private float weaponSize = 1f;
    public float WeaponSize { get => weaponSize; set => weaponSize = value; }

    [SerializeField] private float power = 1f;
    public float Power { get => power; set => power = value; }

    [SerializeField] private float speed = 1f;
    public float Speed { get => speed; set => speed = value; }

    [SerializeField] private float attackRange = 10f;
    public float AttackRange { get => attackRange; set => attackRange = value; }

    public LayerMask target;

    [Header("Knockback Info")]
    [SerializeField] private bool isOnKnockback = false;
    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }

    [SerializeField] private float knockbackPower = 0.1f;
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }

    [SerializeField] private float knockbackTime = 0.5f;
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }


    private static readonly int IsAttack = Animator.StringToHash("isAttack");
    public BaseController m_controller { get; private set; }
    private Animator m_animator;
    private SpriteRenderer m_weaponRenderer;

    protected virtual void Awake()
    {
        // 컨트롤러는 플레이어에게서 받아오고, 무기는 하위에 속해있으므로, 부모인 플레이어로부터 받아온다.
        m_controller = GetComponentInParent<BaseController>();

        // 애니메이션은 하위 객체에서 받아온다.
        m_animator = GetComponentInChildren<Animator>();
        m_weaponRenderer = GetComponentInChildren<SpriteRenderer>();

        // 애니메이션의 스피드를 딜레이가 작을수록 더 빠르게, delay가 클수록 더 느리게 한다.
        m_animator.speed = 1.0f / delay;
        transform.localScale = Vector3.one * weaponSize;
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        
    }

    public virtual void Attack()
    {
        AttackAnimation();
    }

    public void AttackAnimation()
    {
        m_animator.SetTrigger(IsAttack);
    }
    public virtual void Rotate(bool isLeft)
    {
        m_weaponRenderer.flipY = isLeft;
    }
}
