using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Transform player;
    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        print(player);
        dir = player.transform.position - transform.position;
        //AudioManger.Instance.PlayBgm("test");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            AudioManager.Instance.StopBgm();
        if (Input.GetKeyDown(KeyCode.P))
            AudioManager.Instance.PlayBgm("test");

        transform.position = player.transform.position - dir;

        print(Input.GetAxis("Mouse X"));
          float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, mouseX * 90 * Time.deltaTime);

    }
}
