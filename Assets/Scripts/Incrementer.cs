using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Incrementer : MonoBehaviour
{
    public TextMeshProUGUI value;

    public void Increment()
    {
        int currentValue = int.Parse(value.text);
        if (currentValue < 5)
        {
            currentValue++;
            value.text = currentValue.ToString();
        }
    }

    public void Decrement()
    {
        int currentValue = int.Parse(value.text);
        if (currentValue > 0)
        {
            currentValue--;
            value.text = currentValue.ToString();
        }
    }
}
