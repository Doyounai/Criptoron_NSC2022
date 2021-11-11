using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*  =====================================================
 *
 *                       _oo0oo_
 *                      o8888888o
 *                      88" . "88
 *                      (| -_- |)
 *                      0\  =  /0
 *                    ___/`---'\___
 *                  .' \|     |// '.
 *                 / \|||  :  |||// \
 *                / _||||| -:- |||||- \
 *               |   | \\  -  /// |   |
 *               | \_|  ''\---/''  |_/ |
 *               \  .-\__  '-'  ___/-. /
 *             ___'. .'  /--.--\  `. .'___
 *          ."" '<  `.___\_<|>_/___.' >' "".
 *         | | :  `- \`.;`\ _ /`;.`/ - ` : | |
 *         \  \ `_.   \_ __\ /__ _/   .-` /  /
 *     =====`-.____`.___ \_____/___.-`___.-'=====
 *                       `=---='
 *
 *     ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 *
 *               Buddha Bless:  "No Bugs"
 *
 */

public class GraphRenderer : Graphic
{
    public Vector2Int gridSize;
    public CSVReader Data;

    [Header("Customize")]
    public int upper_margin;
    public int dower_margin;
    public Color growColor;
    public Color downColor;

    float XW_max;
    float YW_max;
    float XW_min;
    float YW_min;

    float XV_max;
    float YV_max;
    float XV_min;
    float YV_min;

    float SX;
    float SY;

    float candleWidth;
    int min;
    int max;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        //XW
        XV_max = rectTransform.rect.width;
        YV_max = rectTransform.rect.height;
        XV_min = 0;
        YV_min = 0;

        candleWidth = XV_max / Data.DATA.Length;
        min = Data.minValue() - dower_margin;
        max = Data.maxValue() + upper_margin;

        //XV
        XW_max = candleWidth * (Data.DATA.Length + 1);
        YW_max = max;
        XW_min = 0;
        YW_min = min;

        //SX SY
        SX = (XV_max - XV_min) / (XW_max - XW_min);
        SY = (YV_max - YV_min) / (YW_max - YW_min);

       //Debug.Log("SY" + SY.ToString());
       //Debug.Log("Max " + max.ToString() + " min " + min.ToString());
       //Debug.Log("posY" + convertCoordinateYToViewportY(Data.DATA[0].Open));

        //add vertex
        for (int i = 0; i < Data.DATA.Length; i++)
        {
            candleData candle = Data.DATA[i];
            AddVertex(candle, i, vh);
        }
    }

    private void AddVertex(candleData candle, int index, VertexHelper vh)
    {
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = (candle.isGrowCandle)? growColor : downColor;
        //vertex.color = color;

        //0
        vertex.position = new Vector3(candleWidth * index, convertCoordinateYToViewportY(candle.Open));
        vh.AddVert(vertex);
        //1
        vertex.position = new Vector3(candleWidth * index, convertCoordinateYToViewportY(candle.Close));
        vh.AddVert(vertex);
        //2
        vertex.position = new Vector3(candleWidth * (index + 1), convertCoordinateYToViewportY(candle.Close));
        vh.AddVert(vertex);
        //3
        vertex.position = new Vector3(candleWidth * (index + 1), convertCoordinateYToViewportY(candle.Open));
        vh.AddVert(vertex);

        int i = index * 4;
        vh.AddTriangle(i + 0, i + 1, i + 2);
        vh.AddTriangle(i + 2, i + 3, i + 0);
    }

    private int convertCoordinateYToViewportY(int posY)
    {
        return Mathf.RoundToInt(YV_min + (posY - YW_min) * SY);
    }

    private int convertCoordinateXToViewportX(int posX)
    {
        return Mathf.RoundToInt(XV_min + (posX - XW_min) * SX);
    }
}
