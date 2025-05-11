using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundedState : EnemyState
{
    protected Transform player;
    protected Enemy_Skeleton enemy;

    public SkeletonGroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //在前面被线检测到 或者 离得太近也会进入索敌状态
        //这样不管是在敌人前面还是后面很近都会被敌人发现
        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position,player.position) < 2)
            stateMachine.ChangeState(enemy.battleState);
    }
}
