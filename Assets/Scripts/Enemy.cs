using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int ID = 101;
    public Player player;
    public int Hp = 100;
    public int Attack = 20;
    private float timer = 1;
    private bool isAttack = false;
    private Animator animator;
    private Vector3 position;
    public GameObject PotionPre;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        position = transform.position;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player.Hp <= 0||Hp <= 0)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Attack", false);
            return;
        }

        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance > 7f)
        {
            if (Vector3.Distance(transform.position, position) > 1f)
            {
                transform.LookAt(new Vector3(position.x, transform.position.y, position.z));
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);
                animator.SetBool("Run", true);
            }
            else
            {
                animator.SetBool("Run", false);
            }
        }
        else if (distance > 3f)
        {
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, position.z));
            transform.Translate(Vector3.forward * 2 * Time.deltaTime);
            animator.SetBool("Run", true);
            isAttack = false;
            animator.SetBool("Attack", false);
        }
        else
        {
            animator.SetBool("Run", false);
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            animator.SetBool("Attack", true);
            if (isAttack == false)
            {
                isAttack = true;
                timer = 1;
            }

            timer += Time.deltaTime;

            if (timer >= 2)
            {
                timer = 0;
                player.GetDamage(Attack);
            }
        }
    }


    public void GetDamage(int damage)
    {
        if (Hp > 0) 
        {
            GetComponentInChildren<HpManager>().ShowText("-" + damage);
            Hp -= damage;
            if (Hp <= 0)
            {
                Instantiate(PotionPre, transform.position, transform.rotation);
                animator.SetTrigger("Die");
                QuestManager.Instance.AddEnemy(ID);
                Destroy(gameObject, 2f);
            }
        }
    }



}