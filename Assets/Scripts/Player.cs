using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public enum PlayerState
{
    idle,
    run,
    die,
    attack,
    attack2,
}

public class Player : MonoBehaviour
{
    private PlayerState state = PlayerState.idle;
    private Rigidbody rBody;
    private Animator animator;
    public  int MaxHp = 100;
    public  int Hp = 100;
    private List<Transform> fxList;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        fxList = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
        }
        if (Cursor.lockState == CursorLockMode.None)
        {
            return;
        }

        switch (state)
        {
            case PlayerState.idle:
                Rotate();
                Move();
                Attack();
                animator.SetBool("Run", false);
                break;
            case PlayerState.run:
                Rotate();
                Move();
                Attack();
                animator.SetBool("Run", true);
                break;
            case PlayerState.die:
                break;
            case PlayerState.attack:
                break;
            case PlayerState.attack2:
                break;
        }

        Transform fx = null;
        foreach (Transform trans in fxList)
        {
            trans.Translate(Vector3.forward * 20 * Time.deltaTime);
            Collider[] colliders = Physics.OverlapSphere(trans.position, 1f);
            foreach (Collider collider in colliders)
            {
                if (collider.tag == "Enemy")
                {
                    collider.GetComponent<Enemy>().GetDamage(20);
                    fx = trans;
                    GameObject fxPre = Resources.Load<GameObject>("Explosion");
                    GameObject go = Instantiate(fxPre, collider.transform.position, collider.transform.rotation);

                    Destroy(go, 2f);

                    break;
                }
            }
        }

        if (fx != null)
        {
            fxList.Remove(fx);
        }

    }


    void Rotate()
    {
        transform.rotation = Camera.main.transform.parent.rotation;
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(horizontal, 0, vertical);
        if (dir != Vector3.zero)
        {
            rBody.velocity = transform.forward * vertical * 4;
            rBody.velocity += transform.right * horizontal * 2;
            state = PlayerState.run;
        }
        else
        {
            state = PlayerState.idle;
        }

    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
            state = PlayerState.attack;
        }
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("Attack2");
            state = PlayerState.attack2;
        }
    }

    void AttackEnd()
    {
        state = PlayerState.idle;
    }

    // ÊÜµ½¹¥»÷
    public void GetDamage(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            state = PlayerState.die;
            animator.SetTrigger("Die");
            Invoke("Revive", 3f);
        }
    }

    // ¸´»î
    public void Revive()
    {
        if (state == PlayerState.die)
        {
            Hp = MaxHp;
            animator.SetTrigger("Revive");
            state = PlayerState.idle;
            transform.position = transform.position;
        }
    }

    // ¶ÔµÐÈËÔì³ÉÉËº¦
    void Damage(int damage)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3f);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy" && Vector3.Angle(collider.transform.position - transform.position, transform.forward) < 60)
            {
                collider.GetComponent<Enemy>().GetDamage(damage);
            }
        }
    }

    // ÌØÐ§
    Transform FX(string name, float desTime)
    {
        GameObject fxPre = Resources.Load<GameObject>(name);
        GameObject go = Instantiate(fxPre, transform.position, transform.rotation);
        Destroy(go, desTime);
        return go.transform;
    }

    // ¹¥»÷1_1
    void Attack1_1()
    {
        Damage(20);
        FX("fx_hr_arthur_attack_01_1", 0.5f);
    }

    // ¹¥»÷1_2
    void Attack1_2()
    {
        Damage(20);
        FX("fx_hr_arthur_attack_01_2", 0.5f);

        for (int i = 0; i < 5; i++)
        {
            Transform fire = FX("Magicfire prored", 1f);
            fire.transform.rotation = transform.rotation;
            fire.transform.Rotate(fire.transform.up, 15 * i - 30);
            fxList.Add(fire);
            Invoke("ClearFXList", 1f);
        }
    }

    void ClearFXList()
    {
        fxList.Clear();
    }

    // ¹¥»÷2_0
    void Attack2_0()
    {
        FX("fx_hr_arthur_pskil_03_1", 1f);
        FX("RotatorPS2", 4f);
    }

    // ¹¥»÷2_1
    void Attack2_1()
    {
        Damage(80);
        FX("fx_hr_arthur_pskill_01", 1.8f);
    }

    // ¹¥»÷2_2
    void Attack2_2()
    {
        Damage(20);
    }

}


