using System;

using UIKit;

namespace MobileAppClass
{
    public partial class MyViewController : UIViewController
    {
        public MyViewController() : base("MyViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            // Set up nav controller
            Title = "Pokemon List";
            AddNavButton();

        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            // TODO Add hiding and viewing main screen depending on if MyPartyPokemonTable is full or empty
            EmptyListLabel.Hidden = true;

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        #region UIHandlers
        void AddButton_TouchUpInside(object sender, EventArgs e)
        {
            PokemonSettingViewController vc = new PokemonSettingViewController();
            NavigationController.PushViewController(vc, true);
        }

        #endregion

        private void AddNavButton()
        {
            UIBarButtonItem addBtn = new UIBarButtonItem(UIBarButtonSystemItem.Add, AddButton_TouchUpInside);
            UIBarButtonItem[] buttonArray = { addBtn };
            NavigationItem.RightBarButtonItems = buttonArray;
        }

    }
}

