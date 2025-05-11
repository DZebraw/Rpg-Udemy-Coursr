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

        //�������ι�������߳�ʱ��û�й������ͷ��ʼ����
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;

        player.anim.SetInteger("ComboCounter", comboCounter);
        //player.anim.speed = 1.2f;


        //���Դ�һ���ʱ���л����������
        float attackDir = player.facingDir;

        if (xInput != 0)
            attackDir = xInput;

        //���Ų�ͬ�Ĺ���ģʽ���ƶ��ľ���Ҳ��ͬ
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

        //�տ�ʼ����ʱstateTimerΪ�������𽥵ݼ��������������ʱ��AD�������ڹ�����ʱ���ƶ�
        if (stateTimer < 0)
            player.SetZeroVelocity();

        //������һ��ʼ�� false���ڶ���������֮�� ִ��AnimationTrigger��ķ������ô�������Ϊtrue��תΪ����״̬
        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
