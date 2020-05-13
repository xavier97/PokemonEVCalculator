using System;
using UIKit;
using MobileAppClass.Screens;
using System.Collections.Generic;
using MobileAppClass;

namespace PKMNEVCalc
{
    public partial class PokemonSettingViewController : UIViewController
    {
        int? rowToEdit;
        string heldItem; // Pokemon's held item selected from picker view
        private readonly Dictionary<string, UIButton> pokemonBattleButtonDict = new Dictionary<string, UIButton>();

        public PokemonSettingViewController(int? rowToEditIn) : base("PokemonSettingViewController", null)
        {
            rowToEdit = rowToEditIn;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            // Adds save button to nav bar
            SaveNavButton();

            #region Add long press button gestures
            // Long press
            UILongPressGestureRecognizer longp1 = new UILongPressGestureRecognizer(() => LongPress(1));
            UILongPressGestureRecognizer longp2 = new UILongPressGestureRecognizer(() => LongPress(2));
            UILongPressGestureRecognizer longp3 = new UILongPressGestureRecognizer(() => LongPress(3));

            // Add gestures
            pokemonButton1.AddGestureRecognizer(longp1);
            pokemonButton2.AddGestureRecognizer(longp2);
            pokemonButton3.AddGestureRecognizer(longp3);
            #endregion

            #region set up picker view
            // create a simple picker model
            var pickerDataModel = new PickerDataModel();
            pickerDataModel.Items.Add("-- None --");
            pickerDataModel.Items.Add("Power Weight");
            pickerDataModel.Items.Add("Power Bracer");
            pickerDataModel.Items.Add("Power Belt");
            pickerDataModel.Items.Add("Power Lens");
            pickerDataModel.Items.Add("Power Band");
            pickerDataModel.Items.Add("Power Anklet");

            // Set model to the to the class 
            HeldItemPicker.Model = pickerDataModel;

            // wire up the value change method
            pickerDataModel.ValueChanged += (s, e) =>
            {
                heldItem = pickerDataModel.SelectedItem;
            };
            #endregion

            #region set up button variables
            pokemonBattleButtonDict["pokemonButton1"] = pokemonButton1;
            pokemonBattleButtonDict["pokemonButton2"] = pokemonButton2;
            pokemonBattleButtonDict["pokemonButton3"] = pokemonButton3;
            #endregion

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
                heldItem = pkmnToEdit.heldItem;

                // Pre-fill the stats
                AttackEVLabel.Text = pkmnToEdit.attackEV.ToString();
                DefenseEVLabel.Text = pkmnToEdit.defenseEV.ToString();
                SpAtkEVLabel.Text = pkmnToEdit.spAttackEV.ToString();
                SpDefEVLabel.Text = pkmnToEdit.spDefenseEV.ToString();
                SpeedEVLabel.Text = pkmnToEdit.speedEV.ToString();
                HPEVLabel.Text = pkmnToEdit.hpEV.ToString();

                // Pre-fill the images/enable buttons
                if (pkmnToEdit.GetAButton(1).Name != string.Empty)
                {
                    var toBattle1 = pkmnToEdit.GetAButton(1).Name;
                    pokemonButton1.SetImage(UIImage.FromBundle(toBattle1), UIControlState.Normal);
                }
                if (pkmnToEdit.GetAButton(2).Name != string.Empty)
                {
                    var toBattle2 = pkmnToEdit.GetAButton(2).Name;
                    pokemonButton2.SetImage(UIImage.FromBundle(toBattle2), UIControlState.Normal);
                }
                if (pkmnToEdit.GetAButton(3).Name != string.Empty)
                {
                    var toBattle3 = pkmnToEdit.GetAButton(3).Name;
                    pokemonButton3.SetImage(UIImage.FromBundle(toBattle3), UIControlState.Normal);
                }
            }
        }

