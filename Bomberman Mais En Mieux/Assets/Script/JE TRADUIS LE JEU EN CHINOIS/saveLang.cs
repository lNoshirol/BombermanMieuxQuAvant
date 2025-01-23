using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using UnityEngine;

public class saveLang : MonoBehaviour
{
    public string langId;

    private ChangeLang changeLang;

    private void Start()
    {
        changeLang = GetComponent<ChangeLang>();

        if (System.IO.File.Exists("Assets" + "/save.xml"))
        {
            Test();
        }
    }

    public void SaveLangId()
    {
        XmlWriterSettings settings = new XmlWriterSettings
        {
            NewLineOnAttributes = true,
            Indent = true,
        };

        XmlWriter writer = XmlWriter.Create("Assets" + "/save.xml", settings);
        writer.WriteStartDocument();

        writer.WriteStartElement("global");

        WriteXML(writer, "langId", langId);

        writer.WriteEndElement();

        writer.WriteEndDocument();
        writer.Close();
    }

    public void WriteXML(XmlWriter _writer, string key, string value)
    {
        _writer.WriteStartElement(key);
        _writer.WriteString(value);
        _writer.WriteEndElement();
    }

    public void EditLangId(string newId)
    {
        langId = newId;
        SaveLangId();
    }

    public void Test()
    {
        XmlDocument saveFile = new XmlDocument();

        string theLangId = "";

        if (!System.IO.File.Exists("Assets" + "/save.xml"))
        {
            Debug.LogError("Tu te fou de ma gueule fdp");
            return;
        }

        saveFile.Load("Assets" + "/save.xml");

        string key;
        string value;

        foreach (XmlNode node in saveFile.ChildNodes[1])
        {
            key = node.Name;
            value = node.InnerText;

            switch (key)
            {
                case "langId":
                    theLangId = value; 
                    break;
            }
        }

        changeLang.ChangeAllText(theLangId);
    }
}
