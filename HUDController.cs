﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {
    /*
     * Generating a singleting. _instance is going to contain the instance of a HUDController type class.
     * The class Instance of type HUDController will return the _instance of the HUDController class, which is initialized on Awake() and points to this script/class.
     */
    private static HUDController _instance;

    public static HUDController Instance {
        get {
            return _instance;
        }
    }

    void Awake() {
        _instance = this;
    }

    [Header("Spell Controller")]
    public RawImage [] spellIcons;
    string [] spellNames;

    [Header("Health Display")]
    /*
     * We take the whole game object, instead of just the slider component, 
     * so we can control whether the slider is displayed or not 
     */
    public GameObject enemyHealth;
    public GameObject playerHealth;

    void Start(){
        spellNames = new string[3];
    }
    

    public void changeSpell(int spellSlot, Texture icon, string name){
        // int emptySlot = findEmpty(name);

        // If empty slot is available, then uses that empty slot instead
        // if(emptySlot < 3)
        //     spellSlot = emptySlot;
            
        spellIcons[spellSlot].texture = (Texture) icon;
            spellNames[spellSlot] = name;
    }

    public bool containsSpell(string name){
        foreach(string s in spellNames){
            if(s == name)
                return true;
        }
        return false;

    }

    public void UpdateHealth(string tag, int ammount) {
        switch (tag) {
            case "Player":
                playerHealth.GetComponent<Slider>().value = ammount;
                break;
            case "Enemy":
                /*
                 * Only display the health if it is not being displayed already
                 * and if the health is greater than 0. We do this to avoid
                 * stacking coroutines one after the other, giving the sense of
                 * lag between updates.
                 */
                enemyHealth.GetComponent<Slider>().value = ammount;
                if (!enemyHealth.activeSelf && ammount > 0) {
                    StartCoroutine(disableInNSeconds(enemyHealth, 1.5f));
                }
                break;
        }
    }

    public IEnumerator disableInNSeconds (GameObject target, float seconds) {
        target.SetActive(!enemyHealth.activeSelf);
        yield return new WaitForSeconds(seconds);
        target.SetActive(!enemyHealth.activeSelf);
    }

}



// CANVASCONTROLLER SCRIPT
// Declare an array