using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    // [Range] 는 inspector 창에서 값을 조정할 때 제한을 걸 수 있음.
    // [SerializeField]는 inspector 창에서 편집 가능하지만 private이기에 외부에선 접근할 수 없음.
    [Range(1, 100)][SerializeField] private int health = 10;
    public int Health 
    {
        get => health;                              // get { return health; } 
        set => health = Mathf.Clamp(value, 0, 100); // set { health = Mathf.Clamp(value, 0, 100); }
    }


    [Range(1f, 20f)][SerializeField] private float speed = 3;
    public float Speed
    {
        get => speed;                             
        set => speed = Mathf.Clamp(value, 0, 20); 
    }
}
