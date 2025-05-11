using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState//继承角色在地上状态
{
    //使用父级PlayerState的构造函数，来完成初始化赋值
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        //执行Player中的 移动 方法
        player.SetVelocity(xInput * player.moveSpeed , rb.velocity.y);

        //如果不动了，或者抵到墙上了，就会待机
        if (xInput == 0 || player.IsWallDetected())
            stateMachine.ChangeState(player.idleState);
    }
}
