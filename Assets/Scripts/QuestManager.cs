using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private static QuestManager instance;
    public static QuestManager Instance
    {
        get
        {
            if(instance ==null)
            {
                instance = new QuestManager();
            }
            return instance;
        }
    }

    private List<QuestData> QuestList = new List<QuestData>();

    public bool HasQuest(int id)
    {
        foreach (QuestData qd in QuestList)
        {
            if(qd.id==id)
            {
                return true;
            }
        }
        return false;
    }

    public void AddQuest(int id)
    {
        if(!HasQuest(id))
        {
            QuestList.Add(QuestDateManger.Instance.QuestDic[id]);
        }
    }
    
    public void AddEnemy(int enemyid)
    {
        for(int i=0;i<QuestList.Count;i++)
        {
            QuestData qd= QuestList[i];
            if(qd.enemyId==enemyid)
            {
                qd.currentCount++;
                if(qd.currentCount >=qd.count )
                {
                    Debug.Log("任务完成");
                    qd.currentCount = 0;
                    QuestList.Remove(qd);
                    GameObject go = Resources.Load<GameObject>("fx_hr_arthur_pskill_03_2");
                    Transform player = GameObject.FindWithTag("Player").transform ;
                    GameObject.Instantiate(go, player.position, player.rotation);
                }
            }
        }
    }

}
