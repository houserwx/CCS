using System.Collections;
using UnityEngine;
using MCS;
using MCS.FOUNDATIONS;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UIElements;
using System.Linq;
public class ListView_MorphUXML : MonoBehaviour
{

	[SerializeField]
	public GameObject mcsModel;
	public GameObject UIMenu;
	public GameObject MorphSliderPrefab;
	public GameObject ButtonPanel;
	public MCSCharacterManager manager;
	//public TabbedView currentTab;
	private List<Slider> _sliders;
	//public List<TabbedView> tabs;
	public int tabDisplayed = 0;
	public UIDocument uiDocument;
	int pageSize = 10;
	public Dictionary<string, float> morphList = new Dictionary<string, float>();
	private void BuildUI()
	{

		if (mcsModel == null)
		{
			this.enabled = false;
		}
		else
		{
			int itemCount = 0;
			uiDocument = GetComponent<UIDocument>();

			//tabs = new List<TabbedView>();
			manager = mcsModel.GetComponent<MCSCharacterManager>();
			foreach (var morph in manager.coreMorphs.morphs)
			{
				//morph.attached = true;
				morphList.Add(morph.name, morph.value);
			}
			var totalCount = manager.coreMorphs.morphLookup.Count;
			var pageCount = Mathf.RoundToInt(totalCount / pageSize) + 1;

			_sliders = new List<Slider>();
			//currentTab = AddTab("0",uiDocument);
			//tabs.Add(currentTab);

			foreach (var m in manager.coreMorphs.morphLookup)
			{
				//var mm = Instantiate(MorphSliderPrefab);

				var mm = new Slider();

				mm.name = m.Key;
				mm.label = m.Key;
				mm.highValue = 100f;
				mm.lowValue = 0f;
				mm.value = 0f;
				mm.name = m.Key;
				mm.RegisterValueChangedCallback(delegate { UpdateMorph(mm); });
				itemCount++;
				_sliders.Add(mm);

			}
			PopTab(0);
		}
	}
	public void NextTab()
    {
		ClearTab();
		tabDisplayed++;
		PopTab(tabDisplayed);
		
    }
	public void PrevTab()
	{
		ClearTab();
		tabDisplayed--;
		if(tabDisplayed < 0)
        {
			tabDisplayed = 0;
        }
		PopTab(tabDisplayed);
	}
	public void ClearTab()
    {
		uiDocument.rootVisualElement.Clear();
	}
	public void PopTab(int idx)
    {
		var rangeStart = idx * pageSize;
		for(int i = rangeStart; i < rangeStart + pageSize;i++ )
        {
			if (i <= _sliders.Count())
			{
				uiDocument.rootVisualElement.contentContainer.Add(_sliders[i]);
			}
        }
		if (!(rangeStart + pageSize > _sliders.Count()))
		{
			var _nextButton = new Button();
			_nextButton.name = "Next";
			_nextButton.text = "Next";
			_nextButton.clicked += delegate { NextTab(); };
			uiDocument.rootVisualElement.Add(_nextButton);
		}
		if (idx != 0)
		{
			var _prevButton = new Button();
			_prevButton.name = "Prev";
			_prevButton.text = "Prev";

			_prevButton.clicked += delegate { PrevTab(); };

			uiDocument.rootVisualElement.Add(_prevButton);
		}
		var _resetButton = new Button();
		_resetButton.name = "Reset";
		_resetButton.text = "Reset";
		_resetButton.clicked += delegate { ResetAll(); };
		uiDocument.rootVisualElement.Add(_resetButton);
		var _exitButton = new Button();
		_exitButton.name = "Exit";
		_exitButton.text = "Exit";
		uiDocument.rootVisualElement.Add(_exitButton);
		_exitButton.clicked += delegate { Application.Quit(); };
	}
	public void ResetAll()
    {
		foreach(var s in _sliders)
        {
			s.value = 0;
        }
    }
/*	public TabbedView AddTab(string name, UIDocument uiDocument)
    {
		TabbedView newTabbedView = new TabbedView();
		newTabbedView.name = name;
		if (name != "0")
		{
			newTabbedView.visible = false;

		}
		newTabbedView.tabIndex = int.Parse(name);
		uiDocument.rootVisualElement.Add(newTabbedView);
		
	
		return newTabbedView;
	}*/
	public void SetBlendshapeValue(float value)
	{
		manager.SetBlendshapeValue("FBMHeavy", value);
	}
	public void UpdateMorph(Slider slider)
	{
		manager.SetBlendshapeValue(slider.name, slider.value);
	}
}