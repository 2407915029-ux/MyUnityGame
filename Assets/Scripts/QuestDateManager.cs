using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class QuestData
{
    public int id;
    public string name;
    public int enemyId;
    public int count;
    public int currentCount;
    public int money;
}


public class QuestDateManger : MonoBehaviour
{
    private static QuestDateManger instance;
    public static QuestDateManger Instance
    {
        get
        {
            if(instance ==null)
            {
                instance = new QuestDateManger();
            }
            return instance;
        }
    }

    public Dictionary<int, QuestData> QuestDic = new Dictionary<int, QuestData>();

    private QuestDateManger ()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Application.dataPath + "/quest.xml");
        XmlElement rootEle = doc.LastChild as XmlElement;
        foreach (XmlElement questEle in rootEle)
        {
            QuestData qd = new QuestData();
            qd.id = int.Parse(questEle.GetElementsByTagName("id")[0].InnerText);
            qd.name = questEle.GetElementsByTagName("name")[0].InnerText;
            qd.enemyId =int.Parse(questEle.GetElementsByTagName("enemyId")[0].InnerText);
            qd.count = int.Parse(questEle.GetElementsByTagName("count")[0].InnerText);
            QuestDic.Add(qd.id, qd);

        }
    }
}
