using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    protected Rigidbody2D m_rigidbody2D;

    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;

    // �̵� ����
    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    // �ٶ󺸴� ����
    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    // �˹� ����
    private Vector2 knockBack = Vector2.zero;
    private float knockbackDuration = 0f;

    // �ִϸ��̼� ����
    protected AnimationHandler m_animationHandler;
    protected StatHandler m_statHandler;





    protected virtual void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_animationHandler = GetComponent<AnimationHandler>();
        m_statHandler = GetComponent<StatHandler>();
    }

    protected abstract void Start();

    protected virtual void Update()
    {
        // �Է�, �̵��� ���� ������ ó��
        HandleAction();

        // �ٶ󺸴� ���⿡ ���� ȸ�� ó��.
        Rotate(lookDirection);
    }

    protected virtual void FixedUpdate()
    {
        // ������
        Movement(movementDirection);

        // �˹� ȿ���� �ִٸ�, ���� �ð��� ����
        if(knockbackDuration > 0f)
        {
            knockbackDuration -= Time.deltaTime;
        }
    }

    protected abstract void HandleAction();

    private void Movement(Vector2 direction)
    {
        // ������ speed�� ����
        direction = direction * m_statHandler.Speed;

        // ���� �˹��� ����Ǿ���Ѵٸ�
        if(knockbackDuration > 0f)
        {
            // ���� �̵������� ���� ���̰�
            direction *= 0.2f;

            // �˹��� �����Ų��.
            direction += knockBack;
        }

        // direction�� ��ü�� �ӵ��� �����Ѵ�.
        m_rigidbody2D.velocity = direction;

        // �����ӿ� ���� �ִϸ��̼� ����
        m_animationHandler.Move(direction);
    }

    // ��Ʈ�� �Ǵ� ��� ��ü�� Ư�� �������� ȸ����Ű�� �Լ�
    // ���⿡ ���� ��/�츦 �Ǻ��ϰ�, ��ü�� �����´�.
    // ���� ��ü�� ���Ⱑ �ִٸ�, �ش� ���� ������ ���ڷ� ���� ���⿡ �´� ������ �����Ѵ�.
    private void Rotate(Vector2 direction)
    {
        // ���ڷ� ���� ������ ������ ���Ѵ�. ������ radian���� �Ǿ� �����Ƿ� degree�� �ٲ��ֱ� ���� Rad2Deg�� ���� ������ �ٲ��ش�.
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Atan2�� ���� ������ 1,4 ��и鿡�� -90 ~ 90�� ���� ������.
        // 2,3 ��и鿡�� �������� ������ 90 ~ 180�� ���� �����Ƿ� 90f �̻��̶�� �����̶�� �� �� �ִ�.
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        // �⺻ �̹����� ����ϴ� sprite�� �������� �ٶ󺸰� �����Ƿ�, flipX�� true���� ������ x�� �������� �¿�� �����´�.
        // y���� �������� 180�� ȸ���ϴ� �Ͱ� ����. 
        characterRenderer.flipX = isLeft;

        // ���Ⱑ �����Ѵٸ�
        if(weaponPivot)
        {
            weaponPivot.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
    }

    // �˹� ���� �Լ�
    // ���ڷ� ���� ��ġ���� �ش� ��ü���� �˹��� ���� ��.
    // ������ ���κ��� ���� ���� ���̰�, ���� �������� ���� ������ �ϴ� ������ �ݴ�� �з����� ���̴�.
    public void ApplyKnockBack(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockBack = (transform.position - other.position).normalized * power;
    }
}
