using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        //在空中的时候检测到墙面 则 执行滑墙动作
        if (player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlide);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
        
        //使在空中的时候可以左右移动
        if (xInput != 0)
            player.SetVelocity(player.moveSpeed * .8f * xInput, rb.velocity.y);
    }
}
