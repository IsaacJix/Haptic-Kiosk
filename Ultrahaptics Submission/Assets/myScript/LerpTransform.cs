using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTransform : MonoBehaviour
{
    public enum TransformSel { Position, LocalPosition, Rotation, LocalRotation, LocalScale};
    public TransformSel Transform;

    [Range(0, 1)]
    public float Speed = 0.1f;

    public bool Loop = true;
    public bool Reset = false;

    public List<Vector3> Frames;

    public int CurrentFrame = 0;
    private float CurrentTime = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        if (Reset)
        {
            if (Transform == TransformSel.LocalRotation)
            {
                transform.localRotation = Quaternion.Euler(Frames[0]);
                CurrentFrame = 0;
            }
            if (Transform == TransformSel.LocalScale)
            {
                transform.localScale = Frames[0];
                CurrentFrame = 0;
            }
            if (Transform == TransformSel.LocalPosition)
            {
                transform.localPosition = Frames[0];
                CurrentFrame = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Transform == TransformSel.LocalRotation)
        {
            if (CurrentFrame < Frames.Count -1)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(Frames[CurrentFrame + 1]), Speed);

                if (Vector3.Distance(transform.localEulerAngles, Frames[CurrentFrame + 1]) < Vector3.Distance(Frames[CurrentFrame], Frames[CurrentFrame + 1]) * 0.1f)
                {
                    CurrentFrame += 1;
                }
            }
            else if(Loop)
            {
                if (Reset)
                {
                    transform.localRotation = Quaternion.Euler(Frames[0]);
                    CurrentFrame = 0;
                }
                else
                {
                    transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(Frames[0]), Speed);

                    if (Vector3.Distance(transform.localEulerAngles, Frames[0]) < Vector3.Distance(Frames[CurrentFrame], Frames[0]) * 0.1f)
                    {
                        CurrentFrame = 0;
                    }
                }
            }
            else if (Reset)
            {
                transform.localRotation = Quaternion.Euler(Frames[0]);
                CurrentFrame = 0;
            }
        }
        if (Transform == TransformSel.LocalScale)
        {
            if (CurrentFrame < Frames.Count - 1)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, Frames[CurrentFrame + 1], Speed);

                if (Vector3.Distance(transform.localScale, Frames[CurrentFrame + 1]) < Vector3.Distance(Frames[CurrentFrame], Frames[CurrentFrame +1]) * 0.1f)
                {
                    CurrentFrame += 1;
                }
            }
            else if (Loop)
            {
                if (Reset)
                {
                    transform.localScale = Frames[0];
                    CurrentFrame = 0;
                }
                else
                {
                    transform.localScale = Vector3.Lerp(transform.localScale, Frames[0], Speed);

                    if (Vector3.Distance(transform.localScale, Frames[0]) < Vector3.Distance(Frames[CurrentFrame], Frames[0]) * 0.1f)
                    {
                        CurrentFrame = 0;
                    }
                }
            }
            else if (Reset)
            {
                transform.localScale = Frames[0];
                CurrentFrame = 0;
            }
        }
        if (Transform == TransformSel.LocalPosition)
        {
            if (CurrentFrame < Frames.Count - 1)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, Frames[CurrentFrame + 1], Speed);

                if (Vector3.Distance(transform.localPosition, Frames[CurrentFrame + 1]) < Vector3.Distance(Frames[CurrentFrame], Frames[CurrentFrame + 1]) * 0.1f)
                {
                    CurrentFrame += 1;
                }
            }
            else if (Loop)
            {
                if (Reset)
                {
                    transform.localPosition = Frames[0];
                    CurrentFrame = 0;
                }
                else
                {
                    transform.localPosition = Vector3.Lerp(transform.localPosition, Frames[0], Speed);

                    if (Vector3.Distance(transform.localPosition, Frames[0]) < Vector3.Distance(Frames[CurrentFrame], Frames[0]) * 0.1f)
                    {
                        CurrentFrame = 0;
                    }
                }
            }
            else if (Reset)
            {
                transform.localPosition = Frames[0];
                CurrentFrame = 0;
            }
        }
    }
}
