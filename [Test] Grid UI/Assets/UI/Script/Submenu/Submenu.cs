using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Submenu : ISubmenu
{
    private UIColorSchema _colorSchema;

    [SerializeField]
    private List<SubmenuOption> _options;

    public void Start(){
        UpdateColor(_colorSchema);
    }
    public void Update(){
        if(!Application.isPlaying){
            UpdateColor(_colorSchema);
        }
    }
    public void AddOption(SubmenuOption option){
        if(!_options.Contains(option)){
            _options.Add(option);
        }
    }
    public override void Action(ISlot slot)
    {
        Debug.Log("Action!");
    }

    public override void Move(UIControlEnum move)
    {
        Debug.Log("Move! " + move);
    }

    public override void Close()
    {
        SetActiveActions(UIAction.ALL_ACTIONS);
        this.gameObject.SetActive(false);
    }
    public override void UpdateColor(UIColorSchema colorSchema)
    {
        if(colorSchema != null){
            _colorSchema = colorSchema;
            _options.ForEach(option => {
                option.UpdateColor(colorSchema);
            });
        }
    }
    public override void SetActiveActions(List<UIAction.Action> activeOptions)
    {
        _options.ForEach(option => {
            if(activeOptions.Contains(option.GetOptionType())){
                option.Activate();
            }
            else {
                option.Deactivate();
            }
        });
    }
}