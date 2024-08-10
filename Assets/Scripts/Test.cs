using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Test : SingleMonoBase<Test>
{
    void Start()
    {
        MonoManager.INSTANCE.AddUpdateAction(Task);
        MonoManager.INSTANCE.AddFixedUpdateAction(Task2);
    }

    void Task()
    {
        Debug.Log("update任务被执行");
    }

    void Task2()
    {
        Debug.Log("fixedupdate任务被执行");
    }
}
