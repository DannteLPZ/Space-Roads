using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceEventManager : MonoBehaviour
{
    [SerializeField] List<SpaceEvent> listSpaceEvent= new();
    [SerializeField] Image eventImage;
    [SerializeField] GameEvent onEventSelected;
    private SpaceEvent selectedEvent;


    public void ModNumProjectiles(){
        PlayerFire playerFire = FindObjectOfType<PlayerFire>();
        if (playerFire != null)
        {
            int currentNumProjectiles = playerFire.ProjectileCount;
            int numProjectilesValue = int.Parse(selectedEvent.Arguments);
            playerFire.SetProjectileCount(currentNumProjectiles + numProjectilesValue);
        }
    }

    public void ModHealth(){

        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth!= null)
        {
            int damageValue = int.Parse(selectedEvent.Arguments);
            if(damageValue<0){
                playerHealth.TakeDamage(damageValue);
            }
            else if(damageValue>0){
                playerHealth.RestoreHealth(damageValue);
            }
        }
    }

    public void ModSpeed (){
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            float speedValue = float.Parse(selectedEvent.Arguments);
            float percentage = speedValue/100;

            if(speedValue > 0){
                playerController.ModifySpeed(1.0f+percentage);
            }
            else if(speedValue < 0){
                playerController.ModifySpeed(1.0f-percentage);
            }
        }
    }

    public void ModScore (){
        GameManager.Instance.AddScore(int.Parse(selectedEvent.Arguments));
    }

    public void ModShootSpeed (string shootSpeed){   
    }

    public void ModShields (string shield){    
    }

    public void ModDamage (string damage){
    }

    [ContextMenu("Select Random Event")]
    public void SelectEvent(){
        if (listSpaceEvent.Count == 0) return;
        int randomIndex = Random.Range(0, listSpaceEvent.Count);
        selectedEvent = listSpaceEvent[randomIndex];
        ResolveEvent();
    }

    public void ResolveEvent(){
        if (selectedEvent == null) return;
        eventImage.sprite=selectedEvent.EventImage;
        onEventSelected.Invoke();

        Invoke(selectedEvent.Method, 0);
        listSpaceEvent.Remove(selectedEvent);

    }
}

