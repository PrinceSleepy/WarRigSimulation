using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerCombat : MonoBehaviour, IDamage
{
    public static PlayerCombat Instance;

    [SerializeField] float playerHP = 10f;
    bool enemyInRange = false;
    [SerializeField] List<EnemyAI> enemyList;
    [SerializeField] GameObject bombPrefab;
    public Transform enemyTarget;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyList = new List<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {

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
        if (enemyInRange && Input.GetButtonDown("Fire1"))
        {
            //fire at enemy that is in range
            if (enemyList.Count > 0)
            {
                Transform target = enemyList[0].transform;
                Vector3 dir = target.root.position - transform.position;
                GameObject go = Instantiate(bombPrefab, transform.position, Quaternion.LookRotation(dir));
                Rigidbody rb = go.GetComponent<Rigidbody>();
                rb.AddForce(dir * 50);
            }
        }
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
        }
    }
}
