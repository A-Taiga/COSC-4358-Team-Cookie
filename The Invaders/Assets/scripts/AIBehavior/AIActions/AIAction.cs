using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAction : MonoBehaviour
{
    protected float desire;

    public float Desire()
    {
        return desire;
    }

    public virtual void Execute()
    {
        Debug.Log("AIAction.Execute() called");
    }
}
