using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceCapitalize : MonoBehaviour
{
    public InputField your_input;

    void Start()
    {
        your_input.onValidateInput +=
        delegate (string s, int i, char c) { return char.ToUpper(c); };
    }
}
