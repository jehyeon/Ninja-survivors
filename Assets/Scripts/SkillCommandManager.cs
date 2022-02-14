using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCommandManager
{
    private Dictionary<string, ISkillCommand> commandDic = new Dictionary<string, ISkillCommand>();

    public void SetSkillCommand(string name, ISkillCommand skillCommand)
    {
        if (commandDic.ContainsValue(skillCommand))
        {
            // 이미 스킬이 있는 경우
            return;
        }
        commandDic.Add(name, skillCommand);
    }

    public void InvokeExecute(string name)
    {
        commandDic[name].Execute();
    }
}
