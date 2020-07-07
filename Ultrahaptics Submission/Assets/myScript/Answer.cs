using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using InteractionEngineUtility;
using Leap.Unity.Attributes;
using Leap.Unity.Interaction.Internal;
using Leap.Unity.Query;
using Leap.Unity.Space;
public class Answer : MonoBehaviour
{
    public string CorrectAnimation, WrongAnimation;
    public AudioClip CorrectSound, WrongSound;
    public AudioSource PlaySounds;

    public bool Correct;

    public Questions Questions;

    public float WaitTime = 2;
    private float WaitedTime = 0;

    private bool Touched = false;

    void Update()
    {
        if (PlaySounds != null)
        {
            if (Correct)
            {
                if (CorrectSound != null)
                {
                    if (PlaySounds.clip != CorrectSound)
                    {
                        PlaySounds.clip = CorrectSound;

                        if (PlaySounds.gameObject.activeInHierarchy)
                        {
                            PlaySounds.Play();
                        }
                    }
                }
            }
            else
            {
                WaitedTime = 0;
                if (WrongSound != null)
                {
                    if (PlaySounds.clip != WrongSound)
                    {
                        PlaySounds.clip = WrongSound;

                        if (PlaySounds.gameObject.activeInHierarchy)
                        {
                            PlaySounds.Play();
                        }
                    }
                }
            }
        }

        if (Touched)
        {
            TestAnswer();
            if (GetComponent<Leap.Unity.Interaction.InteractionBehaviour>() != null)
            {
                GetComponent<Leap.Unity.Interaction.InteractionBehaviour>().OnContactStay();
            }
        }
    }

    public void TestAnswer()
    {
        if (Correct)
        {
            WaitedTime += Time.deltaTime;
            if (CorrectAnimation.Length > 0)
            {
                if (GetComponent<Animator>().parameters.Length > 0)
                {
                    bool correctValid = false;

                    foreach (AnimatorControllerParameter paramater in GetComponent<Animator>().parameters)
                    {
                        if (paramater.name == CorrectAnimation)
                        {
                            correctValid = true;
                        }
                    }

                    if (correctValid)
                    {
                        GetComponent<Animator>().SetBool(CorrectAnimation, true);
                    }
                    else
                    {
                        GetComponent<Animator>().Play(CorrectAnimation);
                    }
                }
                else
                {
                    GetComponent<Animator>().Play(CorrectAnimation);
                }
            }

            if (WrongAnimation.Length > 0)
            {
                if (GetComponent<Animator>().parameters.Length > 0)
                {
                    bool valid = false;

                    foreach (AnimatorControllerParameter paramater in GetComponent<Animator>().parameters)
                    {
                        if (paramater.name == WrongAnimation)
                        {
                            valid = true;
                        }
                    }

                    if (valid)
                    {
                        GetComponent<Animator>().SetBool(WrongAnimation, false);
                    }
                }
            }

            if (WaitedTime > WaitTime)
            {
                WaitedTime = 0;
                if (Questions != null)
                {
                    Questions.NewQuestion();
                }
            }
        }
        else
        {
            if (CorrectAnimation.Length > 0)
            {
                if (GetComponent<Animator>().parameters.Length > 0)
                {
                    bool correctValid = false;

                    foreach (AnimatorControllerParameter paramater in GetComponent<Animator>().parameters)
                    {
                        if (paramater.name == CorrectAnimation)
                        {
                            correctValid = true;
                        }
                    }

                    if (correctValid)
                    {
                        GetComponent<Animator>().SetBool(CorrectAnimation, false);
                    }
                }
            }

            if (WrongAnimation.Length > 0)
            {
                if (GetComponent<Animator>().parameters.Length > 0)
                {
                    bool valid = false;

                    foreach (AnimatorControllerParameter paramater in GetComponent<Animator>().parameters)
                    {
                        if (paramater.name == WrongAnimation)
                        {
                            valid = true;
                        }
                    }

                    if (valid)
                    {
                        GetComponent<Animator>().SetBool(WrongAnimation, true);
                    }
                    else
                    {
                        GetComponent<Animator>().Play(WrongAnimation);
                    }
                }
                else
                {
                    GetComponent<Animator>().Play(WrongAnimation);
                }
            }
        }
    }

    public void ResetBools()
    {
        if (CorrectAnimation.Length > 0)
        {
            if (GetComponent<Animator>().parameters.Length > 0)
            {
                bool correctValid = false;

                foreach (AnimatorControllerParameter paramater in GetComponent<Animator>().parameters)
                {
                    if (paramater.name == CorrectAnimation)
                    {
                        correctValid = true;
                    }
                }

                if (correctValid)
                {
                    GetComponent<Animator>().SetBool(CorrectAnimation, false);
                }
            }
        }
        if (WrongAnimation.Length > 0)
        {
            if (GetComponent<Animator>().parameters.Length > 0)
            {
                bool valid = false;

                foreach (AnimatorControllerParameter paramater in GetComponent<Animator>().parameters)
                {
                    if (paramater.name == WrongAnimation)
                    {
                        valid = true;
                    }
                }

                if (valid)
                {
                    GetComponent<Animator>().SetBool(WrongAnimation, false);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Touched = true;
        CancelInvoke("ResetTouch");
    }
    private void OnTriggerExit(Collider other)
    {
        Invoke("ResetTouch", 0.1f);
    }

    void ResetTouch()
    {
        CancelInvoke("ResetTouch");
        Touched = false;
        if (GetComponent<Leap.Unity.Interaction.InteractionBehaviour>() != null)
        {
            GetComponent<Leap.Unity.Interaction.InteractionBehaviour>().OnContactEnd();
        }
    }
}
