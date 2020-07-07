using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    public GameObject target;

    public bool Position = true;
    public bool Rotation = true;
    public bool Local = true;

    //public Vector3 Offset;

    public Transform OffsetObject;
    public float OffsetMultiplier = 0;

    public float Multiplier = 1;
    // Start is called before the first frame update
    void Start()
    {
        if(OffsetObject == null)
        {
            OffsetObject = target.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Local)
        {
            if (Position)
            {
                transform.localPosition = (target.transform.localPosition * Multiplier) + (OffsetObject.localPosition * OffsetMultiplier);
            }
            if (Rotation)
            {
                transform.localRotation = target.transform.localRotation;
            }
        }
        else
        {
            if (Position)
            {
                transform.position = (target.transform.position * Multiplier) + (OffsetObject.position * OffsetMultiplier);
            }
            if (Rotation)
            {
                transform.rotation = target.transform.rotation;
            }
        }
    }
}
