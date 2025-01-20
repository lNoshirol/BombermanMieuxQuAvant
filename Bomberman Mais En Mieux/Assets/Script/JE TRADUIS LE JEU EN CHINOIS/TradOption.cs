using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TradOption : MonoBehaviour
{
    public string textFr;
    public string textCn;

    private TextMeshProUGUI m_TextMeshProUGUI;

    public TMP_FontAsset FontFr;
    public TMP_FontAsset FontCn;

    private void Awake()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    public void Change(string langId)
    {
        switch (langId)
        {
            case "Fr":
                m_TextMeshProUGUI.text = textFr;
                m_TextMeshProUGUI.font = FontFr;
                break;
            case "Cn":
                m_TextMeshProUGUI.text = textCn;
                m_TextMeshProUGUI.font = FontCn;
                break;
        }
    }
}
