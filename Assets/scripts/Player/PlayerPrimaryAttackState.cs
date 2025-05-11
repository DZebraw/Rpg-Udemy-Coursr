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
        xInput = 0;

        //当第三次攻击后或者长时间没有攻击则从头开始攻击
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;

        player.anim.SetInteger("ComboCounter", comboCounter);
        //player.anim.speed = 1.2f;


        //可以打到一半的时候切换方向继续打
        float attackDir = player.facingDir;

        if (xInput != 0)
            attackDir = xInput;

        //随着不同的攻击模式，移动的距离也不同
        player.SetVelocity(player.attackMovement[comboCounter] * attackDir, rb.velocity.y);

        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();
         
        player.StartCoroutine("BusyFor", .15f);
        //player.anim.speed = 1;

        comboCounter++;
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        //刚开始攻击时stateTimer为正数会逐渐递减，如果在正数的时候按AD，可以在攻击的时候移动
        if (stateTimer < 0)
            player.SetZeroVelocity();

        //触发器一开始是 false，在动画播放完之后 执行AnimationTrigger里的方法：让触发器变为true，转为待机状态
        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
