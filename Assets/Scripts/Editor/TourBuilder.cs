using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class TourBuilder : Editor {

    string tourId;
    UnityEngine.Object jsonFile;
    bool loadFromWebsite;
    bool loadFromFile;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Label("Tour Sources", EditorStyles.boldLabel);

        loadFromWebsite = EditorGUILayout.BeginToggleGroup("Load from Online ID?", loadFromWebsite);
        tourId = EditorGUILayout.TextField("Tour ID", tourId);
        EditorGUILayout.EndToggleGroup();
        loadFromFile = EditorGUILayout.BeginToggleGroup("Load from static file?", loadFromFile);
        jsonFile = EditorGUILayout.ObjectField("File to use", jsonFile, typeof(TextAsset), false);
        EditorGUILayout.EndToggleGroup();
        

        GameManager gm = (GameManager)target;
        if (GUILayout.Button("Build Tour"))
        {
            if(loadFromWebsite && loadFromFile)
            {
                Debug.Log("Error! Can only choose file or website");
            }
            else if (loadFromFile)
            {
                LoadFromFile();
            }
            else if (loadFromWebsite)
            {
                LoadFromWebsite();
            }
            else
            {
                Debug.Log("Error! Must provide a source");
            }
        }
    }

    private void LoadFromWebsite()
    {
        throw new NotImplementedException();
    }

    private void LoadFromFile()
    {

    }
}
