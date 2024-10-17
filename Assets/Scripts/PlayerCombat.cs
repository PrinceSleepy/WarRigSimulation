using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerCombat : MonoBehaviour, IDamage
{
    public static PlayerCombat Instance;

    [SerializeField] float playerHP = 10f;
    //bool enemyInRange = false;
    [SerializeField] List<EnemyAI> enemyList;
    [SerializeField] GameObject bombPrefab;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] List<Transform> enemyTargets;
    public Transform enemyTarget;
    public Transform truck;
    public bool isAlive = true;

    Coroutine attackCoroutine;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyList = new List<EnemyAI>();
        enemyTarget = enemyTargets[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Attack();
            ChangeEnemyTarget();
        }
    }

    public void AddEnemy(EnemyAI enemy)
    {
        int i = enemyList.IndexOf(enemy);
        if (i == -1)
        {
            enemyList.Add(enemy);
        }
    }
    public void RemoveEnemy(EnemyAI enemy)
    {
        int i = enemyList.IndexOf(enemy);
        if (i > -1)
        {
            enemyList.RemoveAt(i);
        }
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if(attackCoroutine == null)
            attackCoroutine = StartCoroutine(AttackCoroutine());
        }
    }

    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(2);
        //fire at enemy that is in range
        if (enemyList.Count > 0)
        {
            Transform target = enemyList[0].transform;
            Vector3 dir = target.root.position - transform.position;
            GameObject go = Instantiate(bombPrefab, transform.position, Quaternion.LookRotation(dir));
            Rigidbody rb = go.GetComponent<Rigidbody>();
            rb.AddForce(dir * 50);
        }
        attackCoroutine = null;
    }

    public void TakeDamage(int amount)
    {
        if (playerHP > 0)
        {
            playerHP -= amount;
        }
        if (playerHP <= 0)
        {
            //you lose
            GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(explosion, 5);
            isAlive = false;
            
        }
    }

    void ChangeEnemyTarget()
    {
        if (GameManager.Instance.trailerDestroyed)
        {
            enemyTarget = enemyTargets[1];
        }
    }
}
