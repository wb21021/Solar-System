using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using XCharts.Runtime;

public class DopplerChangeInGraph : MonoBehaviour
{
    private BarChart chart;

    void Awake()
    {
        chart = this.GetComponent<BarChart>();
        chart.ClearData();
    }

    public void UpdateChangeInSpectra(List<double> wavelengths, List<float> original_wavelengths)
    {
        chart.ClearData();
        for (int i = 0; i < wavelengths.Count; i++)
        {
            double wavelengthChange = original_wavelengths[i] - wavelengths[i] ;
            chart.AddData(0, wavelengthChange, 1.0);
        }
        
        chart.RefreshChart();
    }
}
