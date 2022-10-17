using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInteraction : MonoBehaviour
{
    public int coins_count;
    public Text coinsText;

    public bool isDie = false;

    public AudioSource coins;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        coins_count = 0;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Coin"))
        {   
            Destroy(collider.gameObject);
            coins.Play();
            coins_count++;        
            coinsText.text = "Coins:" + coins_count;
        }
        if(collider.gameObject.CompareTag("Obstacle"))
        {
            animator.Play("die");
           
            isDie = true;

        }   
    }

}
