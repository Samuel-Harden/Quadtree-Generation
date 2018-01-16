﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CubeMeshData
{
    
    public static Vector3[] vertices = {
        new Vector3( 1,  2,  1),
        new Vector3(-1,  2,  1),
        new Vector3(-1, -2,  1),
        new Vector3( 1, -2,  1),
        new Vector3(-1,  2, -1),
        new Vector3( 1,  2, -1),
        new Vector3( 1, -2, -1),
        new Vector3(-1, -2, -1)
    };

    public static int[][] face_triangles = {
        new int [] { 0, 1, 2, 3},
        new int [] { 5, 0, 3, 6},
        new int [] { 4, 5, 6, 7},
        new int [] { 1, 4, 7, 2},
        new int [] { 5, 4, 1, 0},
        new int [] { 3, 2, 7, 6}
    };

    public static Vector3[] FaceVertices(int _dir, float _scale)
    {
        Vector3[] fv = new Vector3[4];

        for(int i = 0; i < fv.Length; i++)
        {
            fv[i] = vertices[face_triangles[_dir][i]] * _scale;
        }

        return fv;
    }
}
