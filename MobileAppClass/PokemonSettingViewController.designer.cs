// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MobileAppClass
{
    [Register ("PokemonSettingViewController")]
    partial class PokemonSettingViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel AttackEVLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel AttackEVTitleLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel DefenseEVLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel DefenseEVTItleLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPickerView HeldItemPicker { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel HeldItemTitleLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel HPEVLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel HPEVTitleLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField PokemonBreedText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField PokemonNicknameText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch PokerusSwitch { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel PokerusTitleLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel SpAtkEVLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel SpAtkEVTitleLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel SpDefEVLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel SpDefEVTitleLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel SpeedEVLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel SpeedEVTitleLabel { get; set; }

        [Action ("UIButton10873_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton10873_TouchUpInside (UIKit.UIButton sender);

        [Action ("UIButton8246_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton8246_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (AttackEVLabel != null) {
                AttackEVLabel.Dispose ();
                AttackEVLabel = null;
            }

            if (AttackEVTitleLabel != null) {
                AttackEVTitleLabel.Dispose ();
                AttackEVTitleLabel = null;
            }

            if (DefenseEVLabel != null) {
                DefenseEVLabel.Dispose ();
                DefenseEVLabel = null;
            }

            if (DefenseEVTItleLabel != null) {
                DefenseEVTItleLabel.Dispose ();
                DefenseEVTItleLabel = null;
            }

            if (HeldItemPicker != null) {
                HeldItemPicker.Dispose ();
                HeldItemPicker = null;
            }

            if (HeldItemTitleLabel != null) {
                HeldItemTitleLabel.Dispose ();
                HeldItemTitleLabel = null;
            }

            if (HPEVLabel != null) {
                HPEVLabel.Dispose ();
                HPEVLabel = null;
            }

            if (HPEVTitleLabel != null) {
                HPEVTitleLabel.Dispose ();
                HPEVTitleLabel = null;
            }

            if (PokemonBreedText != null) {
                PokemonBreedText.Dispose ();
                PokemonBreedText = null;
            }

            if (PokemonNicknameText != null) {
                PokemonNicknameText.Dispose ();
                PokemonNicknameText = null;
            }

            if (PokerusSwitch != null) {
                PokerusSwitch.Dispose ();
                PokerusSwitch = null;
            }

            if (PokerusTitleLabel != null) {
                PokerusTitleLabel.Dispose ();
                PokerusTitleLabel = null;
            }

            if (SpAtkEVLabel != null) {
                SpAtkEVLabel.Dispose ();
                SpAtkEVLabel = null;
            }

            if (SpAtkEVTitleLabel != null) {
                SpAtkEVTitleLabel.Dispose ();
                SpAtkEVTitleLabel = null;
            }

            if (SpDefEVLabel != null) {
                SpDefEVLabel.Dispose ();
                SpDefEVLabel = null;
            }

            if (SpDefEVTitleLabel != null) {
                SpDefEVTitleLabel.Dispose ();
                SpDefEVTitleLabel = null;
            }

            if (SpeedEVLabel != null) {
                SpeedEVLabel.Dispose ();
                SpeedEVLabel = null;
            }

            if (SpeedEVTitleLabel != null) {
                SpeedEVTitleLabel.Dispose ();
                SpeedEVTitleLabel = null;
            }
        }
    }
}