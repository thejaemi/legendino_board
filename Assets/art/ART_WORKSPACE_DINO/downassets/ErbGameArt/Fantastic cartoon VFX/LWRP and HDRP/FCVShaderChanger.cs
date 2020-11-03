using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

// public class FCVShaderChanger : EditorWindow
public class FCVShaderChanger
{

    // [MenuItem("Window/FCV Pipeline changer")]
    public static void ShowWindow()
    {
        // EditorWindow.GetWindow(typeof(FCVShaderChanger));
    }

    public void OnGUI()
    {
        GUILayout.Label("Change pipeline to:");

        if (GUILayout.Button("Standard RP"))
        {
            FindShaders();
            ChangeToSRP();
        }
        if (GUILayout.Button("Lightweight RP"))
        {
            FindShaders();
            ChangeToLWRP();
        }
        if (GUILayout.Button("HD RP (From Unity 2018.3+)"))
        {
            FindShaders();
            ChangeToHDRP();
        }
    }

    Shader LightGlow;
    Shader Blood;
    Shader Blend_Tornado;

    Shader LightGlow_LWRP;
    Shader Blood_LWRP;
    Shader Blend_Tornado_LWRP;

    Shader LightGlow_HDRP;
    Shader Blood_HDRP;
    Shader Blend_Tornado_HDRP;

    Material[] shaderMaterials;

    private void FindShaders()
    {
        if (Shader.Find("ERB/Particles/LightGlow") != null) LightGlow = Shader.Find("ERB/Particles/LightGlow");
        if (Shader.Find("ERB/Particles/Blood") != null) Blood = Shader.Find("ERB/Particles/Blood");
        if (Shader.Find("ERB/Particles/Blend_Tornado") != null) Blend_Tornado = Shader.Find("ERB/Particles/Blend_Tornado");

        if (Shader.Find("ERB/LWRP/Particles/LightGlow") != null) LightGlow_LWRP = Shader.Find("ERB/LWRP/Particles/LightGlow");
        if (Shader.Find("ERB/LWRP/Particles/Blood") != null) Blood_LWRP = Shader.Find("ERB/LWRP/Particles/Blood");
        if (Shader.Find("ERB/LWRP/Particles/Blend_Tornado") != null) Blend_Tornado_LWRP = Shader.Find("ERB/LWRP/Particles/Blend_Tornado");

        if (Shader.Find("ERB/HDRP/Particles/LightGlow") != null) LightGlow_HDRP = Shader.Find("ERB/HDRP/Particles/LightGlow");
        if (Shader.Find("ERB/HDRP/Particles/Blood") != null) Blood_HDRP = Shader.Find("ERB/HDRP/Particles/Blood");
        if (Shader.Find("ERB/HDRP/Particles/Blend_Tornado") != null) Blend_Tornado_HDRP = Shader.Find("ERB/HDRP/Particles/Blend_Tornado");

        // string[] folderMat = AssetDatabase.FindAssets("t:Material", new[] { "Assets/ErbGameArt" });
        // shaderMaterials = new Material[folderMat.Length];

        // for (int i = 0; i < folderMat.Length; i++)
        // {
        //     var patch = AssetDatabase.GUIDToAssetPath(folderMat[i]);
        //     shaderMaterials[i] = (Material)AssetDatabase.LoadAssetAtPath(patch, typeof(Material));
        // }
    }

    private void ChangeToLWRP()
    {

        foreach (var material in shaderMaterials)
        {
            if (Shader.Find("ERB/LWRP/Particles/LightGlow") != null)
            {
                if (material.shader == LightGlow || material.shader == LightGlow_HDRP)
                {
                    material.shader = LightGlow_LWRP;
                }
            }
            /*----------------------------------------------------------------------------------------------------*/
            if (Shader.Find("ERB/LWRP/Particles/Blood") != null)
            {
                if (material.shader == Blood || material.shader == Blood_HDRP)
                {
                    material.shader = Blood_LWRP;
                }
            }
            /*----------------------------------------------------------------------------------------------------*/
            if (Shader.Find("ERB/LWRP/Particles/Blend_Tornado") != null)
            {
                if (material.shader == Blend_Tornado || material.shader == Blend_Tornado_HDRP)
                {
                    material.shader = Blend_Tornado_LWRP;
                }
            }             
        }
    }


    private void ChangeToSRP()
    {

        foreach (var material in shaderMaterials)
        {
            if (Shader.Find("ERB/Particles/LightGlow") != null)
            {
                if (material.shader == LightGlow_LWRP || material.shader == LightGlow_HDRP)
                {
                    material.shader = LightGlow;
                }
            }
            /*----------------------------------------------------------------------------------------------------*/
            if (Shader.Find("ERB/Particles/Blood") != null)
            {
                if (material.shader == Blood_LWRP || material.shader == Blood_HDRP)
                {
                    material.shader = Blood;
                }
            }
            /*----------------------------------------------------------------------------------------------------*/
            if (Shader.Find("ERB/Particles/Blend_Tornado") != null)
            {
                if (material.shader == Blend_Tornado_LWRP || material.shader == Blend_Tornado_HDRP)
                {
                    material.shader = Blend_Tornado;
                }
            }
        }
    }

    private void ChangeToHDRP()
    {
        foreach (var material in shaderMaterials)
        {
            if (Shader.Find("ERB/HDRP/Particles/LightGlow") != null)
            {
                if (material.shader == LightGlow || material.shader == LightGlow_LWRP)
                {
                    material.shader = LightGlow_HDRP;
                }
            }
            /*----------------------------------------------------------------------------------------------------*/
            if (Shader.Find("ERB/HDRP/Particles/Blood") != null)
            {
                if (material.shader == Blood || material.shader == Blood_LWRP)
                {
                    material.shader = Blood_HDRP;
                }
            }
            /*----------------------------------------------------------------------------------------------------*/
            if (Shader.Find("ERB/HDRP/Particles/Blend_Tornado") != null)
            {
                if (material.shader == Blend_Tornado || material.shader == Blend_Tornado_LWRP)
                {
                    material.shader = Blend_Tornado_HDRP;
                }
            }
        }
    }
}