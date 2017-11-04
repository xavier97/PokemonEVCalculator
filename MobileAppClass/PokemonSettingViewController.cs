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
        string pathFile;           // The location of the Pokemon's details
        List<string> heldItems;    // A list of selectable training items

        public PokemonSettingViewController() : base("PokemonSettingViewController", null)
        {
            // Instatiate list used for picker
            heldItems = new List<string>();

            // Populate list
            heldItems.Add("-- None --");
            heldItems.Add("Macho Brace");
            heldItems.Add("Power Weight");
            heldItems.Add("Power Bracer");
            heldItems.Add("Power Belt");
            heldItems.Add("Power Lens");
            heldItems.Add("Power Band");
            heldItems.Add("Power Anklet");
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            #region set up picker wheel
            var heldItemPVM = new HeldItemPicker(heldItems);
            // Set PickerViewModel to PickerView
            HeldItemPicker.Model = heldItemPVM;
            #endregion

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

        // File Saving
        partial void SaveButton_TouchUpInside(UIButton sender)
        {
            Console.WriteLine("saved");
            PerformSave();

            /*#region file management
            // Start saving data in the file
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            pathFile = Path.Combine(path, "myPokemon.json");
            #endregion*/
        }

        private void PerformSave()
        {
            HeldItemPicker myHeldItem = new HeldItemPicker(heldItems);
            PokemonDetail pkmnDetail = new PokemonDetail();

            pkmnDetail.breed = PokemonBreedText.Text.Trim();
            pkmnDetail.nickname = PokemonNicknameText.Text.Trim();
            pkmnDetail.heldItem = myHeldItem.SelectedItem;
            pkmnDetail.pokerus = PokerusSwitch.On;

            // TODO: Test if the data is being read from UI
            Console.WriteLine( pkmnDetail.ToString() );
        }
    }
}