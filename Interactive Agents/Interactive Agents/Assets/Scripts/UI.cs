using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text selected, nametext, dex, str, intel, context1Label, context2Label, state, context1, context2;
    

    public void UpdateUI(string selectedS, string nametextS, string dexS, string strS, 
                         string intelS, string context1LabelS, string context2LabelS, 
                         string stateS, string context1S, string context2S)
    {
        selected.text = selectedS;
        nametext.text = nametextS;
        dex.text = dexS;
        str.text = strS;
        intel.text = intelS;
        context1Label.text = context1LabelS;
        context2Label.text = context2LabelS;
        state.text = stateS;
        context1.text = context1S;
        context2.text = context2S;
    }
}
