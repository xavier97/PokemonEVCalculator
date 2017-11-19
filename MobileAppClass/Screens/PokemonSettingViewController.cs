﻿using System;
using System.Collections.Generic;
using UIKit;
using System.Runtime.CompilerServices;
using System.IO;
//using MobileAppClass.Resources;

namespace PKMNEVCalc
{
    public partial class PokemonSettingViewController : UIViewController
    {
        int? rowToEdit;
        string heldItem; // Pokemon's held item selected from picker view

        public PokemonSettingViewController(int? rowToEditIn) : base("PokemonSettingViewController", null)
        {
            rowToEdit = rowToEditIn;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            #region set up picker view
            // create our simple picker model
            var pickerDataModel = new PickerDataModel();
            pickerDataModel.Items.Add("-- None --");
            pickerDataModel.Items.Add("Macho Brace");
            pickerDataModel.Items.Add("Power Weight");
            pickerDataModel.Items.Add("Power Bracer");
            pickerDataModel.Items.Add("Power Belt");
            pickerDataModel.Items.Add("Power Lens");
            pickerDataModel.Items.Add("Power Band");
            pickerDataModel.Items.Add("Power Anklet");

            // Set model to the to the class 
            HeldItemPicker.Model = pickerDataModel;

            // wire up the value change method
            pickerDataModel.ValueChanged += (s, e) => {
                heldItem = pickerDataModel.SelectedItem;
            };
            #endregion

            // Adds save button to nav bar
            SaveNavButton();

            // Edit mode - pre-fill data
            if (rowToEdit != null)
            {
                var pkmnToEdit = FileManager.getInstance.pokemonDetailsStorage[(int)rowToEdit];

                PokemonBreedText.Text = pkmnToEdit.breed;
                PokemonNicknameText.Text = pkmnToEdit.nickname;
                if (pkmnToEdit.pokerus == true)
                {
                    PokerusSwitch.SetState(true, true);
                }

                // Pre-fill the held item picker
                HeldItemPicker.Select(pickerDataModel.Items.IndexOf(pkmnToEdit.heldItem), 0, true);
            }

        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void UIButton8246_TouchUpInside(UIButton sender)
        {
            Console.WriteLine("button touched");
        }

        partial void UIButton10873_TouchUpInside(UIButton sender)
        {
            Console.WriteLine("other button touched");
        }

        // Creates save button in nav bar
        private void SaveNavButton()
        {
            UIBarButtonItem saveBtn = new UIBarButtonItem(UIBarButtonSystemItem.Save, SaveButton_TouchUpInside);
            UIBarButtonItem[] buttonArray = { saveBtn }; // An array of Navigation Buttons
            NavigationItem.RightBarButtonItems = buttonArray;
        }

        // File saving button calls
        void SaveButton_TouchUpInside(object sender, EventArgs e)
        {
            // Save details to JSON
            // Pop back to main screen
            PerformSave();
            Console.WriteLine("Saved");
        }

        // Saves both edited and new Pokemon
        private void PerformSave()
        {
            PokemonDetail pokemonToAddOrEdit;
            PokemonDetail pkmnDetail = new PokemonDetail();

            if (rowToEdit == null) // Adding new record
            {
                pokemonToAddOrEdit = new PokemonDetail();
            }
            else // Editing
            {
                // Manipulate the existng Pokemon in list
                pokemonToAddOrEdit = FileManager.getInstance.pokemonDetailsStorage[(int)rowToEdit];
            }

            // Get data from fields
            pokemonToAddOrEdit.breed = PokemonBreedText.Text.Trim();
            pokemonToAddOrEdit.nickname = PokemonNicknameText.Text.Trim();
            pokemonToAddOrEdit.pokerus = PokerusSwitch.On;
            pokemonToAddOrEdit.heldItem = heldItem;

            if (rowToEdit == null)
            {
                // adding
                FileManager.getInstance.SavePokemon(pokemonToAddOrEdit);
            }
            else
            {
                Console.WriteLine(pokemonToAddOrEdit.heldItem);

                // editing
                FileManager.getInstance.SavePokemon(null);
            }
        }
    }
}