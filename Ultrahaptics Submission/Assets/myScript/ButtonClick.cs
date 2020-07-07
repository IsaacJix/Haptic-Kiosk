using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClick : MonoBehaviour
{
    public void Click()
    {
        GetComponent<Button>().onClick.Invoke();
    }
}
