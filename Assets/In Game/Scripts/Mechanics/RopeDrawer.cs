using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeDrawer : MonoBehaviour
{
    LineRenderer line;


    private void Start()
    {
        line = GetComponent<LineRenderer>();
    }
    public void DrawRope(GameObject barrel)
    {
        line.positionCount = 2;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, barrel.transform.position);        
    }

    public void ResetRope()
    {
        line.positionCount = 0;
    }
}
