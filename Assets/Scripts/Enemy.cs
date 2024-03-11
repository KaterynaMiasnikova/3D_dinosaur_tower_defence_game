using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    //public Material objectMaterial; // Reference to the object's material
    public Transform shootElement;
    public GameObject bullet;
    public GameObject Enemybug;
    public int Creature_Damage = 10;
    public float Speed;
    // 
    public int moneyForDeath;
    public Transform[] waypoints;
    int curWaypointIndex = 0;
    public float previous_Speed;
    public Animator anim;
    public EnemyHp Enemy_Hp;
    public Transform target;
    public GameObject EnemyTarget;
    GameManager gm;
    bool payedMoney;
    int initialHP;

    void Start()
    {
        payedMoney = false;
        gm = GameObject.FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
        Enemy_Hp = Enemybug.GetComponent<EnemyHp>();
        initialHP = gm.GetEnemyHP();
        Enemy_Hp.EnemyHP = gm.GetEnemyHP();
        moneyForDeath = gm.GetEnemyHP() / 3;
        previous_Speed = Speed;
        anim.SetBool("isWalking", true);
        Speed = (float)(Speed * gm.GetSpeedMultiplier());
        gameObject.tag = "Enemy";
    }

    // Attack
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Castle")
        {
            Speed = 0;
            EnemyTarget = other.gameObject;
            target = other.gameObject.transform;
            Vector3 targetPosition = new Vector3(EnemyTarget.transform.position.x, transform.position.y, EnemyTarget.transform.position.z);
            transform.LookAt(targetPosition);
            //anim.SetBool("RUN", false);
            anim.SetBool("isWalking", false);
            //anim.SetBool("Attack", true);
            anim.SetBool("isAttacking", true);
            gm.PlayDinoAttack();
        }
    }

    // Attack
    void Shooting()
    {
        //if (EnemyTarget)
        // {           
        GameObject с = GameObject.Instantiate(bullet, shootElement.position, Quaternion.identity) as GameObject;
        с.GetComponent<EnemyBullet>().target = target;
        с.GetComponent<EnemyBullet>().twr = this;
        // }  
    }

    void GetDamage()
    {
        EnemyTarget.GetComponent<TowerHP>().Dmg_2(Creature_Damage);
    }

    void Update()
    {
        //Debug.Log("Animator  " + anim);
        // MOVING
        if (curWaypointIndex < waypoints.Length)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[curWaypointIndex].position, Time.deltaTime * Speed);
            if (!EnemyTarget)
            {
                transform.LookAt(waypoints[curWaypointIndex].position);
            }
            if (Vector3.Distance(transform.position, waypoints[curWaypointIndex].position) < 0.5f)
            {
                curWaypointIndex++;
            }
        }
        else
        {
            //anim.SetBool("Victory", true);  // Victory
            anim.SetBool("isRoaring", true);  // Victory
        }

        if (Enemy_Hp.EnemyHP < initialHP * 0.75)
        {
            UpdateColour(new Color(1.0f, 1.0f, 0.0f));
        }
        if (Enemy_Hp.EnemyHP < initialHP * 0.5)
        {
            UpdateColour(new Color(1.0f, 0.66f, 0.0f));
        }
        if (Enemy_Hp.EnemyHP < initialHP * 0.25)
        {
            UpdateColour(new Color(1.0f, 0.33f, 0.0f));
        }
        // DEATH
        if (Enemy_Hp.EnemyHP <= 0)
        {
            Speed = 0;
            anim.SetBool("isRoaring", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isDead", true);
            UpdateColour(new Color(1.0f, 0.0f, 0.0f));
            Destroy(gameObject, 5f);
            if (!payedMoney)
            {
                gm.UpdateMoney(moneyForDeath);
                gm.PlayDinoDead();
                payedMoney = true;
                gm.AddDeadEnemie();

            }
            // anim.SetBool("Death", true);
        }
    }
    void UpdateColour(Color newColor)
    {
        Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            foreach (Material material in renderer.materials)
            {
                // Tint each material by setting its color
                material.color = newColor;
            }
        }
    }
}
// Attack to Run
        /*if (EnemyTarget)
        {
            if (EnemyTarget.CompareTag("Castle_Destroyed")) // get it from BuildingHp
            {
                //anim.SetBool("Attack", false);
                anim.SetBool("isAttacking", false);
                //anim.SetBool("RUN", true);
                anim.SetBool("isWalking", true);
                Speed = previous_Speed;
                EnemyTarget = null;
            }
        }*/