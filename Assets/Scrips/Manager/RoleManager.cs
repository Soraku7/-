using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用户信息管理器(拥有卡牌信息  金币)
public class RoleManager
{
    public static RoleManager Instance = new RoleManager();

    public List<string> cardList;//存储拥有的卡牌ID
    public void Init()
    {
        cardList = new List<string>();
        //四张攻击卡   四张防御卡  两张效果卡
        cardList.Add("1000");
        cardList.Add("1000");
        cardList.Add("1000");
        cardList.Add("1000");

        cardList.Add("1001");
        cardList.Add("1001");
        cardList.Add("1001");
        cardList.Add("1001");

        cardList.Add("1002");
        cardList.Add("1002");
    }
}
