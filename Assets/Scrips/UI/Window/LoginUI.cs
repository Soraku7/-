using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoginUI : UIBase
{
    private void Awake()
    {
        //开始游戏
        Register("bg/startBtn").onClick = onStartGameBtn;
        Register("bg/quitBtn").onClick = onExitGameBtn;
    }

    private void onStartGameBtn(GameObject obj , PointerEventData pData)
    {
        //关闭login界面
        Close();

        //战斗初始化
        FightManager.Instance.ChangeType(FightType.Init);
    }
    
    private void onExitGameBtn(GameObject obj , PointerEventData pData)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
