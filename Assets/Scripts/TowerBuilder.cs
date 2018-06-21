using UnityEngine;
using System.Collections;
using UnityEngine.AI;

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
                tower.tag = "old";
            foreach (var tower in GameObject.FindGameObjectsWithTag("PowerConduit"))
            {
                tower.tag = "old";
                tower.GetComponent<PowerConduitScript>().UnlinkTowers();
            }
            yield return webCam.CalculateTexture();
            levelBuilder.LegoBlocks = webCam.calculatedTexture;
            yield return levelBuilder.BuildLevel();
            foreach (var tower in GameObject.FindGameObjectsWithTag("old"))
                Destroy(tower);
            foreach (var tower in GameObject.FindGameObjectsWithTag("PowerConduit"))
                tower.GetComponent<PowerConduitScript>().LinkTowers();
            yield return new WaitForSeconds(waitingPeriod);
        }
    }
}
