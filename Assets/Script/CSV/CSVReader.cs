using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class candleData
{
    public string Date;
    public int Open;
    public int Close;
    public int Hightest;
    public int Lowest;

    public bool isGrowCandle
    {
        get
        {
            return (Close > Open) ? true : false;
        }
    }
}

[CreateAssetMenu(fileName = "new CSV", menuName = "CSV DATA")]
public class CSVReader : ScriptableObject
{
    public TextAsset CSV_DATA;
    public int startRow;
    public int endRow;

    [Header("Data")]
    public candleData[] DATA;

    public void OnValidate()
    {
        string[] datas = CSV_DATA.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int dataSize = datas.Length / 7 - 1;

        if (startRow < 1)
            startRow = 1;
        if(endRow > dataSize + 1)
        {
            endRow = dataSize + 1;
        }


        loadData();
    }

    private void loadData()
    {
        string[] datas = CSV_DATA.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        //Debug.Log(datas[8]);

        int dataSize = endRow - startRow;
        DATA = new candleData[dataSize];

        for (int i = 0; i < dataSize; i++)
        {
            DATA[i] = new candleData();
            int index = (7 * (i + 1)) + ((startRow - 1) * 7);
            DATA[i].Date = datas[index];
            DATA[i].Open = convertStringFloatToInt(datas[index + 1]);
            DATA[i].Hightest = convertStringFloatToInt(datas[index + 2]);
            DATA[i].Lowest = convertStringFloatToInt(datas[index + 3]);
            DATA[i].Close = convertStringFloatToInt(datas[index + 4]);
        }
    }

    int convertStringFloatToInt(string text)
    {
        return (int)float.Parse(text);
    }

    public int minValue()
    {
        int min;

        min = (DATA[0].isGrowCandle) ? DATA[0].Open : DATA[0].Close;

        for (int i = 1; i < DATA.Length; i++)
        {
            candleData candle = DATA[i];
            int temp = (candle.isGrowCandle) ? candle.Open : candle.Close;
            min = (temp < min) ? temp : min;
        }

        return min;
    }

    public int maxValue()
    {
        int max;

        max = (DATA[0].isGrowCandle) ? DATA[0].Close : DATA[0].Open;

        for (int i = 0; i < DATA.Length; i++)
        {
            candleData candle = DATA[i];
            int temp = (candle.isGrowCandle) ? candle.Close : candle.Open;
            max = (temp > max) ? temp : max;
        }

        return max;
    }
}
