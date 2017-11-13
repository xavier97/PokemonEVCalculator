using System;
using System.Collections.Generic;
using Foundation;
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

            MyPartyPokemonTable.Source = new TableViewSource(this);
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
            PokemonSettingViewController vc = new PokemonSettingViewController(null);
            NavigationController.PushViewController(vc, true);
        }
        #endregion

        #region UICustomization
        private void AddNavButton()
        {
            UIBarButtonItem addBtn = new UIBarButtonItem(UIBarButtonSystemItem.Add, AddButton_TouchUpInside);
            UIBarButtonItem[] buttonArray = { addBtn };
            NavigationItem.RightBarButtonItems = buttonArray;
        }
        #endregion

    }

    #region Table View Stuff, including swipe to delete
    public class TableViewSource : UITableViewSource
    {
        MyViewController vc;

        public TableViewSource(MyViewController _vc)
        {
            vc = _vc;

            Console.WriteLine("you're in tablesource constructor");
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            Console.WriteLine("you're in getcell");

            // TODO: Test cell section/row
            Console.WriteLine("Indexpath Section{0}  Row{1}",
            indexPath.Section,
            indexPath.Row);

            UITableViewCell cell;
            // try to get a reusable cell
            cell = tableView.DequeueReusableCell("pokemonstyle");
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, "pokemonstyle");
            }
            cell.TextLabel.Text = FileManager.getInstance.pokemonDetailsStorage[indexPath.Row].nickname;
            cell.DetailTextLabel.Text = FileManager.getInstance.pokemonDetailsStorage[indexPath.Row].breed;
            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            // create a pokemon details vc
            PokemonDetail pkmnToPass = new PokemonDetail();

            if (indexPath.Section == 0 && FileManager.getInstance.pokemonDetailsStorage[indexPath.Row] != null)
            {
                pkmnToPass = FileManager.getInstance.pokemonDetailsStorage[indexPath.Row];
            }
            else
            {
                throw new Exception("Bad section in rowSelected");
                // dataToPass = "invalid data";
            }

            PokemonSettingViewController pkmnSettingVC = new PokemonSettingViewController(indexPath.Row);

            // Push the edit display 
            vc.NavigationController.PushViewController(pkmnSettingVC, true);
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            if (section == 0)
            {
                return FileManager.getInstance.pokemonDetailsStorage.Count;
            }
            else
            {
                throw new Exception("Bad section requested");
            }
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, Foundation.NSIndexPath indexPath)
        {
            switch (editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:

                    // TODO: delete the Chuck from the JSON storage

                    // remove the item from the underlying data source
                    FileManager.getInstance.pokemonDetailsStorage.RemoveAt(indexPath.Row);

                    // delete the row from the table
                    tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);

                    break;
                case UITableViewCellEditingStyle.None:

                    Console.WriteLine("CommitEditingStyle:None called");

                    break;
            }
        }

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true; // return false if you wish to disable editing for a specific indexPath or for all rows
        }
    }
    #endregion
}