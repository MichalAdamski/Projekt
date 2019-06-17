using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.OVRInputWrapper;
using Assets.HUD;

namespace Assets
{
    public class OrientationSaver : MonoBehaviour
    {
        [SerializeField]
        private GameObject player;
        [SerializeField]
        private GameObject fullHead;

        private List<SavePoint> savePointList;

        private float loadSaveDelay = 1;
        private float confirmSaveDelay = 3;
        private int listNumber = 1;

        void Awake()
        {
            RegisterInputs();
        }

        private void RegisterInputs()
        {
            TimeDelay.IsSaveConfirmDelayEnd = true;
            TimeDelay.IsLoadSaveDelayEnd = true;
            savePointList = new List<SavePoint>();

            var inputRegistrator = OVRInputBinder.Instance.GetInputRegistrator(Consts.InputGroups.LocationSaving, Consts.InputSets.Standard);

            // przypisanie przyciskowi funckji: zapis
            inputRegistrator.RegisterButtonDownAction(OVRInput.Button.Start, SaveLocation);

            // przypisanie przyciskowi funckji: ladowanie pozniejszego i wczesniejszego zapisu zgodnie z aktualna pozycja na liscie
            inputRegistrator.RegisterButtonDownAction(OVRInput.Button.PrimaryThumbstick, LoadLocationUpList);
            inputRegistrator.RegisterButtonDownAction(OVRInput.Button.SecondaryThumbstick, LoadLocationDownList);

            // przypisanie przyciskowi funckji: wczytanie pierwszego i ostatniego zapisu
            inputRegistrator.RegisterButtonUpAction(OVRInput.Button.PrimaryThumbstick, LoadLastSave);
            inputRegistrator.RegisterButtonUpAction(OVRInput.Button.SecondaryThumbstick, LoadFirstSave);

            inputRegistrator.ActivateInputSet();
        }

        private void SaveLocation()
        {
            if(TimeDelay.IsSaveConfirmDelayEnd)
            {
                AskForSaveConfirmation();
            }
            else if(!TimeDelay.IsSaveConfirmDelayEnd)
            {     
                SavePoint savePoint = new SavePoint();

                savePoint.skullTransform = CreateMyTransform(fullHead);
                savePoint.playerTransform = CreateMyTransform(player);

                savePointList.Add(savePoint);

                PopUpInfoController.ShowPopUpInfo("Orientation saved");

                TimeDelay.IsSaveConfirmDelayEnd = true;
                StopCoroutine("WaitForSaveConfirmationDelay");
            }
            
        }

        private void AskForSaveConfirmation()
        {
            PopUpInfoController.ShowPopUpInfo("Do you want to save this orientation?");
            TimeDelay.IsSaveConfirmDelayEnd = false;
            StartCoroutine(TimeDelay.WaitForSaveConfirmationDelay(confirmSaveDelay));
        }

        private MyTransform CreateMyTransform(GameObject obj)
        {
            MyTransform myTransform = new MyTransform();
            myTransform.myPosition = obj.transform.position;
            myTransform.myRotation = obj.transform.rotation;

            return myTransform;
        }

        private void LoadLocationDownList()
        {
            if (savePointList.Count != 0)
            {
                if (TimeDelay.IsLoadSaveDelayEnd)
                {
                    StartCoroutine(TimeDelay.WaitForLoadSaveDelay(loadSaveDelay));
                    TimeDelay.IsLoadSaveDelayEnd = false;
                }

                if (listNumber < savePointList.Count)
                {
                    listNumber++;
                }
                fullHead.transform.position = (savePointList[savePointList.Count - listNumber]).skullTransform.myPosition;
                fullHead.transform.rotation = (savePointList[savePointList.Count - listNumber]).skullTransform.myRotation;
                player.transform.position = (savePointList[savePointList.Count - listNumber]).playerTransform.myPosition;
                player.transform.rotation = (savePointList[savePointList.Count - listNumber]).playerTransform.myRotation;
            }
        }

        private void LoadLocationUpList()
        {
            if (savePointList.Count != 0)
            {
                if (TimeDelay.IsLoadSaveDelayEnd)
                {
                    StartCoroutine(TimeDelay.WaitForLoadSaveDelay(loadSaveDelay));
                    TimeDelay.IsLoadSaveDelayEnd = false;
                }

                if (listNumber > 1)
                {
                    listNumber--;
                }
                fullHead.transform.position = (savePointList[savePointList.Count - listNumber]).skullTransform.myPosition;
                fullHead.transform.rotation = (savePointList[savePointList.Count - listNumber]).skullTransform.myRotation;
                player.transform.position = (savePointList[savePointList.Count - listNumber]).playerTransform.myPosition;
                player.transform.rotation = (savePointList[savePointList.Count - listNumber]).playerTransform.myRotation;

            }
        }

        private void LoadFirstSave()
        {
            if (TimeDelay.IsLoadSaveDelayEnd)
            {
                listNumber = (savePointList.Count - 1);
                LoadLocationDownList();
            }
        }

        private void LoadLastSave()
        {
            if (TimeDelay.IsLoadSaveDelayEnd)
            {
                listNumber = 1;
                LoadLocationUpList();
            }
        }
    }
}

