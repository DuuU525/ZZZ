using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateBase : StateBase
{
    //玩家控制器
    protected PlayerController playerController;
    //玩家模型
    protected PlayerModel playerModel;


    public override void Init(IStateMachineOwner owner)
    {
        playerController = (PlayerController)owner;   
        playerModel = playerController.playerModel;
    }
    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void LateUpdate()
    {
    }

    public override void UnInit()
    {
    }

    public override void Update()
    {
    }
}
