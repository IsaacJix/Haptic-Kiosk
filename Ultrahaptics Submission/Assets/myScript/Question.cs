using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour
{
    public List<GameObject> CorrectAnswers, WrongAnswers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject obj in CorrectAnswers)
        {
            if (WrongAnswers.Contains(obj))
            {
                WrongAnswers.Remove(obj);
            }

            if(obj.GetComponent<Answer>() != null)
            {
                obj.GetComponent<Answer>().Correct = true;
            }
        }

        foreach (GameObject obj in WrongAnswers)
        {
            if (obj.GetComponent<Answer>() != null)
            {
                obj.GetComponent<Answer>().Correct = false;
            }
        }
    }
}
