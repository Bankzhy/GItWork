using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Test : MonoBehaviour {
    public void ChangeColor()
    {
        GetComponent<Image>().color = Color.black;
    }

    public void Change()
    {
        Color color = GetComponent<Image>().color;
        if (color == Color.black)
        {
            GetComponent<Image>().color = Color.white;

        }
        else
        {
            GetComponent<Image>().color = Color.black;
        }
    }
}
