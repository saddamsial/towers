using System;
using System.Collections.Generic;
using Utils;
[Serializable]
public struct VisibilityObj
{
    public ConditionType conditionType;
    public Visibility visibility;

    public VisibilityObj(ConditionType conditionType, Visibility visibility) : this()
    {
        this.conditionType = conditionType;
        this.visibility = visibility;
    }
}
[Serializable]
public class VisibilityManager : Singleton<VisibilityManager>
{

    public List<VisibilityObj> visibilityObjs = new();

    public void AddToList(ConditionType t, Visibility v)
    {
        visibilityObjs.Add(new VisibilityObj(t, v));
    }
    public void RemoFromList(Visibility v)
    {
        if (CheckContains(v).visibility == null) return;
        visibilityObjs.Remove(CheckContains(v));
    }
    public VisibilityObj CheckContains(Visibility v)
    {
        foreach (var item in visibilityObjs)
        {
            if (item.visibility == v)
                return item;
        }
        return new VisibilityObj(ConditionType.none, null);
    }
}
