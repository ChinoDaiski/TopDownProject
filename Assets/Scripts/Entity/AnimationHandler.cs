using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    // Animator.StringToHash 함수는 가지고 있는 문자열 형식으로 관리되는 파라미터를 해쉬 함수를 통해서 int값으로 변경해준다.
    // 근데 값이
    private static readonly int isMoving = Animator.StringToHash("isMove");
    private static readonly int isDamaged = Animator.StringToHash("isDamaged");

    protected Animator m_animator;

    protected virtual void Awake()
    {
        // 현재 스크립트가 붙은 객체가 아닌 하위 객체에서부터 Animator를 찾는다.
        m_animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 direction)
    {
        // 인자로 받은 위치의 크기가 0.5이상이라면, isMoving 파라미터를 true로 만들고, 아니라면 false로 만든다.
        // 근데 왜 0.5 이상의 크기? 이동벡터를 인자로 받는데, 일정 크기 이하면 Idle이고, 0.5이상이면 움직이는(Run) 애니메이션이 나온다.
        m_animator.SetBool(isMoving, direction.magnitude > 0.5f);
    }
    public void Damage()
    {
        // 데미지를 입은 경우 isDamaged를 true로 만든다.
        m_animator.SetBool(isDamaged, true);
    }

    public void InvincibilityEnd()
    {
        // 데미지를 입은 경우 일정 시간 무적 상태로 유지되다, 데미지 상태를 off한다.
        m_animator.SetBool(isDamaged, false);
    }
}
