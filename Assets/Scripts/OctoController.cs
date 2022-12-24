using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class OctoController : MonoBehaviour
{
    public GameObject mcsModelPrefab;
    public GameObject uiDocu;
    // Start is called before the first frame update
    void Start()
    {
        var mcsModel = Instantiate(mcsModelPrefab);
        mcsModel.SetActive(true);
        var menuScript = uiDocu.GetComponentInChildren<ListView_MorphUXML>();
        if(menuScript != null)
        {
            menuScript.mcsModel = mcsModel;
            menuScript.manager = mcsModel.GetComponentInChildren<MCS.MCSCharacterManager>();
            menuScript.Invoke("BuildUI", 0f);
            menuScript.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
