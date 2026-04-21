using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private Image dialog;
    private Image hpBar;
    private Player player;
    private int questid;

    void Awake()
    {
        Instance = this;
        hpBar = transform.Find("Head").Find("HpBar").GetComponent<Image>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        dialog = transform.Find("Dialog").GetComponent<Image>();
        dialog.gameObject.SetActive(false);
    }

    void Update()
    {
        hpBar.fillAmount = (float)player.Hp / player.MaxHp;
    }

    // 显示对话框，参数为对话标题、内容、相关的任务
    public void Show(string name, string content, int id = -1)
    {
        Cursor.lockState = CursorLockMode.None;
        dialog.gameObject.SetActive(true);
        dialog.transform.Find("NameText").GetComponent<Text>().text = name;
        questid = id;
        if (QuestManager.Instance.HasQuest(id))
        {
            dialog.transform.Find("ContentText").GetComponent<Text>().text = "你已经接受该任务了";
        }
        else
        {
            dialog.transform.Find("ContentText").GetComponent<Text>().text = content;
        }
    }

    // 将AcceptButton的鼠标单击事件设置为该方法
    public void AcceptButtonClick()
    {
        dialog.gameObject.SetActive(false);
        QuestManager.Instance.AddQuest(questid);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // 将CancelButton的鼠标单击事件设置为该方法
    public void CancelButtonClick()
    {
        dialog.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}