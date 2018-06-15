using UnityEngine;
using System.Collections;

public class TowerBuilder : MonoBehaviour
{
    public float waitingPeriod;
    LevelBuilder levelBuilder;
    WebCamPhotoCamera webCam;

    public bool active;

    private void Start()
    {
        levelBuilder = GetComponent<LevelBuilder>();
        webCam = GameObject.Find("Plane").GetComponent<WebCamPhotoCamera>();
        active = false;
    }

    public IEnumerator BuildTowers()
    {
        levelBuilder.ColorPaletteNames = GameObject.Find("LevelBuilder").GetComponent<LevelBuilder>().ColorPaletteNames;
        while (active)
        {
            foreach (var tower in GameObject.FindGameObjectsWithTag("tower"))
                Destroy(tower);
            levelBuilder.LegoBlocks = webCam.GetTexture();
            levelBuilder.BuildLevel();
            yield return new WaitForSeconds(waitingPeriod);
        }
    }
}
