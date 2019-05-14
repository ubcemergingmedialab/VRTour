using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTour.Serialize;

/// <summary>
/// Defines behaviour for tour prefab. Currently doesn't really do anything, just displays the name of the tour on a canvas
/// </summary>
public class TourBehaviour : MonoBehaviour {

    [SerializeField]
    private Text tourName;

    private Tour tour;

    public TourBuilderScriptable instance;

    public void Setup(Tour t, TourBuilderScriptable tb)
    {
        instance = tb;
        tour = t;
        tourName.text = tour.name;
    }
}
