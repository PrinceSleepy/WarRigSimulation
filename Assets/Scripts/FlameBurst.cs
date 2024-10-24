using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameBurst : MonoBehaviour
{
    public AudioSource aud;
    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        if (aud != null)
        {
            aud.Play();
        }
    }

    private void OnDisable()
    {
        aud.Pause();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(0.05f);
            }
        }
    }
}
