using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敌人管理器
/// </summary>
public class EnemyManager
{
    public static EnemyManager Instance = new EnemyManager();

    private List<Enemy> enemyList;//储存战斗中的敌人

    //加载敌人资源
    // param name = "id"
    public void LoadRes(string id)
    {
        enemyList = new List<Enemy>();
        //10003	3	10001=10002=10003	3,0,1=0,0,1=-3,0,1	
        //Id Name    EnemyIds Pos
        //读取关卡表
        Dictionary<string , string> levelData = GameConfigManager.Instance.GetLevelById(id);

        //读取敌人ID
        string[] enemyIds = levelData["EnemyIds"].Split('=');

        string[] enemyPos = levelData["Pos"].Split('=');//敌人位置
        for(int i = 0; i < enemyIds.Length; i++)
        {
            string enemyId = enemyIds[i];
            string[] posArr = enemyPos[i].Split(',');
            //敌人位置
            float x = float.Parse(posArr[0]);
            float y = float.Parse(posArr[1]);
            float z = float.Parse(posArr[2]);
            //根据敌人ID 获取单个敌人的信息
            Dictionary<string , string> enemyData = GameConfigManager.Instance.GetEnemyById(enemyId);
        
            GameObject obj = Object.Instantiate(Resources.Load(enemyData["Model"])) as GameObject;//从资源加载对应敌人
            
            Enemy enemy = obj.AddComponent<Enemy>();

            enemy.Init(enemyData);//存储敌人信息

            //存储到集合
            enemyList.Add(enemy);
            
            obj.transform.position = new Vector3(x, y, z);
        
        }
    }

    //移除敌人
    public void DeleteEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);

        //后续做是否击杀所有怪物i
        if(enemyList.Count == 0)
        {
            FightManager.Instance.ChangeType(FightType.Win);
        }
    }

    //执行活着的怪行为
    public IEnumerator DoAllEnemyAction()
    {
        for(int i = 0; i < enemyList.Count; i++)
        {
            yield return FightManager.Instance.StartCoroutine(enemyList[i].DoAction());
        }

        //行动后更新所有敌人的行为
        for(int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].SetRandomAction();
        }

        //切换到玩家回合
        FightManager.Instance.ChangeType(FightType.Player);
    }
}
