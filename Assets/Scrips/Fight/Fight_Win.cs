using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//胜利
public class Fight_Win : FightUnit
{
    public override void Init()
    {
        Debug.Log("游戏胜利");
        UIManager.Instance.ShowTip("游戏胜利", Color.blue, delegate ()
        {
            //返回初始页面
            UIManager.Instance.ShowUI<LoginUI>("LoginUI");
        }
        );
    }
}
