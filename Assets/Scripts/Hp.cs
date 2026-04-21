using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hp : MonoBehaviour
{ 
    private float timer = 0; 

    public void SetText(string text)
    {
        GetComponent<TMP_Text>().text = text; 
    }

    void Update() 
    {
        // 计时器时间增加
        timer += Time.deltaTime; 
        if (timer > 1f)
        {
            Destroy(gameObject); 
        }

        // 移动
        transform.Translate(Vector3.up * Time.deltaTime); 
    }
}