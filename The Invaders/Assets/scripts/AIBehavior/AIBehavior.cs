using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehavior : MonoBehaviour
{

    //[SerializeField]
    public float FollowDistance = 0.5f;

    CircleCollider2D cc;
    Rigidbody2D rb;

    /// <summary>
    /// The velocity that should be used for movement AI code. For 2D chars this velocity will be on 
    /// the X/Y plane. For 3D grounded characters this velocity will be on the X/Z plane but will be
    /// applied on whatever plane the character is currently moving on. For 3D flying characters the
    /// velocity will be in full 3D (X/Y/Z).
    /// </summary>
    public Vector3 Velocity
    {
        get
        {
            return rb.velocity;
        }

        set
        {
            rb.velocity = value;
        }
    }

    /// <summary>
    /// Gets the position of the collider (which can be offset from the transform position).
    /// </summary>
    public Vector3 ColliderPosition
    {
        get
        {
            return transform.TransformPoint(cc.offset);
        }
    }

    void Awake() {
        cc = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DebugDraw());
    }

    int count = 0;
    int countDebug = 0;
    IEnumerator DebugDraw()
    {
        yield return new WaitForFixedUpdate();

        Vector3 origin = ColliderPosition;
        Debug.DrawLine(origin, origin + (Velocity.normalized), Color.red, 0f, false);

        count++;
        countDebug = 0;
        StartCoroutine(DebugDraw());
    }
    // Update is called once per frame
    void Update()
    {
        AIAction[] actions = GetComponents<AIAction>();
        AIAction mostDesired = null;
        foreach (AIAction action in actions)
        {
            if(action.Desire() > mostDesired?.Desire() ?? 0)
            {
                mostDesired = action;
            }
        }
        mostDesired?.Execute();
    }
}
