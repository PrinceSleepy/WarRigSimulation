using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject truck;
    public GameObject trailer;

    public bool trailerDestroyed = false;
    [SerializeField] public GameObject menuWin;
    [SerializeField] public GameObject menuLose;

    public Slider playerHPBar;
    public Slider trailerHPBar;
    public float gameTime = 0;
    [SerializeField] TextMeshProUGUI gameTimeTextField;
    public string finalTime;
    [SerializeField] private TextMeshProUGUI finalTimeTextField;


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
        gameTime += Time.deltaTime;
        TimeSpan formattedTime = TimeSpan.FromSeconds(gameTime);
        gameTimeTextField.text = formattedTime.ToString(@"mm\:ss\:fff");
    }

    public void WinGame()
    {
        finalTime = gameTimeTextField.text;
        gameTimeTextField.gameObject.SetActive(false);
        finalTimeTextField.text = finalTime;
        menuWin.SetActive(true);
    }

    public void LoseGame()
    {
        menuLose.SetActive(true);
    }
}
