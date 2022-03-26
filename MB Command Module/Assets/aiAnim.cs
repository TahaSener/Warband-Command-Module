using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiAnim : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    


    public void walk()
    {
        animator.SetFloat("Speed", 0.5f);
    }
    public void stop()
    {
        animator.SetFloat("Speed", 0);
    }
}
