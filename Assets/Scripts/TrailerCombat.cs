using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerCombat : MonoBehaviour, IDamage
{

    [SerializeField] float trailerHP = 10f;
    [SerializeField] GameObject explosionPrefab;
    Coroutine damageCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        if (damageCoroutine == null)
        {
            damageCoroutine = StartCoroutine(DamageCoroutine(2, 1));
        }
        if (trailerHP > 0f)
        {
        
            trailerHP -= amount;
        }
        if (trailerHP <= 0)
        {
            Camera.main.GetComponent<Follow>().index = 1;
            GameManager.Instance.trailerDestroyed = true;
            GameObject explosion =Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(explosion, 5);
            Destroy(gameObject);
            //move camera foward
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


}
