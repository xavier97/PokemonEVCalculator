using System;
using Foundation;
using UIKit;
using System.Xml.Linq;
using System.IO;
using System.Linq;

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

            // Set up Table View
            MyFoePokemonTable.Source = new TableViewSource(this);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            MyFoePokemonTable.Hidden = false;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            MyFoePokemonTable.ReloadData();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

    }


    #region Table View Stuff, including swipe to delete
    public class TableViewSource : UITableViewSource
    {
        readonly NewOpponentViewController vc;
        readonly XDocument xDoc;

        public TableViewSource(NewOpponentViewController _vc)
        {
            vc = _vc;

            // Load XML doc
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var library = Path.Combine(documents, "..", "Library");
            var fileName = Path.Combine(library, "AllPokemon.xml");
            xDoc = XDocument.Load(fileName);
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell;
            // try to get a reusable cell
            cell = tableView.DequeueReusableCell("pokemonstyle");
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, "pokemonstyle");
            }

            cell.TextLabel.Text = xDoc.Descendants("Name").ElementAt(indexPath.Row).Value;
            cell.DetailTextLabel.Text = xDoc.Descendants("Name").Attributes("DexNum").ElementAt(indexPath.Row).Value;


            return cell;
        }

        public override void RowDeselected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.CellAt(indexPath).Accessory = UITableViewCellAccessory.None;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.CellAt(indexPath).Accessory = UITableViewCellAccessory.Checkmark;

            //if (indexPath.Section == 0 && FileManager.getInstance.pokemonDetailsStorage[indexPath.Row] != null)
            //{
            //    pkmnToPass = FileManager.getInstance.pokemonDetailsStorage[indexPath.Row];
            //}
            //else
            //{
            //    throw new Exception("Bad section in rowSelected");
            //    // dataToPass = "invalid data";
            //}

            //PokemonSettingViewController pkmnSettingVC = new PokemonSettingViewController(indexPath.Row);

            // Push the edit display 
            //vc.NavigationController.PushViewController(pkmnSettingVC, true);
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            if (section == 0)
            {
                return xDoc.Descendants("Pokemon").Count();
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
                case UITableViewCellEditingStyle.None:

                    Console.WriteLine("CommitEditingStyle:None called");

                    break;
            }
        }

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return false;
        }
    }
    #endregion
}

