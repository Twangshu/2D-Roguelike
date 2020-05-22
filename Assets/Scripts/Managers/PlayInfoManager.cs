using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

[Serializable]
public  class PlayInfoManager:MonoBehaviour
{
    private static PlayInfoManager _instance;
    public static PlayInfoManager Instance { get => _instance; }

    public List<GameObject> equipmentSlots = new List<GameObject>();
   

    public int ATK = 0;
    public int DEF = 0;
    public int maxHP = 0;
    public int currentHP = 30;
    public int maxMP = 0;
    public int currentMP = 10;
    public int Level = 1;
    public int exp = 0;
    private int baseExp = 10;
    //新加的
    public int gold = 10;
    public int playerHP=30, playerMP=10, playerATK=10, playerDEF=0;
    public int equipmentHP=0, equipmentMP=0, equipmentATK=0, equipmentDEF=0;

    //UI部分
    public Text goldText;
    public Text HPText;
    public Text MPText;
    public Text ATKText;
    public Text DEFText;
    public Text EXPText;

    public Slider HPSlider;
    public Slider MPSlider;
    public Slider expSlider;
    public float textColor = 0.5f;
    private Color goldColor;



    private void Start()
    {
        _instance = this;
        //goldColor = goldText.color;
        //UpdateGoldAmount();
        //UpdatePlayInfo();
    }

    private void Update()
    {
        //EXPText.text = "EXP:"+exp.ToString()+"/"+ (baseExp * Level).ToString();
        //if (exp >= baseExp * Level)
        //{
        //    UpLevel();
        //}
        //HPSlider.value = (float)currentHP / maxHP;
        //MPSlider.value = (float)currentMP / maxMP;
        //expSlider.value = (float)exp / (baseExp * Level);
        //if (currentHP > maxHP)
        //{
        //    currentHP = maxHP;
        //}
        //HPText.text = "HP:"+currentHP.ToString() + "/" + maxHP.ToString();
        //if (currentMP > maxMP)
        //{
        //    currentMP = maxMP;
        //}
        //MPText.text = "MP:"+currentMP.ToString() + "/" + maxMP.ToString();
    }
    public void UpdatePlayInfo()
    {
        //equipmentHP = 0; equipmentMP = 0; equipmentATK = 0; equipmentDEF = 0;
        //foreach (GameObject equipmentSlot in equipmentSlots)
        //{
        //    if (equipmentSlot.transform.childCount > 0)//该装备栏有装备
        //    {
        //        Equipment equipment = (Equipment)equipmentSlot.transform.GetChild(0).GetComponent<ItemUI>().Item;
        //        equipmentATK += equipment.ATK;
        //        equipmentDEF += equipment.DEF;
        //        equipmentHP += equipment.HP;
        //        equipmentMP += equipment.MP;
        //    }

        //}
        //maxHP = playerHP + equipmentHP;
        //maxMP = playerMP + equipmentMP;
        //ATK = playerATK + equipmentATK;
        //DEF = playerDEF + equipmentDEF;

        //if (currentHP > maxHP)
        //{
        //    currentHP = maxHP;
        //}
        //HPText.text ="HP:"+ currentHP.ToString() + "/" + maxHP.ToString();
        //MPText.text = "MP:" + currentMP.ToString() + "/" + maxMP.ToString();
        //ATKText.text ="ATK:"+ ATK.ToString();
        //DEFText.text ="DEF:"+ DEF.ToString();
    }
    public void UpdateGoldAmount()
    {
        goldText.text = gold.ToString();
    }
    public void NotEnoughMoney()
    {
        goldText.color = new Vector4(1.0f, textColor, textColor,1.0f);
        Invoke("TextBack", 0.2f);
    }
    public void TextBack()
    {
        goldText.color = goldColor;
    }
    public void UpdateState()
    {
        
    }
    public void UpLevel()
    {
        exp -= baseExp * Level;
        Level++;
        playerHP += 10;
        maxHP += 10;
        currentHP = maxHP;
        maxMP += 5;
        currentMP = maxMP;
        ATK += 2;
        DEF += 1;
    }
}

