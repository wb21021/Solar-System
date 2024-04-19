using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XCharts.Runtime;

public class DopplerGraph : MonoBehaviour
{
    private BarChart chart;

    void Awake()
    {
        chart = this.GetComponent<BarChart>();
        chart.ClearData();
    }

    public void UpdateSpectra(List<double> wavelengths)
    {
        chart.ClearData();
        for (int i = 0; i < wavelengths.Count; i++)
        {
            chart.AddXAxisData("x" + wavelengths[i]);
            chart.AddData(0, wavelengths[i], 1.0);
        }
        
        chart.RefreshChart();
        Debug.Log("I ahve been callled =======================================" + wavelengths);
    }
}
