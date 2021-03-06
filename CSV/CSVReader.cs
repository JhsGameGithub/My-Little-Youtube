using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text.RegularExpressions;
using System.IO;

public class CSVReader
{
    public static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";

    public static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";

    public static char[] TRIM_CHARS = { '\"', ' ', '%' };



    public static List<Dictionary<string, object>> Read(string file, bool _isRe = false)
    {

        var list = new List<Dictionary<string, object>>();

      

        string data = "";

        if (!_isRe)
        {
            FileInfo fileInfo = new FileInfo(file);

            if (fileInfo.Exists)
            {
                data = File.ReadAllText(file);
            }
            else
            {
                return null;
            }
        }
        else
        {
            TextAsset fileData = Resources.Load(file) as TextAsset;

            if (fileData == null)
            {
                return null;
            }

            data = fileData.text;
        }

      

        //줄단위로 받음
        var lines = Regex.Split(data, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        //ㄹㅇ 첫줄
        var header = Regex.Split(lines[0], SPLIT_RE);

        for (var i = 1; i < lines.Length; i++)

        {
            var values = Regex.Split(lines[i], SPLIT_RE);

            if (values.Length == 0 || values[0] == "") continue;



            var entry = new Dictionary<string, object>();

            for (var j = 0; j < header.Length && j < values.Length; j++)

            {

                string value = values[j];

                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");

                object finalvalue = value;

                int n;

                float f;

                if (int.TryParse(value, out n))

                {

                    finalvalue = n;

                }

                else if (float.TryParse(value, out f))

                {

                    finalvalue = f;

                }

                entry[header[j]] = finalvalue;

            }

            list.Add(entry);

        }

        return list;

    }
}
