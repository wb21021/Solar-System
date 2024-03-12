using UnityEngine;

public class DisplayCelestialBodyText : MonoBehaviour
{
    private GameObject CanvasBodyInfo;
    // Start is called before the first frame update
    void Start()
    {
        
        //CanvasBodyInfo.SetActive(false);
        //CanvasBodyInfo.GetComponentInChildren<TMP_Text>().text = ;
    }

    // Update is called once per frame
    public void ShowCelestialBodyCanvas()
    {
        GameObject CanvasBodyInfoPrefab = Resources.Load<GameObject>($"UIElements/CanvasCelestialBodyInfo");
        CanvasBodyInfo = Instantiate(CanvasBodyInfoPrefab);
        CanvasBodyInfo.SetActive(true);
    }
    public void HideCelestialBodyCanvas()
    {
        CanvasBodyInfo.SetActive(false);
    }
}
