using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MTest : MonoBehaviour
{
    //WMG_Series mSeries1;
    //WMG_Series mSeries2;

    //void UpdateSer()
    //{
    //    mSeries1.UpdateSprites();
    //    mSeries2.UpdateSprites();
    //}

    void Update()
    {
        //饼状图
        if (Input.GetKeyDown(KeyCode.A))
        {
            WMG_Pie_Graph wmg = GetComponent<WMG_Pie_Graph>();
            Pie(wmg);
        }
        //柱状图、折线图
        else if (Input.GetKeyDown(KeyCode.B))
        {
            WMG_Axis_Graph wmg = GetComponent<WMG_Axis_Graph>();
            BarAndLine(wmg);

            //Invoke("UpdateSer",0.2f);
            //wmg.graphC.Changed();
            //wmg.seriesCountC.Changed();
            //wmg.legend.legendC.Changed();
            //wmg.seriesNoCountC.Changed();
            //wmg.Refresh();
        }
        //环形指针图
        else if (Input.GetKeyDown(KeyCode.C))
        {
            WMG_Ring_Graph ring = GetComponent<WMG_Ring_Graph>();
            Ring(ring);
        }
        //雷达图
        else if (Input.GetKeyDown(KeyCode.D))
        {
            WMG_Radar_Graph radar = GetComponent<WMG_Radar_Graph>();
            StartCoroutine("Radar",radar);
        }
        //散点图
        else if (Input.GetKeyDown(KeyCode.E))
        {
            WMG_Axis_Graph wmg = GetComponent<WMG_Axis_Graph>();
            ScatterPlot(wmg);
        }

        else if (Input.GetKeyDown(KeyCode.U))
        {
            //mSeries1.UpdateSprites();
            //mSeries2.UpdateSprites();
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            GameObject circle = new GameObject("circle");
            circle.AddComponent<MeshFilter>();
            circle.GetComponent<MeshFilter>().mesh = CreateMesh();
            MeshRenderer meshRenderer = circle.AddComponent<MeshRenderer>();
            meshRenderer.material.shader = Shader.Find("UI/Default");
            meshRenderer.material.color = Color.white;
        }
    }

    /// <summary>
    /// 饼状图
    /// </summary>
    /// <param name="wmg"></param>
    void Pie(WMG_Pie_Graph wmg)
    {
        //Core
        string[] sliceLabels = new string[] { "苹果", "小米", "三星", "华为", "一加" };
        float[] sliceValues = new float[] { 4, 3, 3, 3, 2 };//设置所占比例(相加是100%)
        Color[] sliceColors = new Color[] { Color.yellow, Color.blue, Color.gray, Color.green, Color.cyan };
        wmg.sliceLabels.SetList(sliceLabels);
        wmg.sliceValues.SetList(sliceValues);
        wmg.sliceColors.SetList(sliceColors);

        wmg.sortBy = WMG_Pie_Graph.sortMethod.Largest_First;//排序方式
        wmg.swapColorsDuringSort = true;//图例也跟随排序
        wmg.sliceLabelType = WMG_Enums.labelTypes.Labels_Percents;//部分块文本显示类型
        wmg.explodeLength = 0;//部分块分离距离
        wmg.explodeSymmetrical = true;//部分块匀称填充
        wmg.useDoughnut = true;//中心圆填充（用于制作环形图）
        wmg.doughnutPercentage = 0.05f;//填充大小

        //OtherSlice
        wmg.limitNumberSlices = true;//是否使用以下OtherSilice功能
        wmg.includeOthers = true;//使用“其他部分”
        wmg.maxNumberSlices = 4;//最多显示的部分项，多出的项数会用“其他部分”表示
        wmg.includeOthersLabel = "Other";//设置“其他部分”名称
        wmg.includeOthersColor = Color.white;//设置“其他部分”颜色

        //Anim
        wmg.animationDuration = 1;//调整图像大小或数据 设置延迟动画
        wmg.sortAnimationDuration = 0;

        //Labels
        wmg.sliceLabelExplodeLength = -60;//部分块（slice）文本距圆边缘距离
        wmg.sliceLabelFontSize = 15;//文本字体
        wmg.numberDecimalsInPercents = 1;//保留小数位数

        //Misc
        wmg.leftRightPadding = new Vector2(50, 220);//调整背景填充
        wmg.topBotPadding = new Vector2(20, 20);
        wmg.bgCircleOffset = 30;//调节饼图底图圆大小

        //Legend
        wmg.legend.hideLegend = false;//是否隐藏图例
        wmg.legend.legendType = WMG_Legend.legendTypes.Right;//图例显示位置方式
        wmg.legend.labelType = WMG_Enums.labelTypes.Labels_Values;//图例文本说明显示类型
        wmg.legend.showBackground = true;//显示图例背景
        wmg.legend.oppositeSideLegend = false;//反转位置
        wmg.legend.offset = 30;//图例与饼的间距
        wmg.legend.legendEntryWidth = 160;//调整图例大小
        wmg.legend.legendEntryHeight = 50;
        wmg.legend.numRowsOrColumns = 1;//一行或列显示个数
        wmg.legend.numDecimals = 0;//保留小数位数
        wmg.legend.legendEntryLinkSpacing = 0;//图例色块与图例框的开始间距
        wmg.legend.legendEntryFontSize = 15;//图例说明文本字体大小
        wmg.legend.legendEntrySpacing = 40;//图例与说明文本间隔
        wmg.legend.pieSwatchSize = 30;//图例色块大小
        wmg.legend.backgroundPadding = 10;//图例背景填充大小
        wmg.legend.autofitEnabled = true;//图例与饼自动适应大小
    }

    /// <summary>
    /// 柱状图、折线图
    /// </summary>
    void BarAndLine(WMG_Axis_Graph wmg)
    {
        //Core
        wmg.deldteAllSeries();//删除所有系列(实例化出来的预制可能会自带2条数据系列(series),要删除掉)
        wmg.graphType = WMG_Axis_Graph.graphTypes.line;//图形类型(折线line/柱状图bar_side)
        wmg.orientationType = WMG_Axis_Graph.orientationTypes.vertical;//可设置XY轴对换
        wmg.groups.SetList(new string[] { "春", "夏", "秋", "冬" });
        wmg.resizeEnabled = true;
        wmg.paddingLeftRight = new Vector2(65, 40);
        wmg.paddingTopBottom = new Vector2(40, 60);
        wmg.barWidth = 10;//柱状图柱宽度
        wmg.barAxisValue = 0;

        wmg.autoUpdateOrigin = true;//自适应原点
        wmg.autoUpdateBarWidth = false;//自适应柱宽
        wmg.autoUpdateBarWidthSpacing = 0.3f;//柱间隔
        wmg.autoUpdateSeriesAxisSpacing = true;
        wmg.autoUpdateBarAxisValue = true;

        wmg.autoFitLabels = false;//是否开启图像自适应背景
        wmg.autoFitPadding = 10;//图像与背景左下角的间距

        //YAxis
        wmg.yAxis.AxisMaxValue = 40;
        wmg.yAxis.AxisMinValue = -10;
        wmg.yAxis.AxisNumTicks = 11;
        wmg.yAxis.AxisMaxValue = 40;//对应轴上节点的最大最小值
        wmg.yAxis.AxisMinValue = -10;
        wmg.yAxis.AxisNumTicks = 11;//包括原点的轴上节点数
        wmg.yAxis.hideGrid = true;//隐藏网格
        wmg.yAxis.hideTicks = false;//隐藏轴上节点
        wmg.yAxis.AxisTitleString = "温度";
        wmg.yAxis.AxisTitleOffset = new Vector2(30, 0);

        //XAxis
        wmg.xAxis.AxisNumTicks = 4;
        wmg.xAxis.AxisTitleString = "季节";
        wmg.xAxis.AxisTitleOffset = new Vector2(0, 30);
        wmg.xAxis.LabelType = WMG_Axis.labelTypes.groups;//轴上节点文本显示类型

        //Tooltip
        wmg.tooltipEnabled = true;//激活鼠标指向节点显示节点信息的提示功能
        wmg.tooltipOffset = new Vector2(10, 10);//距鼠标的偏移量
        wmg.tooltipNumberDecimals = 2;//保留小数位数
        wmg.tooltipDisplaySeriesName = true;//是否显示该节点所属系列(series)的系列名

        //Anim
        wmg.tooltipAnimationsEnabled = true; //激活鼠标指向节点显示节点信息的提示动画功能
        wmg.tooltipAnimationsEasetype = DG.Tweening.Ease.OutElastic;//动画类型
        wmg.tooltipAnimationsDuration = 0.5f;//动画时长

        //Misc
        wmg.tickSize = new Vector2(2, 5);//X、Y轴上节点的大小
        wmg.graphTitleString = "A、B两地四季平均气温";

        //Legend图例(该图形对象子物体)
        wmg.legend.hideLegend = false;//显示图例
        wmg.legend.legendType = WMG_Legend.legendTypes.Bottom;//显示位置
        wmg.legend.labelType = WMG_Enums.labelTypes.Labels_Only;//说明文本类型
        wmg.legend.showBackground = true;//显示背景图
        wmg.legend.oppositeSideLegend = true;//显示位置反向操作
        wmg.legend.offset = 35;
        wmg.legend.legendEntryWidth = 100;//宽
        wmg.legend.legendEntryHeight = 30;//高
        wmg.legend.numRowsOrColumns = 1;//一行/列显示的个数
        wmg.legend.numDecimals = 0;//保留小数
        wmg.legend.legendEntryLinkSpacing = 15;//调节折线图线段长度
        wmg.legend.legendEntryFontSize = 15;//文本字体
        wmg.legend.legendEntrySpacing = 30;//调节文本位置
        wmg.legend.backgroundPadding = 0;//背景填充
        wmg.legend.autofitEnabled = true;//自适应调节

        //添加两个城市A、B四季平均温度
        WMG_Series series1 = wmg.addSeries();
        series1.seriesName = "A";//这一系列数据对应的对象名称(这表示A地的四季温度)
        series1.pointValues.SetList(new Vector2[] {
            new Vector2(0, 10),
            new Vector2(0, 26),
            new Vector2(0, 12),
            new Vector2(0, 10)});

        series1.UseXDistBetweenToSpace = true;//系列节点之间使用间隔(一般都设为true)
        series1.extraXSpace = 0;//图形开始距离原点距离
        series1.hidePoints = false;//是否隐藏节点
        series1.hideLines = false;//是否隐藏节点之间的线段
        series1.connectFirstToLast = false;//连接初始节点和末节点的线段(仅对折线图使用)
        series1.lineScale = 0.3f;//线段大小
        series1.linePadding = 0;//线段填充
        series1.pointWidthHeight = 10;//节点大小
        series1.lineColor = Color.white;//线段颜色
        series1.pointColor = Color.yellow;//节点颜色

        series1.dataLabelsEnabled = true;//节点附近显示对应的数据
        series1.dataLabelsNumDecimals = 0;//保留小数位数
        series1.dataLabelsFontSize = 1;//显示的Label字体大小
        series1.dataLabelsOffset = Vector2.zero;//调整Label位置(相对节点的偏移量)

        series1.areaShadingType = WMG_Series.areaShadingTypes.Gradient;//显示阴影方式(柱状图无此部分,要设置成None)
        series1.areaShadingColor = Color.red;//折线下的阴影颜色
        //series1.areaShadingColor.a = 0.5f;//调透明度(0 - 1)
        series1.areaShadingAxisValue = 0.01f;//从(y=0.01)的位置开始显示阴影

        WMG_Series series2 = wmg.addSeries();
        series2.seriesName = "B";
        series2.pointColor = Color.blue;
        series2.pointValues.SetList(new Vector2[] {
            new Vector2(0, 10),
            new Vector2(0, 23),
            new Vector2(0, 24),
            new Vector2(0, 5),
        });
        series2.UseXDistBetweenToSpace = true;
        series2.pointColor = Color.green;
        series2.areaShadingType = WMG_Series.areaShadingTypes.Gradient;
        series2.areaShadingColor = Color.blue;
        float a = series2.areaShadingColor.a;
        series2.areaShadingAxisValue = 0.01f;
        series2.lineScale = 0.3f;
        series2.pointWidthHeight = 10;

        //series1.UpdateSprites();
        //series2.UpdateSprites();
        //mSeries1 = series1;
        //mSeries2 = series2;

        wmg.WMG_Click += ClickEvent;
        wmg.WMG_Click_Leg += ClickEvent;
        wmg.WMG_Link_Click += ClickLinkEvent;
        wmg.WMG_Link_Click_Leg += ClickLinkEvent;

        wmg.WMG_MouseEnter += MouseEnterNodeEvent;
        wmg.WMG_MouseEnter_Leg += MouseEnterNodeEvent;
        wmg.WMG_Link_MouseEnter += MouseEnterLinkEvent;
        wmg.WMG_Link_MouseEnter_Leg += MouseEnterLinkEvent;
    }

    /// <summary>
    /// 指针环形图
    /// </summary>
    void Ring(WMG_Ring_Graph ring)
    {
        //Core
        ring.values.SetList(new float[] { 30, 90, 270, 360 });//所占比值(100%为maxValue - minValue)
        ring.labels.SetList(new string[] { "Ring1", "Ring2", "Ring3", "Ring4" });
        ring.bandColors.SetList(new Color[] { Color.clear, Color.blue, Color.cyan, Color.yellow });
        ring.bandMode = true;//开启带圆环的模式
        ring.innerRadiusPercentage = 0;//环内部半径
        ring.degrees = 90;//开始偏移角度（可设置圆环是缺的还是整圆,一般为0或90）
        ring.minValue = 0;// maxValue - minValue 为整圆的分割份数(一般为0-360，-90-270效果相同)
        ring.maxValue = 360;
        ring.ringColor = Color.white;//环线颜色
        ring.ringWidth = 4;//环线粗细
        ring.bandPadding = -2;//环填充
        ring.labelLinePadding = 20;//环指针长度
        ring.leftRightPadding = new Vector2(50, 50);//环形图背景填充
        ring.leftRightPadding = new Vector2(50, 50);
        ring.antiAliasing = true;//环填充边缘渐变
        ring.antiAliasingStrength = 0;//渐变程度

        //ring.ringLabelsParent//获取到ringLabels，可手动激活对应块的Label
        //for (int i = 0; i < ring.ringLabelsParent.transform.childCount; i++)
        //{
        //    Transform line = ring.ringLabelsParent.transform.GetChild(i);
        //    line.GetChild(1).gameObject.SetActive(true);
        //}

        //Misc
        ring.animateData = true;//开启动画
        ring.animDuration = 2;//过渡动画时长
        ring.animEaseType = DG.Tweening.Ease.OutQuad;//动画类型
    }

    /// <summary>
    /// 雷达图
    /// </summary>
    /// <param name="radar"></param>
    /// <returns></returns>
    IEnumerator Radar(WMG_Radar_Graph radar)
    {
        radar.numPoints = 5;//项数
        radar.offset = new Vector2(0, -20);//调整雷达图位置
        radar.degreeOffset = 90;//图旋转角度（默认90）
        radar.radarMinVal = 0;
        radar.radarMaxVal = 100;//满值
        radar.numGrids = 3;//雷达图网层数
        radar.gridLineWidth = 0.3f;//雷达图网格线粗细
        radar.labelsOffset = 20;//Label与雷达图间距
        radar.fontSize = 20;//Label字体大小
        radar.numDataSeries = 1;//数据系列条数
        radar.labelStrings.SetList(new string[] { "ATK", "DEF", "SPD", "HIT", "MAG" });//设置5项的Label
        radar.hideLabels = false;//隐藏Labels
        radar.dataSeriesColors.SetList(new Color[] { Color.yellow });//设置数据系列(Series)线的颜色
        radar.dataSeriesLineWidth = 0.3f;//线粗细

        yield return new WaitForSeconds(0.05f);

        List<float> tmpList = new List<float>(new float[] { 100, 60, 70, 80, 90 });//设置各属性值
        radar.dataSeries[0].pointValues.SetList(radar.GenRadar(tmpList, radar.offset.x, radar.offset.y, radar.degreeOffset));
    }

    /// <summary>
    /// 散点图
    /// </summary>
    void ScatterPlot(WMG_Axis_Graph wmg)
    {
        //与其他轴图像类似（用的都是WMG_Axis_Graph脚本）
        wmg.deldteAllSeries();
        wmg.legend.hideLegend = false;//是否隐藏图例

        wmg.graphTitleString = "A、B两同学2018学期数学考试分数";
        wmg.yAxis.AxisTitleString = "分数";
        wmg.xAxis.AxisTitleString = "2018学期";
        wmg.yAxis.AxisTitleFontSize = 10;
        wmg.xAxis.AxisTitleFontSize = 10;

        wmg.yAxis.AxisMaxValue = 100;//最高分
        wmg.yAxis.AxisMinValue = 0;//最低分
        wmg.yAxis.AxisNumTicks = 11;
        wmg.xAxis.hideTicks = true;//隐藏x轴上节点
        wmg.xAxis.hideLabels = true;

        //Legend
        wmg.legend.legendType = WMG_Legend.legendTypes.Right;
        wmg.legend.offset = 10;
        wmg.legend.legendEntryLinkSpacing = 10;
        wmg.legend.legendEntrySpacing = 10;

        WMG_Series series1 = wmg.addSeries();
        series1.seriesName = "A成绩散点";
        for (int i = 0; i < 30; i++)
        {
            series1.pointValues.Add(new Vector2(i * 3, Change(80)));
        }
        series1.pointWidthHeight = 3;
        series1.pointColor = Color.cyan;//数据节点颜色
        series1.hideLines = true;//隐藏数据节点之间的线

        WMG_Series series2 = wmg.addSeries();
        series2.seriesName = "B成绩散点";
        for (int i = 0; i < 30; i++)
        {
            series2.pointValues.Add(new Vector2(i * 3, Change(80)));
        }
        series2.pointWidthHeight = 3;
        series2.pointColor = Color.red;
        series2.hideLines = true;
    }

    float Change(float n)
    {
        return n + Random.Range(-10f, 10f);
    }

    Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.name = "circle";
        float circle_radius = 5;//圆的半径  
        int segment = 360;//分段数  
        float circle_Z = 0;//圆所处的Z平面  
        //圆心(0,0)  
        float circle_center_point_x = 0;
        float circle_center_point_y = 0;
        List<Vector3> circle_mesh_vertices = new List<Vector3>();
        List<Vector2> circle_mesh_uv = new List<Vector2>();
        List<int> circle_triangles = new List<int>();
        circle_mesh_vertices.Add(new Vector3(circle_center_point_x, circle_center_point_y, circle_Z));
        circle_mesh_uv.Add(new Vector2(0, 0));
        for (int i = 1; i < segment + 1; i++)
        {
            float angle_in_circular_segment = (i - 1) * (360 / segment);//圆周角  
            circle_mesh_vertices.Add(new Vector3(
                circle_radius * Mathf.Cos(angle_in_circular_segment) * Mathf.Sin(angle_in_circular_segment),
                circle_radius * Mathf.Pow(Mathf.Cos(angle_in_circular_segment), 2),
                circle_Z
            ));
            circle_mesh_uv.Add(new Vector2(0, 0));
            circle_triangles.Add(0);
            circle_triangles.Add(i);
            //平时都是0,i,i+1的，但最后一个0,360,1需要特殊处理  
            if (i + 1 < segment + 1)
            {
                circle_triangles.Add(i + 1);
            }
            else
            {
                circle_triangles.Add(1);
            }
        }
        mesh.vertices = circle_mesh_vertices.ToArray();
        mesh.uv = circle_mesh_uv.ToArray();
        mesh.triangles = circle_triangles.ToArray();
        return mesh;
    }

    //鼠标点击和指向事件
    void ClickEvent(WMG_Series aSeries, WMG_Node aNode)
    {
        print("点击");
    }
    void ClickLinkEvent(WMG_Series aSeries, WMG_Link aLink)
    {
        print("点击");
    }
    void MouseEnterNodeEvent(WMG_Series aSeries, WMG_Node aNode, bool state)
    {
        print("Enter");
    }
    void MouseEnterLinkEvent(WMG_Series aSeries, WMG_Link aLink, bool state)
    {
        print("Enter");
    }
}
