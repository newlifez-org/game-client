using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace NewLifeZ.MainMenu
{
    public class MM_FormTabInputField : MonoBehaviour
    {
        public TMP_InputField[] inputFields;
        public Button SubmitButton;
        private int InputSelected;

        private void Awake()
        {
            if (inputFields.Length == 0 && inputFields == null) return;
        }

        private void Start()
        {
            inputFields[0].Select();
            inputFields[0].ActivateInputField();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKeyDown(KeyCode.LeftShift))
            {
                InputSelected--;
                if (InputSelected < 0) InputSelected = inputFields.Length;
                SelectInputField();
            }
            else if (Input.GetKeyDown(KeyCode.Tab))
            {
                InputSelected++;
                if (InputSelected >= inputFields.Length) InputSelected = 0;
                SelectInputField();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {

                SubmitButton.Select();
                SubmitButton.onClick.Invoke();
            }

            UpdateButtonInteraction();
        }

        private void SelectInputField()
        {
            inputFields[InputSelected].Select();
        }

        private void UpdateButtonInteraction()
        {
            bool anyFieldNull = inputFields.Any(field => field.text.Equals(""));
            if (anyFieldNull)
            {
                SubmitButton.interactable = false;
            }
            else
            {
                SubmitButton.interactable = true;
            }
        }
    }
}

