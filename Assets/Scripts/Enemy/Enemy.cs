using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Enemy: MonoBehaviour
{
   

    private Rigidbody2D rigidbody;
    private GameObject player;
    public float restTimer = 0;
    public float restTime = 1;
    public float speed = 0.2f;
    private bool canMove = true;
    private bool canDamage = false;
    public float damageBreakTime = 0;
    public float damageBreakTimer = 0.6f;

    public int isInitDirRight = 1;
    public float attackCD =0;
    public float attackCDTimer = 1;

    public float difficuitDegree = 1f;//难度系数

    public Vector3 initialScale;
    /// <summary>
    /// 关于掉落
    /// </summary>
    //public int[] dropThingsID;
    //public int dropChance;
    //public ItemUI dropItem;
    //private GameObject item;

    /// <summary>
    /// 怪物属性
    /// </summary>
    public int maxHP = 99;
    public int ATK = 1;
    public int exp = 2;
    public string enemyName;
    private Animator animator;
    private GameObject blackBall;
    public float attackScale = 1;//攻击范围
    public int findScale = 5;//感应范围
    public Vector3 test;

    //AI
    private EnemyAI ai;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
        rigidbody = GetComponent<Rigidbody2D>();
        initialScale = GetComponent<Transform>().localScale;
        test = initialScale;
     //   item = Resources.Load("Prefebs/Item/dropItem") as GameObject;
        blackBall = Resources.Load("Prefebs/Monster/Effect/BlackBall") as GameObject;
        animator = GetComponent<Animator>();
        ai = GetComponent<EnemyAI>();
        difficuitDegree = PlayerPrefs.GetFloat("Difficult");
        maxHP =(int) (difficuitDegree*maxHP);
        ATK = (int)(difficuitDegree * ATK);
    }
   
    void FixedUpdate()
    {
        float distance = (transform.position - player.transform.position).magnitude;
        damageBreakTime += Time.deltaTime;
        if (damageBreakTime > damageBreakTimer)
            canDamage = true;
        restTimer += Time.deltaTime;
        if (canMove && distance > findScale)
        {
            ai.enabled = false;
            Move();
        }
        else if (canMove && distance <= findScale)
        {
            ai.enabled = true;
        }
        else
        {
           rigidbody.velocity = Vector3.zero;
        }

        if (distance < attackScale)
        {
            ai.enabled = false;
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
            canMove = false;
            attackCD += Time.deltaTime;
            Attack();

        }
        else
        {
            attackCD = 0;
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            canMove = true;
        }
            
    }
    public void takeDamage(object[] type)
    {
        //canDamage = false;
        //StartCoroutine("ChangeColor");
        //bool isDie = false;
        //for (int i = 0; i < (int)type[1]; i++)
        //{
        //    maxHP -= (int)(PlayInfoManager.Instance.ATK * (float)type[0]);
        //    string msg = "你对" + enemyName + "造成了" + (PlayInfoManager.Instance.ATK * (float)type[0]).ToString() + "点伤害";
        //    EventCenter.Broadcast(EventDefine.ShowMessage, msg);
        //    if (maxHP <= 0)
        //    {
        //        isDie = true;
        //        break;
        //    }
        //}
        //if(isDie)
        //{
        //    //AudioManager.Instance.PlayEffectSound(AudioManager.Instance.dieClip);
        //    EventCenter.Broadcast(EventDefine.ShowMessage, "你杀死了" + enemyName);
        //    PlayInfoManager.Instance.exp += exp;
        //    int chance = Random.Range(0, 100);
        //    if (chance < dropChance)
        //    {
        //        int dropThingIndex = Random.Range(0, dropThingsID.Length);
        //        GameObject dropItem = Instantiate(item, Gamemanager.Instance.gameObject.GetComponent<mapmanager>().mapholder);
        //        dropItem.transform.localPosition = transform.position;
        //        dropItem.GetComponent<dropItem>().SetItem(InventoryManager.Instance.GetItemById(dropThingsID[dropThingIndex]));
        //    }
        //    Destroy(gameObject);
        //}
        


    }
    public void Move()
    {
       
       if(restTimer>restTime)
        {
            StartMove();
            restTimer = 0;
        }  
    }
    private IEnumerator ChangeColor()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1, 0.5f, 0.5f,1f);
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1f, 1f, 1f, 1f);
    }
    private void StartMove()
    {
            int h = Random.Range(-3, 3);
            int v = Random.Range(-3, 3);
            int randomIndex = Random.Range(0, 2);
            Vector2 targetPos;
            if (randomIndex == 0)
                targetPos = new Vector2(h, 0);
            else
                targetPos = new Vector2(0, v);

            if (h > 0)
                transform.localScale = new Vector3(1* isInitDirRight * initialScale.x, initialScale.y, 1);
            else
                transform.localScale = new Vector3(-1* isInitDirRight * initialScale.x, initialScale.y, 1);
            rigidbody.velocity = targetPos * speed;
      
    }
    private void Attack()
    {
        Vector2 dir = (player.transform.position - transform.position).normalized;
        if (dir.x >= 0.01f)
        {
            transform.localScale = new Vector3(1 * isInitDirRight * initialScale.x, initialScale.y, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1 * isInitDirRight * initialScale.x, initialScale.y, 1);
        }
        if (attackCD > attackCDTimer)
        {
            if(enemyName.CompareTo("鬼魂")==0)
            {
                animator.SetTrigger("Attack");
            }
            if(enemyName.CompareTo("黑龙骑士") == 0)
            {
                if (transform.localScale == initialScale)
                    Instantiate(blackBall, transform.position + new Vector3(-1f, 0, 0), Quaternion.identity);
                else
                    Instantiate(blackBall, transform.position + new Vector3(1f, 0, 0), Quaternion.identity);

                attackCD = 0;
                return;
            }
            player.GetComponent<PlayerAct>().takeDamage(ATK,enemyName);
            attackCD = 0;
        }
           

    }
}
