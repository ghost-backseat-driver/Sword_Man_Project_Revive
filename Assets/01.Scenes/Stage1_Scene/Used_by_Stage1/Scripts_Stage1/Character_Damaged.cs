using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ǰݽ� �˹�+���� �÷��̾�� ���̶� ���� ���� ����
public class Character_Damaged : MonoBehaviour
{
    private Character_Core core;
    private Character_Move move;
    private Character_HP hp;

    [Header("�ǰ� ���� ����")]
    [Header("�˹�� X ��")]
    [SerializeField] private float knockbackForceX = 2.0f;
    [Header("�˹�� Y ��")]
    [SerializeField] private float knockbackForceY = 1.0f;
    [Header("���� �ð�")]
    [SerializeField] private float stunTime = 0.5f;
    [Header("���� �ð�")]
    [SerializeField] private float invincibleTime = 1.0f;

    [Header("�ǰݹ��� �� ����-�÷�")]
    //�ϴ� RGBA �ƹ��ų� �־���� �̰� ���鼭 üũ
    [SerializeField] private Color blinkColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    //���� ���� �ߵ�, ����Ÿ�� �ߵ��Ұ�
    private bool isDamaged = false;
    private bool isInvincible = false;

    //HP���� ������������ üũ�� �뵵�� ��(�б��) get
    public bool IsInvincible => isInvincible;

    //Player_ParryBox ���� ����� �������� set �Լ�
    public void SetInvincible(bool value)
    {
        isInvincible = value;
    }

    private static readonly int damagedHash = Animator.StringToHash("isDamaged");

    private void Start()
    {
        core = GetComponent<Character_Core>();
        move = GetComponent<Character_Move>();
        hp = GetComponent<Character_HP>();
    }

    // �ǰ� �� ȣ��-> ������ ��ġ �����ؼ� �˹� ���� ����� ��
    public void OnHit(Vector2 attackerPos)
    {
        //�����̰ų� �׾����� ������
        if (isInvincible || hp.isDead) return;
        StartCoroutine(DamagedCo(attackerPos));
    }

    private IEnumerator DamagedCo(Vector2 attackerPos)
    {
        isDamaged = true;
        isInvincible = true;

        //�̵� ���� �ع����� 
        move.canMove = false;
        move.SetDir(Vector2.zero);

        //�ִϸ��̼� ��������
        core.anim.SetTrigger(damagedHash);

        //�˹� ���� ���
        //�´� ��ġ ������ ��ġ ������� ��������
        Vector2 hitDir = ((Vector2)transform.position - attackerPos).normalized;
        //�˹� ��ġ�� �´� �´� ��ġ�� x,y �� �� ���ϰ�
        Vector2 knockback = new Vector2(hitDir.x * knockbackForceX, knockbackForceY);
        
        //�̵��� �ʱ�ȭ �� ����
        core.rb.velocity = Vector2.zero;
        //�о����
        core.rb.AddForce(knockback, ForceMode2D.Impulse);

        //�ǰ� ���� �����;ߵǰ�..�� ���⼭ �������� �� ���ϵǴµ� �ϴ� �ּ�ó��
        SoundManager.Instance.PlayEffect("Player_ATK2_SFX");

        //����Ÿ�� ��ŭ ��ٸ���,
        yield return new WaitForSeconds(stunTime);

        //���� Ÿ�� ������ ���� �̵����� Ǯ��
        move.canMove = true;
        isDamaged = false;

        //�����ð� ���� ������ �ڷ�ƾ ������
        yield return StartCoroutine(InvincibleBlinkCo());
    }

    private IEnumerator InvincibleBlinkCo()
    {
        float elapsed = 0f;
        SpriteRenderer spriteRenderer = core.spriteRenderer;
        Color original = spriteRenderer.color;

        //��ũ �÷��� ���������̶� �Դٰ��� �� �ݺ���
        while (elapsed < invincibleTime)
        {
            spriteRenderer.color = blinkColor;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = original;
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.2f;
        }

        //�������� ���� ������ �������� false ����
        spriteRenderer.color = original;
        isInvincible = false;
    }
}
