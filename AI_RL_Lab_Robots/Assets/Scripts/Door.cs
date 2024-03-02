using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorActivator : MonoBehaviour
{
    public UnityEvent onActivate { get; } = new UnityEvent();
    public UnityEvent onDeactivate { get; } = new UnityEvent();
}

public class Door : MonoBehaviour
{
    private DoorActivator[] activators;

    private int activeCounter = 0;
    public UnityEvent onDoorOpen;
    public bool door_State;

    public int countOfBoxes = 0;
    public int countOfButtons = 0;


    public void getCounts(int countOfBoxes, int countOfButtons)
    {
      this.countOfBoxes = countOfBoxes;
      this.countOfButtons = countOfButtons;  
    }

    public bool GetState()
    {
        return door_State;
    }

   
    private void OnActivate()
    {
        activeCounter++;
        if (activeCounter == activators.Length) Open();
    }
    private void onDeactivate()
    {
        activeCounter--;
    }
    public void ResetActivators(DoorActivator[] newActivators)
    {
        if (activators != null)
            foreach (var activator in activators)
            {
                activator.onActivate.RemoveListener(OnActivate);
                activator.onDeactivate.RemoveListener(onDeactivate);
            }
        activators = newActivators;
        gameObject.SetActive(true);
        door_State = false;
        foreach (var activator in activators)
        {
            activator.onActivate.AddListener(OnActivate);
            activator.onDeactivate.AddListener(onDeactivate);
        }
    }



    void Open()
    {
        door_State = true;
        gameObject.SetActive(false);
        onDoorOpen.Invoke();
    }
}
