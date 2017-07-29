﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TinHead_Developer
{
    public class CarSelection : MonoBehaviour
    {

        // Use this for initialization
        public GameObject[] Cars;
        int movement = 0;
        public Camera Cam;
		public GameObject [] CarsSelection;
		public int[] StarsForUnlock;
		public GameObject PlayButton;
		public GameObject Lock;
		public Text TotalStars;
		public Text RequiredStars;
        void Start()
        {
			
			SoundManager.Instance.PlaySound ("CarSelection");
			CarsSelection [0].SetActive (true);
			ConsoliAds.Instance.ShowInterstitial (1);
			TotalStars.text = "x"+Preferences.Instance.TotalStars.ToString ();

            if (CarsSelection[0].activeInHierarchy)
            {
                Lock.SetActive(false);
            }
            else
                Lock.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            Cam.gameObject.transform.position = Vector3.MoveTowards(Cam.transform.position, Cars[movement].transform.position, Time.deltaTime * 20f);
        }

        public void LoadLevel()
        {
            GameManager.Instance.SelectedCar = movement;
            GameManager.Instance.Play(3);
        }

        //Activates iTS Manager and iTS Spawner at Runtime
        public void LoadTraffic()
        {
            foreach (var item in GameObject.FindGameObjectsWithTag("Traffic"))
            {
                item.SetActive(true);
                Debug.Log(item.name + "   " + item.activeInHierarchy + "    " + item.activeSelf);
            }
        }
        public void NextCar()
        {

            if (movement < Cars.Length - 1)
            {
				CarsSelection [movement].SetActive (false);
                movement++;

				if (Preferences.Instance.TotalStars >= StarsForUnlock [movement]) {
					PlayButton.SetActive (true);
					Lock.SetActive (false);
				} else {
					PlayButton.SetActive (false);
					Lock.SetActive (true);
					RequiredStars.text = "Require " + StarsForUnlock [movement] + " Stars"; 
				}
				CarsSelection [movement].SetActive (true);
            }
        }
        public void PreviousCar()
        {
            if (movement > 0)
            {
				CarsSelection [movement].SetActive (false);

                movement--;
				if (Preferences.Instance.TotalStars >= StarsForUnlock [movement]) {
					PlayButton.SetActive (true);
					Lock.SetActive (false);
				} else {
					PlayButton.SetActive (false);
					Lock.SetActive (true);
					RequiredStars.text = "Require " + StarsForUnlock [movement] + " Stars"; 
				}
				CarsSelection [movement].SetActive (true);

            }
        }
    }
}
