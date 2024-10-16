using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject truck;
    public GameObject trailer;

    public bool trailerDestroyed = false;

    private void Awake()
    {
        Instance = this;
        trailer = GameObject.FindWithTag("Trailer");
        truck = GameObject.FindWithTag("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
