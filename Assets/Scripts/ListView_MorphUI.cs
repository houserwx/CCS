using System.Collections;
using UnityEngine;
using MCS;
using MCS.FOUNDATIONS;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class ListView_MorphUI : MonoBehaviour
{

	[SerializeField]
	public GameObject mcsModel;
	public GameObject UIMenu;
	public GameObject MorphSliderPrefab;
	public GameObject ButtonPanel;
	private MCSCharacterManager manager;
	public Dictionary<string,float> morphList = new Dictionary<string,float>();
	void Start()
	{
		int itemCount = 0;

		int pageSize = 10;
		manager = mcsModel.GetComponent<MCSCharacterManager>();
		var totalCount = manager.coreMorphs.morphLookup.Count;
		var pageCount = Mathf.RoundToInt( totalCount / pageSize) +1;
		for (int i = 0; i < pageCount; i++)
        {

        }			
		foreach (var m in manager.coreMorphs.morphLookup)
        {
			var mm = Instantiate(MorphSliderPrefab, UIMenu.transform);
			mm.name = m.Key;
			mm.GetComponentInChildren<TextMeshProUGUI>().text = m.Key;
			mm.GetComponentInChildren<Slider>().maxValue = 100f;
			mm.GetComponentInChildren<Slider>().minValue = 0f;
			mm.GetComponentInChildren<Slider>().value = 0f;
			mm.GetComponentInChildren<Slider>().name = m.Key;
			mm.GetComponentInChildren<Slider>().onValueChanged.AddListener(delegate { UpdateMorph(mm.GetComponent<Slider>()); });
			UIMenu.GetComponent<VerticalLayoutGroup>().childForceExpandHeight = true;
			itemCount++;
			if(itemCount >= pageSize)
            {
				mm.GetComponent<VerticalLayoutGroup>().childForceExpandHeight = false;
				mm.SetActive(false);

            }				
		}
	}

	public void SetBlendshapeValue(float value)
	{
		manager.SetBlendshapeValue("FBMHeavy", value);
	}
	public void UpdateMorph(Slider slider)
	{
		manager.SetBlendshapeValue(slider.name, slider.value);
	}
}