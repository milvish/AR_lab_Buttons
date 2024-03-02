using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class GameEnvController : MonoBehaviour
{   

    public int buttonsOnEpisode = 3;
    public int boxesOnEpisode = 3;

    private Agent agent;
    public GridedDistributor buttonsDistributor;
    public GridedDistributor boxDistributor;
    public GridedDistributor agentsDistributor;

    public Door door;
    public MeshCollider goal;
    bool IsOpenedBefore;

    

    //public Button buttonMember;
    private List<Button> buttonsScene;

    private ButtonEventHandler buttonEventHandler;
    void Start()
    {
        ResetScene();

        
    }

    void ResetScene()
    {
        
        var buttons = buttonsDistributor.Respawn(buttonsOnEpisode);
        var boxes = boxDistributor.Respawn(boxesOnEpisode);
    
    

        var activators = new Button[buttons.Length];
        for (var i = 0; i < buttons.Length; i++)
        {
            activators[i] = buttons[i].GetComponent<Button>();
            activators[i].getCounts(boxes.Length, buttons.Length);
            //activators[i].getCountPressedByBoxes(0);
            
            //activators[i].state = ButtonStates.Unpressed;
            //activators[i].mesh.material = activators[i].unpressedMaterial;
            //activators[i].TotalPressedButtons = 0;
            //activators[i].collisionsCount = 0;
            
        }
        door.ResetActivators(activators);
        IsOpenedBefore = false;
        goal.gameObject.SetActive(false);
        

        agent = agentsDistributor.Respawn(1)[0].GetComponent<Agent>();

        buttonEventHandler = new ButtonEventHandler();
        Button[] allButtons = FindObjectsOfType<Button>();
        buttonsScene = new List<Button>(allButtons);
        buttonEventHandler.Initialize(buttonsScene);
        foreach (Button button in buttonsScene) {
            button.OnButtonPressed += buttonEventHandler.OnButtonPressed;
            button.OnButtonReleased += buttonEventHandler.OnButtonReleased;
        }
        buttonEventHandler.totalPressedButtons = 0;
    
    }

    public void OnGoalTriggered()
    {
        
        
        agent.AddReward(5f);
        agent.EndEpisode();
        /*

        foreach (Button button in buttonsScene) {
            button.state = ButtonStates.Unpressed;
            button.mesh.material = button.unpressedMaterial;
            //button.countPressedByBoxes = 0;
            button.TotalPressedButtons = 0;
            button.collisionsCount = 0; 
        }
        buttonEventHandler.totalPressedButtons = 0;
    */
        //buttonEventHandler.ResetTotalPressedButtons();
        ResetScene();
              
        
        
    }

    public void ActivateDoor()
    {
        if (!IsOpenedBefore)
        {
            agent.AddReward(1f);
            IsOpenedBefore = true;
            goal.gameObject.SetActive(true);
        }
        else
        {
            agent.AddReward(-1f);
        }
        
    }

    void FixedUpdate()
    {
    }
}
