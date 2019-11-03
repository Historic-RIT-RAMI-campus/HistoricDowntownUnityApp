using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;
using System;

public class ARTapPlace : MonoBehaviour
{
    public GameObject historicRIT;
    public GameObject placementIndicator;

    private ARSessionOrigin arOrigin;
    private Pose placementPose;
    private bool PlacementPoseIsValid = false;
    private bool placed = false;
    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
    }

    void Update()
    {
        UpdatePlacementPose();
        if(!placed)
        {
            UpdatePlacementIndicator();
        }
        
        if(PlacementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !placed)
        {
            PlaceObject();
            placed = true;
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arOrigin.GetComponent<ARRaycastManager>().Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        PlacementPoseIsValid = hits.Count > 0;
        if (PlacementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    private void UpdatePlacementIndicator()
    {
        if (PlacementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void PlaceObject()
    {
        placementIndicator.SetActive(false);
        Instantiate(historicRIT, placementPose.position, placementPose.rotation);
    }
}
