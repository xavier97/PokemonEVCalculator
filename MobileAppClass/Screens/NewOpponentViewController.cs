using System;
using Foundation;
using UIKit;
using System.Xml.Linq;
using System.IO;
using System.Linq;
using PKMNEVCalc;
using System.Collections.Generic;

namespace MobileAppClass.Screens
{
    public partial class NewOpponentViewController : UIViewController
    {
        private  int? rowToEdit;
        private readonly int buttonNumber;

        public NewOpponentViewController(int? rowToEdit, int buttonNumber) : base("NewOpponentViewController", null)
        {
            this.rowToEdit = rowToEdit;
            this.buttonNumber = buttonNumber;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            // Set up Nav Controller
            Title = "Foe";

            // Set up Table View
            MyFoePokemonTable.Source = new TableViewSource(this, rowToEdit, buttonNumber);
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
        readonly PokemonDetail pkmnToEdit = new PokemonDetail();
        readonly int buttonNumber;

        public TableViewSource(NewOpponentViewController _vc, int? rowToEdit, int buttonNumber)
        {
            vc = _vc;

            // Load XML doc
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var library = Path.Combine(documents, "..", "Library");
            var fileName = Path.Combine(library, "AllPokemon.xml");
            xDoc = XDocument.Load(fileName);

            // Find selected Pokemon
            if (rowToEdit != null)
            {
                pkmnToEdit = FileManager.getInstance.pokemonDetailsStorage[(int)rowToEdit];
            }

            // Determine the button being accessed
            this.buttonNumber = buttonNumber;
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

            // Load the checkmark on the previously selected Pokemon (plural)
            for (int count = 1; count <= pkmnToEdit.GetAllButtons().Count; count++)
            {
                if (pkmnToEdit.GetAButton(count) != null)
                {
                    if (pkmnToEdit.GetAButton(count).Name == cell.TextLabel.Text)
                    {
                        cell.Accessory = UITableViewCellAccessory.Checkmark;
                        cell.SetHighlighted(false, false);
                    }
                    else
                    {
                        cell.Accessory = UITableViewCellAccessory.None;
                    }
                }
            }

            return cell;
        }

        public override void RowDeselected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.CellAt(indexPath).Accessory = UITableViewCellAccessory.None;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            // Clear all decorations from cells
            foreach (UITableViewCell cell in tableView.VisibleCells)
            {
                cell.Accessory = UITableViewCellAccessory.None;
            }

            if (indexPath.Section == 0 && pkmnToEdit != null)
            {
                // Indicate this Pokemon is chosen
                tableView.CellAt(indexPath).Accessory = UITableViewCellAccessory.Checkmark;

                // Select the pokemon from XML
                string name = xDoc.Descendants("Name").ElementAt(indexPath.Row).Value;
                int attack = int.Parse(xDoc.Descendants("attackEV").ElementAt(indexPath.Row).Value);
                Console.WriteLine("Attack:" + attack);
                int defense = int.Parse(xDoc.Descendants("defenseEV").ElementAt(indexPath.Row).Value);
                int spAttack = int.Parse(xDoc.Descendants("spAttackEV").ElementAt(indexPath.Row).Value);
                int spDefense = int.Parse(xDoc.Descendants("spDefenseEV").ElementAt(indexPath.Row).Value);
                int hp = int.Parse(xDoc.Descendants("hpEV").ElementAt(indexPath.Row).Value);
                int speed = int.Parse(xDoc.Descendants("speedEV").ElementAt(indexPath.Row).Value);

                // Create new PokemonBattled object with specifications from XML
                var pkmnToPass = new PokemonBattled(name, attack, defense, spAttack, spDefense, hp, speed);

                // Persist pokemon battling as a part of the desired Pokemon's data
                pkmnToEdit.SetPokemonBattled(buttonNumber, pkmnToPass);

                FileManager.getInstance.SavePokemon(null); // (editing)
            }
            else
            {
                throw new Exception("Bad section in rowSelected");
            }
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

