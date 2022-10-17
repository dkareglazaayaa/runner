using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject hero;
    public GameObject restart;
    //public GameObject[] scripts;
     private bool isDie;
    void Start()
    {
        hero = GameObject.Find("Hero");
        isDie = false;
    }

    // Update is called once per frame
    void Update()
    {
  
        isDie=hero.GetComponent<PlayerInteraction>().isDie; 
        if (isDie)
        {
            restart.SetActive(true);
            
        }
    }
    public void ResrartGame()
    {
        SceneManager.LoadScene("Level");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level");

    }
    public void Exit()
    {
        Application.Quit(); 
    }
}
