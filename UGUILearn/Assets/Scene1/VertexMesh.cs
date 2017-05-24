using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent (typeof(Graphic))]
public class VertexMesh : BaseMeshEffect
{
    public Vector3[] myVertex = new Vector3[6];
    public override void ModifyMesh(VertexHelper vh)
    {
        List<UIVertex> vertexlist = new List<UIVertex>();
        vh.GetUIVertexStream(vertexlist);
        ModifyVertices(vertexlist);
        vh.Clear();
        vh.AddUIVertexTriangleStream(vertexlist);
        
    }

    public void ModifyVertices(List<UIVertex> vList)
    {
        if (IsActive() == false || vList == null || vList.Count == null)
        {
            return;
        }

        for(int i = 0; i < vList.Count; i++)
        {
            UIVertex tmpV = vList[i];
            tmpV.position = myVertex[i];
            vList[i] = tmpV;
        }
    }

}
