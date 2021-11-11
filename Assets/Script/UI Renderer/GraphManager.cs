using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
public class GraphManager : MonoBehaviour
{
    public CSVReader Data;
    public GraphRenderer graph;

    [Header("Customize")]
    public int upperMarginPercent;
    public int dowerMarginPercent;
    public int rightMarginPercent;

    static float XW_max;
    static float YW_max;
    static float XW_min;
    static float YW_min;

    static float XV_max;
    static float YV_max;
    static float XV_min;
    static float YV_min;

    static float SX;
    static float SY;

    static float candleWidth;
    public static float getCandleWidth { get { return candleWidth; } }

    int min;
    int max;

    //change on inspector
    private void OnValidate()
    {
        updateGeometry();
    }

    //change on CSV
    public void updateGeometry()
    {
        updateConfing();
        graph.UpdateGraph();
    }

    void updateConfing()
    {
        RectTransform rectTransform = this.GetComponent<RectTransform>();

        //XW
        XV_max = rectTransform.rect.width;
        YV_max = rectTransform.rect.height;
        XV_min = 0;
        YV_min = 0;

        candleWidth = XV_max / (Data.DATA.Length + findPercentage(rightMarginPercent, Data.DATA.Length));
        min = Data.minValue();
        max = Data.maxValue();
        min -= (int)findPercentage(dowerMarginPercent, max);
        max += (int)findPercentage(upperMarginPercent, max);

        //XV
        XW_max = candleWidth * (Data.DATA.Length + 1);
        YW_max = max;
        XW_min = 0;
        YW_min = min;

        //SX SY
        SX = (XV_max - XV_min) / (XW_max - XW_min);
        SY = (YV_max - YV_min) / (YW_max - YW_min);
    }

    public static float findPercentage(float percent, float max)
    {
        return (percent * max) / 100;
    }

    public static int convertCoordinateYToViewportY(int posY)
    {
        return Mathf.RoundToInt(YV_min + (posY - YW_min) * SY);
    }

    public static int convertCoordinateXToViewportX(int posX)
    {
        return Mathf.RoundToInt(XV_min + (posX - XW_min) * SX);
    }
}
