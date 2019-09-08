﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;
public class CircleImage : Image
{
    public int segementCount;             //将圆绘制成的3角块数
    private int circleVertexCount;      //圆顶点的数量

    protected override void OnPopulateMesh(VertexHelper vertexHelper)
    {
        segementCount = 20;
        circleVertexCount = segementCount + 2;  //圆弧上顶点数比段区域数多1，再加上一个圆心顶点
        //重写后先将之前的vertexHelper存入的顶点信息和3角面信息清除
        vertexHelper.Clear();
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;
        Vector4 uv = (overrideSprite != null) ? DataUtility.GetOuterUV(overrideSprite) : Vector4.zero;


        //利用当前UV坐标计算出uvCenter的坐标(uvCenterX,uvCenterY)
        float uvCenterX = (uv.x + uv.z) * 0.5f;
        float uvCenterY = (uv.y + uv.w) * 0.5f;

        //利用当前Image的宽高和UV宽高计算出    X,Y轴上的UV转换系数(uvScaleX,uvScaleY)(每1单位的position所占的uv值)
        float uvScaleX = (uv.z - uv.x) / width;
        float uvScaleY = (uv.w - uv.y) / height;


        //求每一块段区域所占圆的弧度(radian)与绘制的圆的半径(radius)
        float radian =  2*Mathf.PI/ segementCount;
        float radius = rectTransform.pivot.x * width;

        //创建圆心顶点，并给出Position，color，uvPosition(通过[position的x,y坐标  *   X,Y轴上的UV转换系数]可求得)
        UIVertex centerVertex = new UIVertex();
        centerVertex.position = Vector2.zero;     //这个坐标主要用于作为其他顶点的坐标参照。
        centerVertex.color = color;
        centerVertex.uv0 = new Vector2(centerVertex.position.x * uvScaleX + uvCenterX, centerVertex.position.y * uvCenterY + uvCenterY);
        vertexHelper.AddVert(centerVertex);

        float curRadian = 0;   //记作当前弧度，并添加其余圆弧上的顶点
        for (int i = 0; i < circleVertexCount - 1; i++)
        {
            UIVertex vertex = new UIVertex();
            vertex.position = new Vector2(centerVertex.position.x + Mathf.Cos(curRadian) * radius, centerVertex.position.y + Mathf.Sin(curRadian) * radius);
            vertex.color = color;
            vertex.uv0 = new Vector2(vertex.position.x * uvScaleX + uvCenterX, vertex.position.y * uvScaleY + uvCenterY);
            vertexHelper.AddVert(vertex);
            curRadian += radian;
        }

        //vertexHelper添加3角面信息，在渲染流程中，以3个顶点顺时针添加的面渲染，3个顶点逆时针添加的面不渲染


        for (int i = 1; i < circleVertexCount-1; i++)
        {
            vertexHelper.AddTriangle(i, 0, i + 1);
        }
        vertexHelper.AddTriangle(circleVertexCount - 1, 0, 1);

    }


}