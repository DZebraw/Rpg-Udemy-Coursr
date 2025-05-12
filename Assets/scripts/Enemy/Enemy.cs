using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("被弹反")]
    public float stunDuration;
    public Vector2 stunDirection;
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterImage;//弹反提示图

    [Header("移动")]
    public float moveSpeed;
    public float idleTime;
    public float battleTime;

    [Header("攻击")]
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector]public float lastTimeAttacked;

    public EnemyStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned = true;
        counterImage.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
        counterImage.SetActive(false);
    }

    public virtual bool CanBeStunned()
    {
        if(canBeStunned)
        {
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 50, whatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }
}
