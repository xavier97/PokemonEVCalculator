using System;
using System.Collections.Generic;
using UIKit;
using System.Runtime.CompilerServices;

namespace MobileAppClass
{
    public partial class PokemonSettingViewController : UIViewController
    {
        public PokemonSettingViewController() : base("PokemonSettingViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            #region set up picker wheel
            List<String> heldItems = new List<String>();

            // Populate list
            heldItems.Add("-- None --");
            heldItems.Add("Macho Brace");
            heldItems.Add("Power Weight");
            heldItems.Add("Power Bracer");
            heldItems.Add("Power Belt");
            heldItems.Add("Power Lens");
            heldItems.Add("Power Band");
            heldItems.Add("Power Anklet");

            // Set PickerViewModel to PickerView
            var examplePVM = new HeldItemPicker(heldItems);
            HeldItemPicker.Model = examplePVM;
            #endregion

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
    }
}

