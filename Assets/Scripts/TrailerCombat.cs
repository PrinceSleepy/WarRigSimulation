using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerCombat : MonoBehaviour, IDamage
{

    [SerializeField] float trailerHP = 10f;
    [SerializeField] GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
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


}
