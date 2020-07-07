using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindObjects : MonoBehaviour
{
    public List<string> FindObjectNames;
    public List<GameObject> FoundObjects;

    public bool ActivateFoundObjects = false;
    public bool DeactivateFoundObjects = false;

    private void OnEnable()
    {
        Find();
    }

    public void Find()
    {
        //Invoke("Find", 1);
        foreach(string search in FindObjectNames)
        {
            if (search.Contains("[") && search.Contains("]"))
            {
                string shortSearch = search.Substring(0, search.IndexOf("["));
                string childIndex = search.Substring(search.IndexOf("[") + 1, search.IndexOf("]") - search.IndexOf("[") - 1);

                bool success = int.TryParse(childIndex, out int i);

                if (success)
                {
                    if (GameObject.Find(shortSearch) != null)
                    {
                        var found = GameObject.Find(shortSearch);

                        if (found.transform.childCount >= i)
                        {
                            var foundChild = found.transform.GetChild(i).gameObject;

                            if (!FoundObjects.Contains(foundChild))
                            {
                                FoundObjects.Add(foundChild);
                            }
                        }
                    }
                    else
                    {
                        print("No object with name \"" + shortSearch + "\" was found");
                    }
                }
                else
                {
                    print(childIndex + " is not an integer");
                }
            }
            else
            {
                if (GameObject.Find(search) != null)
                {
                    var found = GameObject.Find(search);

                    if (!FoundObjects.Contains(found))
                    {
                        FoundObjects.Add(found);
                    }
                }
                else
                {
                    print("No object with name \"" + search + "\" was found");
                }
            }
        }

        if (ActivateFoundObjects)
        {
            foreach (GameObject obj in FoundObjects)
            {
                obj.SetActive(true);
            }
        }
        if (DeactivateFoundObjects)
        {
            foreach(GameObject obj in FoundObjects)
            {
                obj.SetActive(false);
            }
        }
    }
}
