using System;
namespace MobileAppClass
{
    public class PokemonDetail
    {
        public string breed { get; set; }
        public string nickname { get; set; }
        public string heldItem { get; set; }
        public bool pokerus { get; set; }

        public PokemonDetail()
        {
        }

        // Testing
        public override string ToString()
        {
            return string.Format("[PokemonDetail: breed={0}, nickname={1}, heldItem={2}, pokerus={3}]",
                                 breed, nickname, heldItem, pokerus);
        }

    }
}
