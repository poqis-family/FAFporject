using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    public Animator animator;
    public AIPath aiPath;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        aiPath = GetComponent<AIPath>();
        Debug.Log("211111111");

    }

    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("IsOnTrigger", true);
        aiPath.canMove = false;
        aiPath.Teleport(other.GetComponent<Transform>().position);
        Debug.Log("2222222");
    }
}
