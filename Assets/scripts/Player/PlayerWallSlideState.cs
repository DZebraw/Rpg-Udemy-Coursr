using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //滑墙的时候跳跃进入 从墙跳跃状态
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJump);
            return;//加入return后，就不会执行后面的代码，不然在同一框架下会混乱
        }

        //如果滑墙中向另一个方向移动，就会取消状态
        if (xInput != 0 && player.facingDir != xInput)
                stateMachine.ChangeState(player.idleState);

        if (yInput < 0)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y * .7f);

        //如果滑到地上，就会取消状态
        if (player.IsGroundDetected())
                stateMachine.ChangeState(player.idleState);
    }
}