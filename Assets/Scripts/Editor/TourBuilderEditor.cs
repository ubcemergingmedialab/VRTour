using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using VRTour.Serialize;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VRTour
{
    /// <summary>
    /// Custom editor for TourBuilderScriptable. Allows for loading tours from file or url 
    /// \todo implement load from url
    /// </summary>
    [CustomEditor(typeof(TourBuilderScriptable))]
    public class TourBuilderScriptableEditor : Editor
    {

        string tourId;
        UnityEngine.Object jsonFile;
        bool loadFromWebsite = true;
        static TourBuilderScriptable tb;
        static UnityWebRequest www;
        string loadStatus = "Status";

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
            if (GUILayout.Button("Reset"))
            {
                tb.ResetTour();
                loadStatus = "Status";
                EditorApplication.update -= EditorUpdate;
            }
            if (GUILayout.Button("Load Tour"))
            {
                Tour t;
                if (loadFromWebsite)
                {
                    www = Receiver.GetFromId(tourId);
                    www.SendWebRequest();
                    loadStatus = "Loading from website...";
                    EditorApplication.update += EditorUpdate;
                }
                else
                {
                    loadStatus = "Loaded from file";
                    TextAsset json = (TextAsset)jsonFile;
                    t = Serialize.Utility.CreateFromJSON(json.text);
                    tb.LoadTour(t);

                }
            }
            if (GUILayout.Button("Build Tour"))
            {
                tb.BuildTour();
                loadStatus = "Built!";
            }
            EditorGUILayout.HelpBox(loadStatus, MessageType.Info);
        }

        static void EditorUpdate()
        {
            if (!www.isDone)
                return;

            if (www.isNetworkError)
            {
                EditorUtility.DisplayDialog("Tour Builder", www.error, "Ok");
            }
            else
            {
                EditorUtility.DisplayDialog("Tour Builder", "Tour loaded OK!", "Ok");
                JObject config = JObject.Parse(www.downloadHandler.text);
                Tour t = Serialize.Utility.CreateFromJSON(config.SelectToken("config").ToString());
                tb.LoadTour(t);
            }
            EditorApplication.update -= EditorUpdate;
        }

    }
}
