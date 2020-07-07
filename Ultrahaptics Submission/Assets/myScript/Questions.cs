using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Questions : MonoBehaviour
{
    public GameObject[] AllQuestions, AllAnswers, CorrectMessages, IncorrectMessages;
    public List<GameObject> RemainingQuestions, ResetTimeIfNoneActive;
    public GameObject ActiveQuestion;
    public bool AvoidRepeats = true;

    private float CurrentTime = 0;
    public List<float> Times;

    public Text DisplayTimes;

    public int QuestionsAsked = 0, MaxQuestions = 0;
    public GameObject MaxQuestionsAsked;

    public string SaveFile = "Saved Times.txt";
    private bool NewUser = false;

    void FirstSave()
    {
        using (StreamWriter file = new StreamWriter(SaveFile, append:true))
        {
            file.WriteLine("Saved Times:");
            file.WriteLine("");
            file.WriteLine("------------------------------");
            file.WriteLine("Time is " + System.DateTime.Now);
            file.WriteLine("New User");
        }
    }

    void Start()
    {
        if (DisplayTimes != null)
        {
            DisplayTimes.text += "\n\nNew User";
        }

        if (!File.Exists(SaveFile))
        {
            print("creating new file");
            File.CreateText(SaveFile);

            Invoke("FirstSave", 2);
        }
        else
        {
            print("file already exists");
            using (StreamWriter file = new StreamWriter(SaveFile, append:true))
            {
                file.WriteLine("");
                file.WriteLine("------------------------------");
                file.WriteLine("Time is " + System.DateTime.Now);
                file.WriteLine("New User");
            }
        }

        if (ActiveQuestion == null)
        {
            ActiveQuestion = RemainingQuestions[Random.Range(0, RemainingQuestions.Count)];
        }
    }

    void Update()
    {
        if (ResetTimeIfNoneActive.Count > 0)
        {
            bool reset = true;
            foreach (GameObject obj in ResetTimeIfNoneActive)
            {
                if (obj.activeInHierarchy)
                {
                    reset = false;
                }
            }

            if (reset)
            {
                CurrentTime = 0;
            }
            else
            {
                CurrentTime += Time.deltaTime;
            }
        }
        else
        {
            CurrentTime += Time.deltaTime;
        }

        foreach(GameObject obj in AllQuestions)
        {
            if (obj == ActiveQuestion)
            {
                if (!obj.activeSelf)
                {
                    CurrentTime = 0;
                }
                obj.SetActive(true);
            }
            else
            {
                obj.SetActive(false);
            }
        }
    }

    public void CheckAnswer(GameObject obj)
    {
        var questions = new List<GameObject>(AllQuestions);

        if (NewUser)
        {
            if (File.Exists(SaveFile))
            {
                using (StreamWriter file = new StreamWriter(SaveFile, append: true))
                {
                    file.WriteLine("");
                    file.WriteLine("New User");
                    NewUser = false;
                }
            }
        }

        if (AllAnswers[questions.IndexOf(ActiveQuestion)].gameObject == obj)
        {
            QuestionsAsked += 1;

            if (MaxQuestions > QuestionsAsked || MaxQuestions <= 0)
            {
                CorrectMessages[questions.IndexOf(ActiveQuestion)].gameObject.SetActive(true);
            }

            NewQuestion();
        }
        else
        {
            IncorrectMessages[questions.IndexOf(ActiveQuestion)].gameObject.SetActive(true);
            if (File.Exists(SaveFile))
            {
                using (StreamWriter file = new StreamWriter(SaveFile, append: true))
                {
                    file.WriteLine("Incorrect");
                }
            }
        }
    }

    public void NewQuestion()
    {
        Times.Add(Mathf.Round(CurrentTime * 100) * 0.01f);

        if (DisplayTimes != null)
        {
            DisplayTimes.text += "\n" + Mathf.Round(CurrentTime * 100) * 0.01f;
        }
        if (File.Exists(SaveFile))
        {
            using (StreamWriter file = new StreamWriter(SaveFile, append: true))
            {
                file.WriteLine(Mathf.Round(CurrentTime * 100) * 0.01f);
            }
        }

        if (MaxQuestions > QuestionsAsked || MaxQuestions <= 0)
        {
            NewUser = false;
            if (RemainingQuestions.Count == 1)
            {
                RemainingQuestions = new List<GameObject>(AllQuestions);
            }

            if (AvoidRepeats)
            {
                if (RemainingQuestions.Contains(ActiveQuestion))
                {
                    RemainingQuestions.Remove(ActiveQuestion);
                }
            }
        }
        else
        {
            NewUser = true;
            RemainingQuestions = new List<GameObject>(AllQuestions);

            if (MaxQuestionsAsked != null)
            {
                MaxQuestionsAsked.SetActive(true);
            }

            if (DisplayTimes != null)
            {
                DisplayTimes.text += "\nNew User";
            }
            if (File.Exists(SaveFile))
            {
                var average = 0f;
                for(int i = 0; i < MaxQuestions; i++)
                {
                    average += Times[Times.Count - i -1];
                }
                average = Mathf.Round(average / MaxQuestions * 100) * 0.01f;

                using (StreamWriter file = new StreamWriter(SaveFile, append: true))
                {
                    file.WriteLine("Average:" + average);
                }
            }

            QuestionsAsked = 0;
        }

        ActiveQuestion = RemainingQuestions[Random.Range(0, RemainingQuestions.Count)];
        CurrentTime = 0;
    }
}
