using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Text;

public class Enemy: MonoBehaviour//怪物基类
{
   

    protected Rigidbody2D Rigidbody;
    protected GameObject player;
    protected float restTimer = 0;

    [Header("怪物寻路CD")]
    public float restTime = 1;
    public float speed = 0.2f;
    protected bool canMove = true;

    [Header("攻击CD")]
    public float attackCDTimer = 1;
    [SerializeField]
    protected float attackCD = 0;

    protected float difficuitDegree = 1f;//难度系数

    protected int isInitDirRight = 1;//预制体初始方向
    [HideInInspector]
    public Vector3 initialScale;//预制体初始范围

    [Space]
    /// <summary>
    /// 关于掉落
    /// </summary>
    public int[] dropThingsID;
    public int dropChance;
    public ItemUI dropItem;
    protected GameObject item;
    [Space]
    /// <summary>
    /// 怪物属性
    /// </summary>
    public int maxHP = 99;
    public int ATK = 1;
    public int exp = 2;
    public string enemyName;
    protected Animator animator;
    [Space]
    public float attackScale = 1;//攻击范围
    public int findScale = 5;//感应范围
    protected EnemyAI enemyAI;//自身的AI

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
        Rigidbody = GetComponent<Rigidbody2D>();
        initialScale = GetComponent<Transform>().localScale;
        item = Resources.Load("Prefebs/Item/dropItem") as GameObject;
        animator = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyAI>();
        difficuitDegree = PlayerPrefs.GetFloat("Difficult",1);
        maxHP =(int) (difficuitDegree*maxHP);
        ATK = (int)(difficuitDegree * ATK);
    }
   
    void FixedUpdate()
    {
        restTimer += Time.deltaTime;
        float distance = (transform.position - player.transform.position).magnitude;
        if (canMove && distance > findScale)//与玩家之间的距离大于搜索距离
        {
            enemyAI.enabled = false;//关闭寻路AI
            Move();//随机移动
        }
        else if (canMove && distance <= findScale)
        {
            enemyAI.enabled = true;
        }
        else//不能移动的情况
        {
           Rigidbody.velocity = Vector3.zero;
        }

        if (distance < attackScale)//如果距离已经小于攻击范围
        {
            enemyAI.enabled = false;//关闭寻路AI
            Rigidbody.bodyType = RigidbodyType2D.Kinematic;//设置为不可移动
            canMove = false;
            attackCD += Time.deltaTime;//开始计时攻击CD，此处可以酌情给一个初始CD，不然怪物第一次攻击有点慢
            Attack();//触发自身攻击
        }
        else//玩家离开攻击范围
        {
            attackCD = 0;//重置CD
            Rigidbody.bodyType = RigidbodyType2D.Dynamic;
            canMove = true;
        }
            
    }
    public void takeDamage(object[] type)
    {
        StartCoroutine("ChangeColor");//受到攻击，改变颜色
        bool isDie = false;
        for (int i = 0; i < (int)type[1]; i++)//计算伤害
        {
            maxHP -= (int)(PlayInfoManager.Instance.ATK * (float)type[0]);
            StringBuilder sb = new StringBuilder();
            sb.Append("你对").Append(enemyName).Append("造成了").Append((PlayInfoManager.Instance.ATK * (float)type[0]).ToString()).Append("点伤害");
            EventCenter.Broadcast(EventDefine.ShowMessage, sb);
            if (maxHP <= 0)
            {
                isDie = true;
                break;
            }
        }
        if (isDie)//如果怪物已经死亡
        {
            AudioManager.Instance.PlayEffect("EnemyDie");
            EventCenter.Broadcast(EventDefine.ShowMessage, "你杀死了" + enemyName);
            PlayInfoManager.Instance.exp += exp;
            int chance = Random.Range(0, 100);
            if (chance < dropChance)//随机掉落物品
            {
                int dropThingIndex = Random.Range(0, dropThingsID.Length);
                Instantiate(item, transform.position, Quaternion.identity).GetComponent<DropItem>().SetItem(InventoryManager.Instance.GetItemById(dropThingsID[dropThingIndex]));
            }
            Destroy(gameObject);
        }
    }
    public void Move()
    {
       if(restTimer>restTime)
        {
            StartMove();
            restTimer = 0;
        }  
    }
    protected IEnumerator ChangeColor()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1, 0.5f, 0.5f,1f);
        yield return new WaitForSeconds(0.6f);
        gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1f, 1f, 1f, 1f);
    }
    public virtual void StartMove()//移动方式
    {
            int h = Random.Range(-3, 3);
            int v = Random.Range(-3, 3);
            int randomIndex = Random.Range(0, 2);
            Vector2 targetPos = randomIndex == 0 ? new Vector2(h, 0) : targetPos = new Vector2(0, v);
            transform.localScale = h > 0? new Vector3(1 * isInitDirRight * initialScale.x, initialScale.y, 1): new Vector3(-1 * isInitDirRight * initialScale.x, initialScale.y, 1); 
            Rigidbody.velocity = targetPos * speed;
      
    }
    public virtual void Attack()//攻击方式
    {
        Vector2 dir = (player.transform.position - transform.position).normalized;//判断攻击方向
        transform.localScale = dir.x >= 0.01f? new Vector3(1 * isInitDirRight * initialScale.x, initialScale.y, 1) : transform.localScale = new Vector3(-1 * isInitDirRight * initialScale.x, initialScale.y, 1); ;
        
        if (attackCD > attackCDTimer)
        {
            AttackEffect();
            attackCD = 0;
        }
    }

    public virtual void AttackEffect()
    {
        player.GetComponent<PlayerAct>().takeDamage(ATK, enemyName);//默认直接让玩家收到伤害
    }

    
}
