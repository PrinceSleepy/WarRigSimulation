using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Diagnostics;

public class EnemyAI : MonoBehaviour, IDamage
{
    [SerializeField] Transform target;
    [SerializeField] List<Transform> targets;
    NavMeshAgent agent;
    [SerializeField] float enemyHP = 5f;
    float pathNextUpdate = 0;
    [SerializeField] float pathCoolDown = 1; //seconds
    float attackNextUpdate = 0;
    [SerializeField] float attackCoolDown = 1;
    [SerializeField] float attackRange = 0;
    [SerializeField] GameObject bombPrefab;

    bool targetInRange = false;
 


    // Start is called before the first frame update
    void Start()
    {
        target = targets[0];
        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Vector3.Distance(transform.position, target.position) < attackRange)
        //{
        //    targetInRange = true;
        //}
        //else
        //{
        //    targetInRange = false;
        //}

        MoveToDest();
        AttackPlayer();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCombat p = other.transform.root.GetComponent<PlayerCombat>();
            if (p != null)
            {
                p.AddEnemy(this);
            }
        }

        if (other.CompareTag(target.tag))
        {
            targetInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCombat p = other.transform.root.GetComponent<PlayerCombat>();
            if (p != null)
            {
                p.RemoveEnemy(this);
            }
        }
       if (other.CompareTag(target.tag) && targetInRange)
       {
           targetInRange = false;
       }
    }

    void MoveToDest()
    {
        if (GameManager.Instance.trailerDestroyed)
            target = targets[1];
        if (Time.time > pathNextUpdate)
        {
            pathNextUpdate = Time.time;
            agent.destination = target.position;
        }
    }

    void AttackPlayer()
    {
        if (targetInRange && Time.time > attackNextUpdate)
        {
            attackNextUpdate = Time.time + attackCoolDown;
            //Vector3 dir = target.root.position - transform.position;
            Vector3 dir = PlayerCombat.Instance.enemyTarget.position - transform.position;
            GameObject go = Instantiate(bombPrefab, transform.position, Quaternion.LookRotation(dir));
            Rigidbody rb = go.GetComponent<Rigidbody>();
            rb.AddForce(dir * 50);
        }
    }

    public void TakeDamage(int amount)
    {
        enemyHP -= amount;
        if (enemyHP <= 0)
        {
            PlayerCombat.Instance.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }
}

//if (Input.GetKeyDown(KeyCode.Space))
//{
//    TakeDamage(100);
//}