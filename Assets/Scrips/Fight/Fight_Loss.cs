using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//失败
public class Fight_Loss : FightUnit
{
    public override void Init()
    {
        Debug.Log("游戏失败");
        FightManager.Instance.StopAllCoroutines();

        //显示失败界面
        UIManager.Instance.ShowTip("游戏失败", Color.yellow, delegate ()
        {
            //返回初始页面
            UIManager.Instance.ShowUI<LoginUI>("LoginUI");
        }
);
    }

    public override void OnUpdate()
    {
    }
}
