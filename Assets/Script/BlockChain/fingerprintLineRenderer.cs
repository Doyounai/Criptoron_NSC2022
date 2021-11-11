using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fingerprintLineRenderer : Graphic
{
    float currentYPosition = 0f;
    public float speed = 5f;

    public float size = 10f;
    public bool is_Active = false;
    float width;
    float height;

    public float getHeight { get { return height; } }
    public float YPosition
    {
        get
        {
            return currentYPosition;
        }
        set
        {
            currentYPosition = value;
            UpdateGeometry();
        }
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        width = rectTransform.rect.width;
        height = rectTransform.rect.height;

        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        vertex.position = new Vector3(0, currentYPosition);
        vh.AddVert(vertex);

        vertex.position = new Vector3(0, currentYPosition + size);
        vh.AddVert(vertex);

        vertex.position = new Vector3(width, currentYPosition + size);
        vh.AddVert(vertex);

        vertex.position = new Vector3(width, currentYPosition);
        vh.AddVert(vertex);

        vh.AddTriangle(0, 1, 2);
        vh.AddTriangle(0, 3, 2);
    }
}
