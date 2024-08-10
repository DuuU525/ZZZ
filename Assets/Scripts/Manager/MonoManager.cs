using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// update托管执行器
/// </summary>
public class MonoManager : SingleMonoBase<MonoManager>
{
    //update任务集合
    public Action updateAction;
    //fixedupdate集合
    public Action fixedUpdateAction;
    //lateupdate集合
    public Action lateUpdateAction;
    /// <summary>
    /// 添加update任务
    /// </summary>
    /// <param name="task">任务</param>
    public void AddUpdateAction(Action task)
    {
        updateAction += task;
    }

    /// <summary>
    /// 移除update任务
    /// </summary>
    /// <param name="task">任务</param>
    public void RemoveUpdateAction(Action task)
    {
        updateAction -= task;
    }
    /// <summary>
    /// 添加fixedUpdate任务
    /// </summary>
    /// <param name="task">任务</param>
    public void AddFixedUpdateAction(Action task)
    {
        fixedUpdateAction += task;
    }
    /// <summary>
    /// 移除fixedUpdate任务
    /// </summary>
    /// <param name="task">任务</param>
    public void RemoveFixedUpdateAction(Action task)
    {
        fixedUpdateAction -= task;
    }

    /// <summary>
    /// 添加lateupdate任务
    /// </summary>
    /// <param name="task">任务</param>
    public void AddLateUpdateAction(Action task)
    {
        lateUpdateAction += task;
    }

    /// <summary>
    /// 移除lateupdate任务
    /// </summary>
    /// <param name="task">任务</param>
    public void RemoveLateUpdateAction(Action task)
    {
        lateUpdateAction -= task;
    }

    void Update()
    {
        updateAction?.Invoke();
    }

    void FixedUpdate()
    {
        fixedUpdateAction?.Invoke();
    }
    void LateUpdate()
    {
        lateUpdateAction?.Invoke();
    }
}