        /// <summary>
        /// Allows the option to (1) KO the Pokemon (if pokemonToBattle is not null) or 
        /// (2) select a new Pokemon to battle against
        /// </summary>
        private void LongPress(int buttonNumber)
        {
            var option = UIAlertController.Create(null, "Switch up on your opponent.", UIAlertControllerStyle.ActionSheet);
            option.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));
            option.AddAction(UIAlertAction.Create("New Opponent",
                                                   UIAlertActionStyle.Default,
                                                   (obj) => { GetNewOpponentScreen(obj, buttonNumber); }));
            option.AddAction(UIAlertAction.Create("Delete Opponent",
                                                   UIAlertActionStyle.Default,
                                                   (obj) => { DeleteOpponent(obj, buttonNumber); })); // TODO: ADD THE DELETE POKEMON OPTION
            PresentViewController(option, animated: true, completionHandler: null);
        }

        private void DeleteOpponent(UIAlertAction obj, int buttonNumber)
        {
            var pkmnToEdit = FileManager.getInstance.pokemonDetailsStorage[(int)rowToEdit];

            pkmnToEdit.GetAButton(buttonNumber).Clear();
            InvokeOnMainThread(() =>
            {
                pokemonBattleButtonDict["pokemonButton" + buttonNumber]
.SetTitle("No PKMN", UIControlState.Normal);
            });
        }

        private void GetNewOpponentScreen(UIAlertAction obj, int buttonNumber)
        {
            NewOpponentViewController vc = new NewOpponentViewController(rowToEdit, buttonNumber);
            NavigationController.PushViewController(vc, true);
            Console.WriteLine(buttonNumber);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (rowToEdit != null)
            {
                //PokemonDetail pkmnToEdit = FileManager.getInstance.pokemonDetailsStorage[(int)rowToEdit];

                // wire up title to nickname
                Title = PokemonNicknameText.Text;
                PokemonNicknameText.AllEditingEvents += (sender, e) =>
                {
                    Title = PokemonNicknameText.Text;
                };
            }
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (rowToEdit != null)
            {
                var pkmnToEdit = FileManager.getInstance.pokemonDetailsStorage[(int)rowToEdit];

                // Wire up buttons to touch event
                if (pkmnToEdit.GetAButton(1) != null)
                {
                    pokemonButton1.TouchUpInside += (sender, ea) =>
                    {
                        CalculateStats(pkmnToEdit.GetAButton(1));
                    };
                }

                if (pkmnToEdit.GetAButton(2) != null)
                {
                    pokemonButton2.TouchUpInside += (sender, ea) =>
                    {
                        CalculateStats(pkmnToEdit.GetAButton(2));
                    };
                }

                if (pkmnToEdit.GetAButton(3) != null)
                {
                    pokemonButton3.TouchUpInside += (sender, ea) =>
                    {
                        CalculateStats(pkmnToEdit.GetAButton(3));
                    };
                }

                // Update when coming back from other screen
                // Pre-fill the Pokemon Battle buttons
                for (int count = 1; count <= pkmnToEdit.GetAllButtons().Count; count++)
                {
                    Console.WriteLine(count);
                    if (pkmnToEdit.GetAButton(count).Name != string.Empty)
                    {
                        // title
                        pokemonBattleButtonDict["pokemonButton" + count].SetTitle(pkmnToEdit.GetAButton(count).Name,
                            UIControlState.Normal);
                        // image
                        pokemonBattleButtonDict["pokemonButton" + count].SetImage(
                            UIImage.FromBundle(pkmnToEdit.GetAButton(count).Name),
                            UIControlState.Normal);
                    }
                }

                // name labels
                evPokemonLabel1.Text = pkmnToEdit.GetAButton(1).Name;
                evPokemonLabel2.Text = pkmnToEdit.GetAButton(2).Name;
                evPokemonLabel3.Text = pkmnToEdit.GetAButton(3).Name;

                // ev yield
                evYieldLabel1.Text = CreateEVLabel(pkmnToEdit.GetAButton(1));
                evYieldLabel2.Text = CreateEVLabel(pkmnToEdit.GetAButton(2));
                evYieldLabel3.Text = CreateEVLabel(pkmnToEdit.GetAButton(3));
            }
        }

        private string CreateEVLabel(PokemonBattled pokemon)
        {
            List<string> labelList = new List<string>();
            if (pokemon.AttackEV > 0)
            {
                labelList.Add("+" + pokemon.AttackEV + " Attack");
            }
            if (pokemon.DefenseEV > 0)
            {
                labelList.Add("+" + pokemon.DefenseEV + " Defense");
            }
            if (pokemon.SpAttackEV > 0)
            {
                labelList.Add("+" + pokemon.SpAttackEV + " Sp. Attack");
            }
            if (pokemon.SpDefenseEV > 0)
            {
                labelList.Add("+" + pokemon.SpDefenseEV + " Sp. Defense");
            }
            if (pokemon.SpeedEV > 0)
            {
                labelList.Add("+" + pokemon.SpeedEV + " Speed");
            }
            if (pokemon.HpEV > 0)
            {
                labelList.Add("+" + pokemon.HpEV + " HP");
            }

            string label = string.Join("\n", labelList.ToArray());

            return label;
        }

        private void CalculateStats(PokemonBattled pokemonBattled)
        {
            if (pokemonBattled.Name != string.Empty)
            {
                // pokemon battled
                int atkEV = pokemonBattled.AttackEV;
                int defEV = pokemonBattled.DefenseEV;
                int spAtkEV = pokemonBattled.SpAttackEV;
                int spDefEV = pokemonBattled.SpDefenseEV;
                int hpEV = pokemonBattled.HpEV;
                int speedEV = pokemonBattled.SpeedEV;

                // pokerus
                var mult = 1;
                if (PokerusSwitch.On)
                    mult = 2;

                // held item
                const int powerYield = 8;
                switch (heldItem)
                {
                    case "Power Weight":
                        hpEV += powerYield;
                        break;
                    case "Power Bracer":
                        atkEV += powerYield;
                        break;
                    case "Power Belt":
                        defEV += powerYield;
                        break;
                    case "Power Lens":
                        spAtkEV += powerYield;
                        break;
                    case "Power Band":
                        spDefEV += powerYield;
                        break;
                    case "Power Anklet":
                        speedEV += powerYield;
                        break;
                }

                // Calculate totals and display
                HPEVLabel.Text = (Int32.Parse(HPEVLabel.Text) + (hpEV * mult)).ToString();
                AttackEVLabel.Text = (Int32.Parse(AttackEVLabel.Text) + (atkEV * mult)).ToString();
                DefenseEVLabel.Text = (Int32.Parse(DefenseEVLabel.Text) + (defEV * mult)).ToString();
                SpAtkEVLabel.Text = (Int32.Parse(SpAtkEVLabel.Text) + (spAtkEV * mult)).ToString();
                SpDefEVLabel.Text = (Int32.Parse(SpDefEVLabel.Text) + (spDefEV * mult)).ToString();
                SpeedEVLabel.Text = (Int32.Parse(SpeedEVLabel.Text) + (speedEV * mult)).ToString();
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
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

            pokemonToAddOrEdit.attackEV = Int32.Parse(AttackEVLabel.Text);
            pokemonToAddOrEdit.defenseEV = Int32.Parse(DefenseEVLabel.Text);
            pokemonToAddOrEdit.spAttackEV = Int32.Parse(SpAtkEVLabel.Text);
            pokemonToAddOrEdit.spDefenseEV = Int32.Parse(SpDefEVLabel.Text);
            pokemonToAddOrEdit.speedEV = Int32.Parse(SpeedEVLabel.Text);
            pokemonToAddOrEdit.hpEV = Int32.Parse(HPEVLabel.Text);

            Console.WriteLine(pokemonToAddOrEdit);

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