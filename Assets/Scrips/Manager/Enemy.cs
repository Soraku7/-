using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//敌人行动枚举
public enum ActionType
{
    None,
    Defend,//加防御
    Attack//攻击
}
/// <summary>
/// Enemy敌人脚本
/// </summary>
public class Enemy : MonoBehaviour
{
    protected Dictionary<string, string> data;//敌人数据信息表

    public ActionType type;

    public GameObject hpItemObj;
    public GameObject actionObj;

    //UI相关
    public Transform attackTf;
    public Transform defendTf;
    public Text defendTxt;
    public Text hpTxt;
    public Image hpImg;

    //数值相关
    public int Defend;
    public int Attack;
    public int MaxHp;
    public int CurHp;

    //组件相关
    SkinnedMeshRenderer _meshRenderer;
    public Animator ani;
    public void Init(Dictionary<string, string> data)
    {
        this.data = data;
    }
    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();
        ani = transform.GetComponent<Animator>();

        type = ActionType.None;
        hpItemObj = UIManager.Instance.CreatHpItem();
        actionObj = UIManager.Instance.CreatActionIcon();

        attackTf = actionObj.transform.Find("attack");
        defendTf = actionObj.transform.Find("defend");

        defendTxt = hpItemObj.transform.Find("fangyu/Text").GetComponent<Text>();
        hpTxt = hpItemObj.transform.Find("hpTxt").GetComponent<Text>();
        hpImg = hpItemObj.transform.Find("fill").GetComponent<Image>();
        //设置血条  行动力位置
        hpItemObj.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.down);
        actionObj.transform.position = Camera.main.WorldToScreenPoint(transform.Find("head").position);

        SetRandomAction();

        //初始化数值
        Attack = int.Parse(data["Attack"]);
        CurHp = int.Parse(data["Hp"]);
        MaxHp = CurHp;
        Defend = int.Parse(data["Defend"]);

        UpdateHp();
        UpdateDefend();
    }

    //随机一个行动
    public void SetRandomAction()
    {
        int ran = Random.Range(1, 3);

        type = (ActionType)ran;

        switch (type)
        {
            case ActionType.None:
                break;
            case ActionType.Defend:
                attackTf.gameObject.SetActive(false);
                defendTf.gameObject.SetActive(true);
                break;
            case ActionType.Attack:
                attackTf.gameObject.SetActive(true);
                defendTf.gameObject.SetActive(false);
                break;
        }
    }

    //更新血量信息
    public void UpdateHp()
    {
        hpTxt.text = CurHp + "/" + MaxHp;
        hpImg.fillAmount = (float)CurHp / (float)MaxHp;

    }

    //更新防御信息
    public void UpdateDefend()
    {
        defendTxt.text = Defend.ToString();
    }

    //攻击卡选中 显示红边
    public void OnSelect()
    {
        _meshRenderer.material.SetColor("_OtlColor", Color.red);

    }

    //未选中
    public void OnUnSelect()
    {
        _meshRenderer.material.SetColor("_OtlColor",Color.black);
    }

    //受伤
    public void Hit(int val)
    {
        //先扣护盾
        if(Defend >= val)
        {
            //扣护盾
            Defend -= val;

            //播放受伤
            ani.Play("hit", 0, 0);
        }
        else
        {
            val = val - Defend;
            Defend = 0;
            if(CurHp <= 0)
            {
                CurHp = 0;
                //播放死亡
                ani.Play("die");

                //敌人从列表移除
                EnemyManager.Instance.DeleteEnemy(this);

                Destroy(gameObject, 1);
                Destroy(actionObj);
                Destroy(hpItemObj);

            }
            else
            {
                //受伤
                ani.Play("hit", 0, 0);
            }
        }
        //刷新血量等UI
        UpdateDefend();
        UpdateHp();
    }
    //隐藏怪物头上的行动标志
    public void HideAction()
    {
        attackTf.gameObject.SetActive(false);
        defendTf.gameObject.SetActive(false);
    }
    
    //执行敌人行动
    public IEnumerator DoAction()
    {
        HideAction();

        //播放对应的动画
        ani.Play("attack");
        //等待某一时间的后执行对应的行为

        yield return new WaitForSeconds(0.5f);

        switch (type)
        {
            case ActionType.None:
                break;
            case ActionType.Defend:
                //加防御
                Defend += 1;
                UpdateDefend();
                //可以播放对应的特效
                break;
            case ActionType.Attack:
                //玩家掉血
                FightManager.Instance.GetPlayerHit(Attack);

                //摄像机抖动
                Camera.main.DOShakePosition(0.1f, 0.2f, 5, 45);
                break;
        }

        //等待动画播放完
        yield return new WaitForSeconds(1);
    }
}
