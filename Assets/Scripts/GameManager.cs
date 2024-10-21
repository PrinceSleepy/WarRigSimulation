using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject truck;
    public GameObject trailer;

    public bool trailerDestroyed = false;
    [SerializeField] public GameObject menuActive;
    [SerializeField] public GameObject menuWin;
    [SerializeField] public GameObject menuLose;

    public Image engine1HPBar;
    public Image engine2HPBar;
    public Image trailerHPBar;


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
