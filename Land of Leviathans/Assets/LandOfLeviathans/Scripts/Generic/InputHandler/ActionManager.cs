using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{

    public ActionInput GetAction(StateManager st)
    {
        ActionInput r = ActionInput.none;

        if (st.rb)
        {
            return ActionInput.rb;
        }
        if (st.rt)
        {
            return ActionInput.rt;
        }
        if (st.lb)
        {
            return ActionInput.lb;
        }
        if (st.lt)
        {
            return ActionInput.lt;
        }

    }

}

public enum ActionInput
{
    none,rb,lb,rt,lt
}
    
[System.Serializable]
public class Action
{

}