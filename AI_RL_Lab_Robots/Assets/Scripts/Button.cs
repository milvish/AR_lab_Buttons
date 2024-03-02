using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonStates {
    Unpressed,
    Pressed
}

public class Button : DoorActivator
{
    public bool buttonState;
    
    int countOfBoxes = 0;
    int countOfButtons = 0;
    bool status = false;

    //public int countPressedByBoxes;
    public void getCounts(int countOfBoxes, int countOfButtons)
    {
      this.countOfBoxes = countOfBoxes;
      this.countOfButtons = countOfButtons;  
    }
    /*
    public void getCountPressedByBoxes(int countPressedByBoxes){
        this.countPressedByBoxes = countPressedByBoxes;
    }
    */

    public delegate void ButtonPressedEventHandler(Button button);
    public event ButtonPressedEventHandler OnButtonPressed;
    public event ButtonPressedEventHandler OnButtonReleased;

    public int TotalPressedButtons { get; set; }

    public MeshRenderer mesh;
    public ButtonStates state = ButtonStates.Unpressed;
    [SerializeField]
    Material pressedMaterial;
    [SerializeField]
    public Material unpressedMaterial;

    


    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.material = unpressedMaterial;

        if (countOfBoxes < countOfButtons){
            status = true;
        }

    }

    public int collisionsCount = 0;

    
    
    
    void OnCollisionEnter(Collision collision) {

        collisionsCount++;
        if (state == ButtonStates.Unpressed) {
            state = ButtonStates.Pressed;
            mesh.material = pressedMaterial;
            onActivate.Invoke();
            if (collision.gameObject.CompareTag("block")){
                OnButtonPressed?.Invoke(this);
                Debug.Log(string.Format("PressedByBox"));
            }
        }
        
        
    }
    void OnCollisionExit(Collision collision) {
        collisionsCount--;
        if (collisionsCount <= 0 && state == ButtonStates.Pressed)
        {
            Debug.Log(string.Format("Count of all Boxes: {0}", countOfBoxes));
            Debug.Log(string.Format("<3 Total PressedButtons: {0}", TotalPressedButtons));
            if ((status == true) && collision.gameObject.CompareTag("agent") && (TotalPressedButtons == countOfBoxes)){
                collisionsCount++;
            } else {
                state = ButtonStates.Unpressed;
                mesh.material = unpressedMaterial;
                onDeactivate.Invoke();
                if (collision.gameObject.CompareTag("block")){
                    OnButtonReleased?.Invoke(this);
                }
            }
        } 


    }
}
