using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SheetProcessor : MonoBehaviour
{
    
    // a1,b1,c1,d1
    // a2,b2,c2,d2
    // a3,b3,c3,d3
    
    private const int _name = 0;
    

    private const char _cellSeporator = ',';
    private const char _inCellSeporator = ';';

    private Dictionary<string, Color> _colors = new Dictionary<string, Color>()
    {
        {"white", Color.white},
        {"black", Color.black},
        {"yellow", Color.yellow},
        {"red", Color.red},
        {"green", Color.green},
        {"blue", Color.blue},
    };
    
    public NameBank ProcessData(string cvsRawData)
    {
        char lineEnding = GetPlatformSpecificLineEnd();
        string[] rows = cvsRawData.Split(lineEnding);
        int dataStartRawIndex = 1;
        NameBank data = new NameBank();
        
        for (int i = dataStartRawIndex; i < rows.Length; i++)
        {
            string[] cells = rows[i].Split(_cellSeporator);

            string nameInCell = cells[_name];
            
            data.Names.Add(nameInCell);
        }
        Debug.Log(data.Names.ToString());
        return data;

    }

    private Color ParseColor(string color)
    {
        color = color.Trim();
        Color result = default;
        if (_colors.ContainsKey(color))
        {
            result = _colors[color];
        }
        
        return result;
    }

    private Vector3 ParseVector3(string s)
    {
        string[] vectorComponents = s.Split(_inCellSeporator);
        if (vectorComponents.Length < 3)
        {
            Debug.Log("Can't parse Vector3. Wrong text format");
            return default;
        }

        float x = ParseFloat(vectorComponents[0]);
        float y = ParseFloat(vectorComponents[1]);
        float z = ParseFloat(vectorComponents[2]);
        return new Vector3(x, y, z);
    }
    
    private int ParseInt(string s)
    {
        int result = -1;
        if (!int.TryParse(s, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out result))
        {
            Debug.Log("Can't parse int, wrong text");
        }

        return result;
    }
    
    private float ParseFloat(string s)
    {
        float result = -1;
        if (!float.TryParse(s, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out result))
        {
            Debug.Log("Can't pars float,wrong text ");
        }

        return result;
    }
    
    private char GetPlatformSpecificLineEnd()
    {
        char lineEnding = '\n';
#if UNITY_IOS
        lineEnding = '\r';
#endif
        return lineEnding;
    }
    
    
}
