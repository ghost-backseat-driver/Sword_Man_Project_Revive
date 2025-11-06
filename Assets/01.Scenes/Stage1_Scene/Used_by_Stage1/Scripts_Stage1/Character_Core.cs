using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//모든 캐릭터들의 공통 기능 쑤셔넣기-어웨이크에서 불러와야 할것 위주,
//추가해야 할게 있을시, 뭘 추가했는지 기억하고, 절대절대절대 지우지 말 것 진짜 큰일남
public class Character_Core : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
