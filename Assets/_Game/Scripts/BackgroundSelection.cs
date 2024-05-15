using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelection : MonoBehaviour
{
    [SerializeField] List<BackgroundManager> bgs = new List<BackgroundManager>();

    public GameObject contentPanel;
    public GameObject Background;
    public GameObject levelButtonPrefab;
    GameObject bgr;

    private void Start()
    {
        PopulateBackground();
    }

    public void OnClickBack()
    {
        this.gameObject.SetActive(false);
    }
    
    public void PopulateBackground()
    {
        foreach(var i in bgs)
        {
            GameObject newButton = Instantiate(i.btnPrefab, contentPanel.transform);
            int bgrId = bgs.IndexOf(i);
            newButton.GetComponent<Button>().onClick.AddListener(() => OnBackgroundButtonClicked(bgrId));
        }
    }
    void OnBackgroundButtonClicked(int bgrId)
    {
        if (Background.transform.GetChild(0) != null)
        {
            Destroy(Background.transform.GetChild(0).gameObject);
        }
        if (bgr != null)
        {
            Destroy(bgr);
        }
        bgr = Instantiate(bgs[bgrId].BackgrPrefab, Background.transform);
        OnClickBack();
    }


}
