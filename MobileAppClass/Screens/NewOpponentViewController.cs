using System;
using Foundation;
using UIKit;
using System.Xml.Linq;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace MobileAppClass.Screens
{
    public partial class NewOpponentViewController : UIViewController
    {
        public NewOpponentViewController() : base("NewOpponentViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            // Set up Nav Controller
            Title = "Foe";

            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var library = Path.Combine(documents, "..", "Library");
            var fileName = Path.Combine(library, "AllPokemon.xml");

            XDocument xDoc = XDocument.Load(fileName);
            Console.WriteLine(xDoc);

            XElement pokemon = XElement.Load(fileName);

            var queryAll = from mon in pokemon.Descendants("Pokemon")
                           select (string)mon;
                           
            Console.WriteLine(queryAll);

            //MyFoePokemonTable.Source = new TableViewSource(this);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }

    /*
    #region Table View Stuff, including swipe to delete
    public class TableViewSource : UITableViewSource
    {
        NewOpponentViewController vc;

        public TableViewSource(NewOpponentViewController _vc)
        {
            vc = _vc;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
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
            PokemonBattled pkmnToPass = new PokemonBattled();

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

                    // remove the item from the underlying data source
                    FileManager.getInstance.pokemonDetailsStorage.RemoveAt(indexPath.Row);

                    // delete the data from JSON
                    FileManager.getInstance.DeletePokemon(indexPath.Row);

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
    */
}

