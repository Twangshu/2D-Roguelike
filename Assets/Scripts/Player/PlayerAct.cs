using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Text;
using System.Runtime.Remoting.Messaging;

public class PlayerAct : MonoBehaviour  //攻击，行走
{


    public float speed = 2f;
    private Rigidbody2D Rigidbody2D;
    private Animator animator;
    public float h = 0;
    public float v = 0;
    private GameObject attackPrefab;
    private GameObject skillPrefab1;
    private GameObject skillPrefab2;
    private string CurrentAnimName;
    private SpriteRenderer sprite;

    //攻击CD
    [SerializeField]
    private float AttackRestTime = 0;
    public float AttackRestTimer = 0.67f;
    [SerializeField]
    private bool isAttacking = false;
    private bool canMove = true;
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attackPrefab = Resources.Load("Prefebs/Player/AttackPrefab") as GameObject;
        skillPrefab1 = Resources.Load("Prefebs/skill/skill1") as GameObject;
        skillPrefab2 = Resources.Load("Prefebs/skill/skill2") as GameObject;
        sprite = GetComponent<SpriteRenderer>();
    }

   
    void FixedUpdate()
    {
        AttackRestTime += Time.deltaTime;
        if (!canMove)
            return;
        Move();
        Attack();
        Skill1();
        Skill2();

    }

    public void takeDamage(int enemyATK, string enemyName)
    {
        if (!canMove)
            return;
        //int damage = enemyATK - PlayInfoManager.Instance.DEF <= 0 ? 1 : enemyATK - PlayInfoManager.Instance.DEF;

        //PlayInfoManager.Instance.currentHP -= damage;
        //StringBuilder sb = new StringBuilder();
        //sb.Append(enemyName).Append("对你造成了").Append(damage.ToString()).Append("点伤害");

        //EventCenter.Broadcast(EventDefine.ShowMessage, msg);
        //if (PlayInfoManager.Instance.currentHP <= 0)
        //{
        //    EventCenter.Broadcast(EventDefine.ShowDeadPanel);
        //    canMove = false;
        //    string deadMsg = "你被" + enemyName + "杀死了";
        //    EventCenter.Broadcast(EventDefine.ShowMessage, deadMsg);
        //}
        StartCoroutine("ChangeColor");
    }
    private IEnumerator ChangeColor()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1, 0.5f, 0.5f, 1f);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(()=>canMove);//等到无敌时间过了再变回去
        gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1f, 1f, 1f, 1f);
    }

    private void Move()//检测移动
    {
        h = Input.GetAxis("Horizontal");//左右值
        v = Input.GetAxis("Vertical");//上下值

        //给人物移动速度
        if (Mathf.Abs(h) > 0 && Mathf.Abs(v) > 0)
        {
            Rigidbody2D.velocity = new Vector2(h, v) * speed * 0.75f;//这种移动方式会检测碰撞
        }
        else if (Mathf.Abs(h) > 0 || Mathf.Abs(v) > 0)
        {
            Rigidbody2D.velocity = new Vector2(h, v) * speed;
        }
        else
        {
            Rigidbody2D.velocity = Vector2.zero;
        }

        if (isAttacking)//如果在攻击中就不用设置移动动画
            return;

        //设置动画
        int temp;
        if (Mathf.Abs(h) > Mathf.Abs(v))
        {
            temp=  h > 0 ? 1:-1;
            animator.SetFloat("h", temp);
            animator.SetFloat("v", 0);
        }
        else if (Mathf.Abs(h) < Mathf.Abs(v))
        {
            temp = v > 0 ? 1 : -1;
            animator.SetFloat("v", temp);
            animator.SetFloat("h", 0);
        }
    }
    private void Attack()//检测普通攻击
    {
        if (Input.GetKeyDown(KeyCode.J) && AttackRestTime >= AttackRestTimer && !isAttacking)//CD好了才能攻击
        {
            AudioManager.Instance.PlayEffect("Attack");
            AttackRestTime = 0;
            Rigidbody2D.velocity = Vector3.zero;
            animator.SetTrigger("Attack");
            CurrentAnimName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;//获取当前播放动画名
            switch (CurrentAnimName)
            {
                case "PlayerMoveRight":
                    GameObject attack1 = Instantiate(attackPrefab, transform.position + new Vector3(0.4f, 0, 0), Quaternion.identity);//生成攻击效果
                    break;
                case "PlayerMoveLeft":
                    GameObject attack2 = Instantiate(attackPrefab, transform.position + new Vector3(-0.4f, 0, 0), Quaternion.identity);
                    break;
                case "PlayerMoveUp":
                    GameObject attack3 = Instantiate(attackPrefab, transform.position + new Vector3(0, 0.6f, 0), Quaternion.Euler(0, 0, 90));
                    break;
                case "PlayerMoveDown":
                    GameObject attack4 = Instantiate(attackPrefab, transform.position + new Vector3(0, -0.6f, 0), Quaternion.Euler(0, 0, 90));
                    break;
                default:
                    break;
            }
            isAttacking = true;
            StartCoroutine(StopAttack(0.67f));
        }

    }
    private void Skill1()//检测技能1
    {
        if (Input.GetKeyDown(KeyCode.K) && AttackRestTime >= AttackRestTimer && !isAttacking)
        {
            //if (PlayInfoManager.Instance.currentMP < 3)
            //    return;
            //else
            //{
                AudioManager.Instance.PlayEffect("Skill1");
               // PlayInfoManager.Instance.currentMP -= 3;
                AttackRestTime = 0;
                Rigidbody2D.velocity = Vector3.zero;
                animator.SetTrigger("Attack");
                CurrentAnimName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

                switch (CurrentAnimName)
                {
                    case "PlayerMoveRight":
                        GameObject attack1 = Instantiate(skillPrefab1, transform.position, Quaternion.identity);
                        attack1.transform.DOMove(transform.position + new Vector3(1, 0, 0), 0.5f);
                        break;
                    case "PlayerMoveLeft":
                        GameObject attack2 = Instantiate(skillPrefab1, transform.position, Quaternion.Euler(0, 0, 180));
                        attack2.transform.DOMove(transform.position + new Vector3(-1, 0, 0), 0.5f);
                        break;
                    case "PlayerMoveUp":
                        GameObject attack3 = Instantiate(skillPrefab1, transform.position, Quaternion.Euler(0, 0, 90));
                        attack3.transform.DOMove(transform.position + new Vector3(0, 1, 0), 0.5f);
                        break;
                    case "PlayerMoveDown":
                        GameObject attack4 = Instantiate(skillPrefab1, transform.position, Quaternion.Euler(0, 0, 270));
                        attack4.transform.DOMove(transform.position + new Vector3(0, -1, 0), 0.5f);
                        break;
                    default:
                        break;
                }
                isAttacking = true;
                StartCoroutine(StopAttack(0.67f));
            //   }
        }
    }
    private void Skill2()
    {
       if (Input.GetKeyDown(KeyCode.O) && AttackRestTime >= AttackRestTimer&&!isAttacking)
        {
            //if (PlayInfoManager.Instance.currentMP < 8)
            //    return;
            //else
            //{
                AudioManager.Instance.PlayEffect("Skill2");
            //  PlayInfoManager.Instance.currentMP -= 8;
                StartCoroutine(ChangeAlpha(0.67f));
                AttackRestTime = 0;
                Rigidbody2D.velocity = Vector3.zero;
                animator.SetTrigger("Attack");
                CurrentAnimName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
                GameObject attack = null;
                switch (CurrentAnimName)
                {
                    case "PlayerMoveRight":
                        attack = Instantiate(skillPrefab2, transform.position + new Vector3(.8f, 0, 0), Quaternion.identity);
                        break;
                    case "PlayerMoveLeft":
                        attack = Instantiate(skillPrefab2, transform.position + new Vector3(-.8f, 0, 0), Quaternion.Euler(0, 0, 180));
                        break;
                    case "PlayerMoveUp":
                        attack = Instantiate(skillPrefab2, transform.position + new Vector3(0, .8f, 0), Quaternion.Euler(0, 0, 90));
                        break;
                    case "PlayerMoveDown":
                        attack = Instantiate(skillPrefab2, transform.position + new Vector3(0, -.8f, 0), Quaternion.Euler(0, 0, 270));
                        break;
                    default:
                        break;
                }
                attack.transform.localScale = Vector3.one;
                isAttacking = true;
                StartCoroutine(StopAttack(0.67f));
           // }
        }
    }
    private IEnumerator StopAttack(float time)
    {
        yield return new WaitForSeconds(time);
        isAttacking = false;
    }

    private IEnumerator ChangeAlpha(float time)
    {
        canMove = false;
        sprite.color = new Vector4(1, 1, 1, 0);
        yield return new WaitForSeconds(time);
        sprite.color = new Vector4(1, 1, 1, 1);
        canMove = true;
    }
}
