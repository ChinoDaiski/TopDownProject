using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    // Animator.StringToHash �Լ��� ������ �ִ� ���ڿ� �������� �����Ǵ� �Ķ���͸� �ؽ� �Լ��� ���ؼ� int������ �������ش�.
    // �ٵ� ����
    private static readonly int isMoving = Animator.StringToHash("isMove");
    private static readonly int isDamaged = Animator.StringToHash("isDamaged");

    protected Animator m_animator;

    protected virtual void Awake()
    {
        // ���� ��ũ��Ʈ�� ���� ��ü�� �ƴ� ���� ��ü�������� Animator�� ã�´�.
        m_animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 direction)
    {
        // ���ڷ� ���� ��ġ�� ũ�Ⱑ 0.5�̻��̶��, isMoving �Ķ���͸� true�� �����, �ƴ϶�� false�� �����.
        // �ٵ� �� 0.5 �̻��� ũ��? �̵����͸� ���ڷ� �޴µ�, ���� ũ�� ���ϸ� Idle�̰�, 0.5�̻��̸� �����̴�(Run) �ִϸ��̼��� ���´�.
        m_animator.SetBool(isMoving, direction.magnitude > 0.5f);
    }
    public void Damage()
    {
        // �������� ���� ��� isDamaged�� true�� �����.
        m_animator.SetBool(isDamaged, true);
    }

    public void InvincibilityEnd()
    {
        // �������� ���� ��� ���� �ð� ���� ���·� �����Ǵ�, ������ ���¸� off�Ѵ�.
        m_animator.SetBool(isDamaged, false);
    }
}
