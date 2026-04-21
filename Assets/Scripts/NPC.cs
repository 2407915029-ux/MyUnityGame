using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string Name = "村民";
    public string Content = "感谢您勇士，击杀了很多石头人！现在需要到我们的酒馆休息吗？";
    public int QuestID = 1001;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float dis = Vector3.Distance(player.position, transform.position);

        if (dis < 4 && Input.GetKeyDown(KeyCode.F))
        {
            UIManager.Instance.Show(Name, Content, QuestID);
        }
    }
}