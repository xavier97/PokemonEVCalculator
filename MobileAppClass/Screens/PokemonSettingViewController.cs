using System;
using UIKit;
using System.IO;
//using MobileAppClass.Resources;
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
            // create our simple picker model
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
            pickerDataModel.ValueChanged += (s, e) => {
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

                // Pre-fill the Pokemon Battle buttons
                foreach (KeyValuePair<string, PokemonBattled> entry in pkmnToEdit.GetAllButtons())
                {
                    int count = 1;
                    if (pkmnToEdit.GetAButton(count) != null)
                    {
                        pokemonBattleButtonDict["pokemonButton" + count].SetTitle(pkmnToEdit.GetAButton(count).Name,
                            UIControlState.Normal);
                    }
                    count++;
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

            pkmnToEdit.SetPokemonBattled(buttonNumber, null);
            InvokeOnMainThread(() => { pokemonBattleButtonDict["pokemonButton" + buttonNumber]
                .SetTitle("No PKMN", UIControlState.Normal); });
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
                PokemonDetail pkmnToEdit = FileManager.getInstance.pokemonDetailsStorage[(int)rowToEdit];

                Title = PokemonNicknameText.Text;

                // wire up title to nickname
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
                pokemonButton1.TouchUpInside += (sender, ea) =>
                {
                    pkmnToEdit.attackEV += pkmnToEdit.GetAButton(1).AttackEV;
                    pkmnToEdit.defenseEV += pkmnToEdit.GetAButton(1).DefenseEV;
                    pkmnToEdit.spAttackEV += pkmnToEdit.GetAButton(1).SpAttackEV;
                    pkmnToEdit.spDefenseEV += pkmnToEdit.GetAButton(1).SpDefenseEV;
                    pkmnToEdit.speedEV += pkmnToEdit.GetAButton(1).SpeedEV;
                    pkmnToEdit.hpEV += pkmnToEdit.GetAButton(1).HpEV;
                };

                pokemonButton2.TouchUpInside += (sender, ea) =>
                {
                    pkmnToEdit.attackEV += pkmnToEdit.GetAButton(2).AttackEV;
                    pkmnToEdit.defenseEV += pkmnToEdit.GetAButton(2).DefenseEV;
                    pkmnToEdit.spAttackEV += pkmnToEdit.GetAButton(2).SpAttackEV;
                    pkmnToEdit.spDefenseEV += pkmnToEdit.GetAButton(2).SpDefenseEV;
                    pkmnToEdit.speedEV += pkmnToEdit.GetAButton(2).SpeedEV;
                    pkmnToEdit.hpEV += pkmnToEdit.GetAButton(2).HpEV;
                };

                pokemonButton3.TouchUpInside += (sender, ea) =>
                {
                    pkmnToEdit.attackEV += pkmnToEdit.GetAButton(3).AttackEV;
                    pkmnToEdit.defenseEV += pkmnToEdit.GetAButton(3).DefenseEV;
                    pkmnToEdit.spAttackEV += pkmnToEdit.GetAButton(3).SpAttackEV;
                    pkmnToEdit.spDefenseEV += pkmnToEdit.GetAButton(3).SpDefenseEV;
                    pkmnToEdit.speedEV += pkmnToEdit.GetAButton(3).SpeedEV;
                    pkmnToEdit.hpEV += pkmnToEdit.GetAButton(3).HpEV;
                };

                // Update when coming back from other screen
                // Pre-fill the Pokemon Battle buttons
                for (int count = 1; count <= pkmnToEdit.GetAllButtons().Count; count++)
                {
                    Console.WriteLine(count);
                    if (pkmnToEdit.GetAButton(count) != null)
                    {
                        pokemonBattleButtonDict["pokemonButton" + count].SetTitle(pkmnToEdit.GetAButton(count).Name,
                            UIControlState.Normal);
                    }
                }
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

            Console.WriteLine(pokemonToAddOrEdit);

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