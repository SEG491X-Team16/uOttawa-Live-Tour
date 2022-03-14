using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DropdownHandler : MonoBehaviour
{   

    public Text TextBox;

    // Start is called before the first frame update
    void Start()
    {
        var dropdown = transform.GetComponent<DropDown>();
        dropdown.options.Clear();

        List<string> items = new List<string>();
        items.Add("item1");
        items.Add("item2");


        foreach(var item in items)
        {
            dropdown.options.Add(new DropDown.OptionData() { text = item} );
        }


        dropdown.onValueChanged.AddListener(delegate {DropdownItemSelcted(dropdown);});

    }

    void DropdownItemSelcted(Dropdown dropdown){

        int index = dropdown.value;
        TextBox.text = dropdown.options[index].text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
