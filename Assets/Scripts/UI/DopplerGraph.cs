using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using XCharts.Runtime;

public class DopplerGraph : MonoBehaviour
{
    private BarChart chart;
    private bool first = true;
    private List<double> firstList = new List<double>();

    void Awake()
    {
        chart = this.GetComponent<BarChart>();
        chart.ClearData();
    }

    public void UpdateSpectra(List<double> wavelengths, List<float> original_wavelengths)
    {
        chart.ClearData();
        for (int i = 0; i < wavelengths.Count; i++)
        {
            double wavelengthChange = original_wavelengths[i] - wavelengths[i] ;
            chart.AddData(0, wavelengthChange, 1.0);
            if (first)
            {
                firstList.Add(wavelengthChange);
            }
            chart.AddData(1, firstList[i], 1.0);
        }
        first = false;
        
        chart.RefreshChart();
    }
}
