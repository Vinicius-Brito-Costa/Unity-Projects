using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DefaultSubmenu : MonoBehaviour, ISubmenu{
    [SerializeField]
    private List<SO_SubmenuOption> _options;
    [SerializeField]
    private int _textSize;
    
    public void Start(){
        _options.ForEach(option => {
            TextMeshProUGUI opt = Instantiate(option.GetTextMesh());
            opt.text = option.GetOptionKey().ToString();
            opt.fontSize = _textSize;
            opt.transform.SetParent(gameObject.transform);
            opt.gameObject.name = opt.text;
            //Instantiate(opt, Vector3.zero, Quaternion.identity);
        });
    }
    public void Action(){
        
    }
    public void Move(UIControlEnum move){

    }
}