using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    Animator animator;

    [Header("Animation Filenames")]
    [SerializeField] private string Idle;
    [SerializeField] private string Run;
    [SerializeField] private string Attack;

    public enum StateSelector
    {
        Idle,
        Run,
        Attack
    }

    StateSelector currentState;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(StateSelector newState)
    {
        // stop animation from interupting itself
        if (currentState == newState) return;

        switch (newState)
        {
            case StateSelector.Idle:
                animator.Play(Idle);
                break;
            case StateSelector.Run:
                animator.Play(Run);
                break;
            case StateSelector.Attack:
                animator.Play(Attack);
                break;
        }

        currentState = newState;
    }
}
