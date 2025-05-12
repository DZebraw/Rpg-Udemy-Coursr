using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{

    private int comboCounter;

    private float lastTimeAttacked;
    private float comboWindow = 2;

    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        xInput = 0;//修复角色攻击与朝向不一致bug
        
        //如果打过最后一击 或者 间隔时间太久 则回到第一击
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;

        player.anim.SetInteger("ComboCounter", comboCounter);
        //player.anim.speed = 1.2f;

        
        float attackDir = player.facingDir;

        if (xInput != 0)
            attackDir = xInput;
        
        //攻击时往前移动一小段
        player.SetVelocity(player.attackMovement[comboCounter] * attackDir, rb.velocity.y);

        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();
         
        //攻击之后busy一段时间，让玩家不会一段攻击刚结束动画还没结束就可以平移
        player.StartCoroutine("BusyFor", .15f);
        //player.anim.speed = 1;

        comboCounter++;
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        
        if (stateTimer < 0)
            player.SetZeroVelocity();
        
        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
