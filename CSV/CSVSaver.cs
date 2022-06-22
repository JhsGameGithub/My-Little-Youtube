using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;


public class CSVSaver 
{
    public void AddRowCSV(string[] rows, List<string[] > rowData)
    {
        string[] rowDataTemp = new string[rows.Length];

        for(int i =0; i< rows.Length; i++)
        {
            rowDataTemp[i] = rows[i];
        }

        rowData.Add(rowDataTemp);
    }

    public static void WriteCSV(List<string[]> rowData, string filePath)
    {
        string[][] output = new string[rowData.Count][];

        for(int i=0; i<output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);

        string delimiter = ",";

        StringBuilder stringBuilder = new StringBuilder();

        for(int index =0; index < length; index++)
        {
            stringBuilder.AppendLine(string.Join(delimiter, output[index]));
        }


        Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);

        StreamWriter outStream = new StreamWriter(fileStream, Encoding.UTF8);

        outStream.WriteLine(stringBuilder);
        outStream.Close();


       // m_IsWritting = false;
    }




}
