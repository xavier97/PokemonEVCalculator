using System;
using System.Collections.Generic;
using UIKit;
using System.Runtime.CompilerServices;
using System.IO;
//using MobileAppClass.Resources;

namespace MobileAppClass
{
    public partial class PokemonSettingViewController : UIViewController
    {
        int? rowToEdit;
        List<string> heldItems;    // A list of selectable training items

        public PokemonSettingViewController(int? rowToEditIn) : base("PokemonSettingViewController", null)
        {
            rowToEdit = rowToEditIn;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            #region set up picker wheel
            heldItems = new List<string>();
            heldItems.Add("-- None --");
            heldItems.Add("Macho Brace");
            heldItems.Add("Power Weight");
            heldItems.Add("Power Bracer");
            heldItems.Add("Power Belt");
            heldItems.Add("Power Lens");
            heldItems.Add("Power Band");
            heldItems.Add("Power Anklet");

            var heldItemPVM = new HeldItemPicker(heldItems);

            // Set PickerViewModel to PickerView
            HeldItemPicker.Model = heldItemPVM;
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
                // TODO : Pre-fill the held item picker 
                //HeldItemPicker myHeldItem = new HeldItemPicker(heldItems);
                //myHeldItem.Selected(HeldItemPicker, heldItems.IndexOf(pkmnToEdit.heldItem), 1);
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
            HeldItemPicker myHeldItem = new HeldItemPicker(heldItems);
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
            pokemonToAddOrEdit.heldItem = myHeldItem.SelectedItem;
            pokemonToAddOrEdit.pokerus = PokerusSwitch.On;

            if (rowToEdit == null)
            {
                // adding
                FileManager.getInstance.SavePokemon(pokemonToAddOrEdit);
            }
            else
            {
                // editing
                FileManager.getInstance.SavePokemon(null);
            }
        }
    }
}