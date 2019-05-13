using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VRTour.Serialize;

[CustomEditor(typeof(TourBuilderScriptable))]
public class TourBuilderScriptableEditor : Editor
{

    string tourId;
    UnityEngine.Object jsonFile;
    bool loadFromWebsite = true;
    TourBuilderScriptable tb;

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


        tb = (TourBuilderScriptable)target;
        if (GUILayout.Button("Load Tour"))
        {
            if (loadFromWebsite)
            {
                //LoadFromWebsite();
            }
            else
            {
                TextAsset json = (TextAsset)jsonFile;
                Tour t = Utility.CreateFromJSON(json.text);
                tb.LoadTour(t);
            }
        }

        if (GUILayout.Button("Build Tour"))
        {
            tb.BuildTour();
        }
    }
}
