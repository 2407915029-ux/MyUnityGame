using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    private Player player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.GetComponent<Player>();
            player.Hp += 10;
            if (player.Hp > player.MaxHp)
            {
                player.Hp = player.MaxHp;
            }
            Destroy(gameObject);
        }
    }
}