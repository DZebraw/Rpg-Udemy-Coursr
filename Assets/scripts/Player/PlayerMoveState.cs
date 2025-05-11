using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState//�̳н�ɫ�ڵ���״̬
{
    //ʹ�ø���PlayerState�Ĺ��캯��������ɳ�ʼ����ֵ
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

        //ִ��Player�е� �ƶ� ����
        player.SetVelocity(xInput * player.moveSpeed , rb.velocity.y);

        //��������ˣ����ߵֵ�ǽ���ˣ��ͻ����
        if (xInput == 0 || player.IsWallDetected())
            stateMachine.ChangeState(player.idleState);
    }
}
