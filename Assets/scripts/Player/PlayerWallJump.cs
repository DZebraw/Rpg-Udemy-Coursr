using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 1f;
        player.SetVelocity(5 * -player.facingDir ,  player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        //墙跳持续时间结束 切换空中状态
        if (stateTimer < 0)
            stateMachine.ChangeState(player.airState);

        //检测到地面也会直接进入待机状态
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
    }
}
