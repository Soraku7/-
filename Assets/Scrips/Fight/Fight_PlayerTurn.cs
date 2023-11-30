using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_PlayerTurn : FightUnit
{
    public override void Init()
    {
        Debug.Log("PlayerTime");
        UIManager.Instance.ShowTip("玩家回合", Color.green, delegate ()
        {
            //回复行动力
            FightManager.Instance.CurPowerCount = 3;
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();
            //卡堆已经没有卡  重新初始化
            if(FightCardManager.Instance.HasCard() == false)
            {
                FightCardManager.Instance.Init();

                //更新弃牌堆的数量
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
            }
            //抽牌
            Debug.Log("抽牌");
            UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(4);//抽四张牌
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
        }
        );
    }

    public override void OnUpdate()
    {
    }
}
