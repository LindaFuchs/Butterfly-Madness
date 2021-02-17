using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatMotion : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent navMeshAgent;
    Animator animator;
    ButterMovement butterMovement;
    
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        butterMovement = FindObjectOfType<ButterMovement>();
    }

    void Update()
    {
        navMeshAgent.SetDestination(target.position);
    }

    private void OnCollisionEnter(Collision butterfly)
    {
        if (butterfly.collider.tag == "Butterfly")
        {
            butterMovement.Death();
            StopRunningAnimation_();
        }
    }

    public void SetRunningAnimation_()
    {
        animator.SetBool("Running", true);
    }

    public void StopRunningAnimation_()
    {
        animator.SetBool("Running", false);
    }
}
