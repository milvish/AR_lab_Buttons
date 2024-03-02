using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class ButtonEventHandler {
    public int totalPressedButtons;
    private List<Button> buttons;
    public void Initialize(List<Button> buttons)
    {
        this.buttons = buttons;
    }   

    public void OnButtonPressed(Button button) {
        //button.countPressedByBoxes++;
        totalPressedButtons++;
        foreach (Button b in buttons) {
            b.TotalPressedButtons = totalPressedButtons;
        }
       // Debug.Log("Count of Pressed Buttons: " + button.countPressedByBoxes);
        Debug.Log("Total Pressed Buttons: " + totalPressedButtons);
    }

    public void OnButtonReleased(Button button) {
        //button.countPressedByBoxes--;
        totalPressedButtons--;
        foreach (Button b in buttons) {
            b.TotalPressedButtons = totalPressedButtons;
        }
        //Debug.Log("Count of Pressed Buttons: " + button.countPressedByBoxes);
        Debug.Log("Total Pressed Buttons: " + totalPressedButtons);
    }
    
    public void ResetTotalPressedButtons()
    {
        totalPressedButtons = 0;
        foreach (Button button in buttons) {
          button.TotalPressedButtons = 0;
          //button.forButtonNewScene = true;
        }
    }
    
}
