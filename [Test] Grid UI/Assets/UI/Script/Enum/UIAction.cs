using System.Collections.Generic;

public class UIAction {
    public enum Action{
        Use,
        Move,
        Combine,
        Drop
    }

    public static List<Action> ALL_ACTIONS = new List<Action>(){
        Action.Use,
        Action.Move,
        Action.Combine,
        Action.Drop
    };

    private UIAction(){}
}