using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;
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
        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToDest();
        AttackPlayer();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(target.tag))
        {
            targetInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(target.tag) && targetInRange)
        {
            targetInRange = false;
        }
    }

    void MoveToDest()
    {
        if (Time.time > pathNextUpdate)
        {
            pathNextUpdate = Time.time;
            print("set dest");
            agent.destination = target.position;
        }
    }

    void AttackPlayer()
    {
        if (targetInRange && Time.time > attackNextUpdate) 
        {
                attackNextUpdate = Time.time + attackCoolDown;
                Vector3 dir = target.root.position - transform.position;
                GameObject go = Instantiate(bombPrefab, transform.position, Quaternion.LookRotation(dir));
                Rigidbody rb = go.GetComponent<Rigidbody>();
                rb.AddForce(dir * 50);
        }
    }
}
