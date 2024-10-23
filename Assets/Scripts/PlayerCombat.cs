using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerCombat : MonoBehaviour, IDamage
{
    public static PlayerCombat Instance;

    public float playerHP = 10f;
    [SerializeField] float playerMaxHP = 10f;
    //bool enemyInRange = false;
    [SerializeField] List<EnemyAI> enemyList;
    [SerializeField] GameObject bombPrefab;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] List<Transform> enemyTargets;
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip truckSound;
    [SerializeField] GameObject flameBurstLeft;
    [SerializeField] GameObject flameBurstRight;
        
    public Transform enemyTarget;
    public Transform truck;
    public bool isAlive = true;

    Coroutine attackCoroutine;
    Coroutine damageCoroutine;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerHP = playerMaxHP;
        enemyList = new List<EnemyAI>();
        enemyTarget = enemyTargets[0];
    }

    // Update is called once per frame
    void Update()
    {
        FlameBurstAttack();
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
        aud.PlayOneShot(truckSound);
        yield return new WaitForSeconds(2);
        //fire at enemy that is in range
        if (enemyList.Count > 0)
        {
            Transform target = enemyList[0].transform;
            Vector3 dir = target.position - transform.position;
            GameObject go = Instantiate(bombPrefab, transform.position, Quaternion.LookRotation(dir));
            PlayerBomb bomb = go.GetComponent<PlayerBomb>();
            bomb.SetTarget(target);
            //Rigidbody rb = go.GetComponent<Rigidbody>();
            //rb.AddForce(dir * 50);
        }
        attackCoroutine = null;
    }

    public void TakeDamage(float amount)
    {
        if(damageCoroutine == null) 
            damageCoroutine = StartCoroutine(DamageCoroutine(3,1));
       
        if (playerHP > 0)
        {
            playerHP -= amount;
            GameManager.Instance.playerHPBar.value = playerHP / playerMaxHP;
        }
        if (playerHP <= 0)
        {
            //you lose
            GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(explosion, 5);
            isAlive = false;
            
        }
    }

    IEnumerator DamageCoroutine(float strength, float duration)
    {
        float curTime = 0;
        while (curTime < duration)
        {
            PlatformController.singleton.Surge = Random.Range(-strength, strength);
            PlatformController.singleton.Sway = Random.Range(-strength, strength);
            curTime += Time.deltaTime;
            yield return null;
        }
        PlatformController.singleton.Surge = 0;
        PlatformController.singleton.Sway = 0;
        damageCoroutine = null;
    }

    void ChangeEnemyTarget()
    {
        if (GameManager.Instance.trailerDestroyed)
        {
            enemyTarget = enemyTargets[1];
        }
    }

    void FlameBurstAttack()
    {
        if (!GameManager.Instance.trailerDestroyed)
        {
            if (Input.GetButton("Fire2"))
            {
                if (!flameBurstLeft.activeSelf && !flameBurstRight.activeSelf)
                {
                    flameBurstLeft.SetActive(true);
                    flameBurstRight.SetActive(true);
                }
            }
            else
            {
                if (flameBurstLeft.activeSelf && flameBurstRight.activeSelf)
                {
                    flameBurstLeft.SetActive(false);
                    flameBurstRight.SetActive(false);
                }
            }
        }
    }
}
