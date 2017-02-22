using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrestigeSelectorButtonsController : MonoBehaviour {
    // UNITY CONFIGURABLE
    public float TransitTime = 0.5f;
    // END UNITY CONFIGURABLE

    private PrestigeController Prestiges;
    private Button NextPrestigeButton;
    private Button PreviousPrestigeButton;

    private int PrestigeIndex = 0;
    private float ArrivalTime = 0;

	// Use this for initialization
	void Start () {
        Prestiges = FindObjectOfType<PrestigeController>();
        NextPrestigeButton = transform.FindChild("NextPrestigeButton").GetComponent<Button>();
        NextPrestigeButton.onClick.AddListener(GoNext);
        PreviousPrestigeButton = transform.FindChild("PreviousPrestigeButton").GetComponent<Button>();
        PreviousPrestigeButton.onClick.AddListener(GoPrevious);
    }
	
	// Update is called once per frame
	void Update () {
        NextPrestigeButton.gameObject.SetActive(Prestiges.transform.childCount > 1);
        NextPrestigeButton.interactable = PrestigeIndex > 0;
        PreviousPrestigeButton.gameObject.SetActive(Prestiges.transform.childCount > 1);
        PreviousPrestigeButton.interactable = PrestigeIndex < Prestiges.transform.childCount - 1;
        
        Vector3 oldCameraPosition = Camera.main.transform.position;
        Vector3 currentPrestigePosition = Prestiges.transform.GetChild(Prestiges.transform.childCount - PrestigeIndex - 1).transform.position;
        Vector3 ultimateCameraPosition = new Vector3(currentPrestigePosition.x, oldCameraPosition.y, oldCameraPosition.z);
        
        if (ArrivalTime > Time.time)
        {
            Camera.main.transform.position = Vector3.Slerp(oldCameraPosition, ultimateCameraPosition, (Time.time - (ArrivalTime - TransitTime)) / TransitTime);
        }
        else
        {
            Camera.main.transform.position = ultimateCameraPosition;
        }
	}

    public void GoPrevious()
    {
        PrestigeIndex++;
        ArrivalTime = Time.time + TransitTime;
    }

    public void GoNext()
    {
        PrestigeIndex--;
        ArrivalTime = Time.time + TransitTime;
    }
}
