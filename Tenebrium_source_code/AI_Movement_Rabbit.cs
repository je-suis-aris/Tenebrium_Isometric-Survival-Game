using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Movement : MonoBehaviour
{
    Animator animator;

    public float moveSpeed = 0.2f;

    Vector3 stopPosition;

    
    [Header("Setări Durată Mers")]
    public float minWalkTime = 10f; 
    public float maxWalkTime = 20f; 

    [Header("Setări Durată Pauză")]
    public float minWaitTime = 2f;
    public float maxWaitTime = 5f;

    private float walkTime;
    public float walkCounter;
    private float waitTime;
    public float waitCounter;

    int WalkDirection;

    public bool isWalking;

    
    void Start()
    {
        animator = GetComponent<Animator>();

        
        waitTime = Random.Range(minWaitTime, maxWaitTime);
        waitCounter = waitTime;

        
        ChooseDirection();
    }

    
    void Update()
    {
        if (isWalking)
        {
            animator.SetBool("isRunning", true);

            walkCounter -= Time.deltaTime;

            switch (WalkDirection)
            {
                case 0:
                    transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
                case 1:
                    transform.localRotation = Quaternion.Euler(0f, 90, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
                case 2:
                    transform.localRotation = Quaternion.Euler(0f, -90, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
                case 3:
                    transform.localRotation = Quaternion.Euler(0f, 180, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
            }

            if (walkCounter <= 0)
            {
                
                
                

                isWalking = false;
                animator.SetBool("isRunning", false);
                
                
                waitTime = Random.Range(minWaitTime, maxWaitTime);
                waitCounter = waitTime;
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;

            if (waitCounter <= 0)
            {
                ChooseDirection();
            }
        }
    }

    public void ChooseDirection()
    {
        WalkDirection = Random.Range(0, 4);

        isWalking = true;
        
        
        
        walkTime = Random.Range(minWalkTime, maxWalkTime);
        walkCounter = walkTime;
    }
}