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

namespace PKMNEVCalc
{
    [Register ("MyViewController")]
    partial class MyViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel EmptyListLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView MyPartyPokemonTable { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (EmptyListLabel != null) {
                EmptyListLabel.Dispose ();
                EmptyListLabel = null;
            }

            if (MyPartyPokemonTable != null) {
                MyPartyPokemonTable.Dispose ();
                MyPartyPokemonTable = null;
            }
        }
    }
}