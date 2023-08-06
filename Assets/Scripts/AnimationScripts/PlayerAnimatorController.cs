using JetBrains.Annotations;
using Unity.Profiling;
using UnityEngine;
using static PlayerAnimatorController;

public class PlayerAnimatorController : MonoBehaviour, IIsMoving, IIsAttacking
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

    public bool IsMoving { get; set; } = false;
    public bool IsAttacking { get; set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        StateSelector NewState = GetState();
        ChangeAnimationState(NewState);
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

    StateSelector GetState()
    {
        if (IsAttacking) return StateSelector.Attack;
        if (IsMoving) return StateSelector.Run;
        return StateSelector.Idle;
    }
}