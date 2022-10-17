using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [SerializeField] public float offset = 180.0f;
    [SerializeField] private float forwardSpeed = 50.0f;
    [SerializeField] private float jumpForce = 100.0f;
    public Vector3 startPos;
    private int Xline;
    private bool up;

    private float stopY;

    private Animator animator;
    public AnimationClip run;
   
    // Start is called before the first frame update
    void Start()
    {
        Xline = 1;
        up = false;
        startPos = transform.position;

        animator = GetComponent<Animator>();
        animator.Play("run");

    }

    // Update is called once per frame
    void Update()
    {
     
        bool isDie = this.gameObject.GetComponent<PlayerInteraction>().isDie;
        if (isDie) return;
        transform.Translate(0, 0, forwardSpeed * Time.fixedDeltaTime);
        if (SwipeDetection.swipeRight && Xline < 2)
        {
            Xline++;
            transform.position += Vector3.right * Time.fixedDeltaTime * offset;
            return;
        }
        if (SwipeDetection.swipeLeft && Xline > 0)
        {
            Xline--;
            transform.position += Vector3.left * Time.fixedDeltaTime * offset;
            return;
        }
        if (SwipeDetection.swipeUp && !up)
        {
            animator.SetBool("run", false);
            animator.SetBool("jump", true);
            stopY = transform.position.y;
            up = true;
            StartCoroutine(Jump());          
            return;
        }
        if (SwipeDetection.swipeDown && up)
        {

            Vector3 newPos = transform.position;
            newPos.y = stopY;
            transform.position = newPos;
            up = false;
            return;
        }
    }
    public float getOffset()
    {
        return offset;
    }
    public IEnumerator Jump()
    {


        transform.position += Vector3.up * Time.fixedDeltaTime * jumpForce;
        yield return new WaitForSeconds(0.5f);
        if (transform.position.y!=stopY)
        {
            Vector3 newPos = transform.position;
            newPos.y = stopY;
            transform.position = newPos;
            up = false;
        }
        animator.SetBool("run", true);
        animator.SetBool("jump", false);
    }
}
