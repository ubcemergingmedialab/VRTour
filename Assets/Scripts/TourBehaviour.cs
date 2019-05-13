using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTour.Serialize;

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
