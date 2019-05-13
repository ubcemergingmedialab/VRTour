using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using VRTour.Serialize;

[CustomEditor(typeof(GameManager))]
public class TourBuilder : Editor {

    string tourId;
    UnityEngine.Object jsonFile;
    bool loadFromWebsite = true;
    GameManager gm;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Label("Tour Sources", EditorStyles.boldLabel);

        loadFromWebsite = EditorGUILayout.BeginToggleGroup("Load from Online ID?", loadFromWebsite);
        tourId = EditorGUILayout.TextField("Tour ID", tourId);
        EditorGUILayout.EndToggleGroup();
        loadFromWebsite = !EditorGUILayout.BeginToggleGroup("Load from static file?", !loadFromWebsite);
        jsonFile = EditorGUILayout.ObjectField("File to use", jsonFile, typeof(TextAsset), false);
        EditorGUILayout.EndToggleGroup();
        

        gm = (GameManager)target;
        if (GUILayout.Button("Build Tour"))
        {
            if (loadFromWebsite)
            {
                LoadFromWebsite();
            }
            else
            {
                LoadFromFile();
            }
        }
    }

    private void LoadFromWebsite()
    {
        throw new NotImplementedException();
    }

    private void LoadFromFile()
    {
        TextAsset json = (TextAsset) jsonFile;
        Tour toBuild = Utility.CreateFromJSON(json.text);
        gm.BuildTour(toBuild);
    }
}
