using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsCondition : MonoBehaviour
{

    [SerializeField] GameObject[] open_doors;
    [SerializeField] GameObject[] closed_doors;

    private bool is_open = true;

    
    void Start()
    {
        DoorsController doorsController = GetComponent<DoorsController>();
        doorsController.onDoorChanged += DoorsController_onDoorChanged;
    }

    private void DoorsController_onDoorChanged(object sender, System.EventArgs e)
    {
        for (int i = 0; i < open_doors.Length; i++)
        {
            //j - open i - closed
            open_doors[i].SetActive(!is_open);
        }
        for (int i = 0; i < closed_doors.Length; i++)
        {
            closed_doors[i].SetActive(is_open);
        }

        is_open = !is_open;
    }
}
