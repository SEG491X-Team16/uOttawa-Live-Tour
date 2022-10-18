using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/**
 * This class handles the building highlights on the AR map of campus.
 */
public class BuildingHighlight : MonoBehaviour
{
    private string[] buildings = {"15/17", "34/36", "38", "40", "90U", "109", "110", "143", "227", "538/540", "542", "555", "559", "600", "603", "631", "ARC", "ATK", "AWHC", "BRS", "BSC", "CBY", "CRG", "CRX", "CTE", "DMS", "DRO", "FSS", "FTX", "GNN", "GSD", "HGN", "HNN", "HSY", "KED", "LABO", "LBC", "LMX", "LPR", "LR3", "LRR", "MCE", "MHN", "MNN", "MNO", "MNT", "MRD", "MRN", "MRT", "ODL", "PRZ", "SCR", "SMD", "SMN", "STE", "STM", "STN", "STT", "SWT", "TBT", "THN", "UCU", "VNR", "WCA", "WLD"};

    // Start is called before the first frame update
    void Start()
    {
        AddOutlineComponent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddOutlineComponent() {
        for (int i = 0; i < transform.childCount; i ++){
            if (Array.Exists(buildings, element => element == transform.GetChild(i).name)){
                var outline = transform.GetChild(i).gameObject.AddComponent<Outline>();
                outline.OutlineColor = Color.red;
                outline.OutlineWidth = 5f;
                outline.OutlineMode = Outline.Mode.OutlineVisible;
                outline.enabled = false;

            }
        }
    }

    public bool SetBuildingHighlight(string name) {
        for (int i = 0; i < transform.childCount; i ++){
            if (transform.GetChild(i).name == name){
                var outline = transform.GetChild(i).GetComponent<Outline>();
                outline.enabled = true;

                return true;
            }
        }
        return false;
    }

    public bool ClearBuildingHighlight(string name) {
        for (int i = 0; i < transform.childCount; i ++){
            if (transform.GetChild(i).name == name){
                var outline = transform.GetChild(i).GetComponent<Outline>();
                outline.enabled = false;

                return true;
            }
        }
        return false;
    }
        
}
