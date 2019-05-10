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

	

    public void Setup(Tour t)
    {
        tour = t;
        tourName.text = tour.name;
    }
}
