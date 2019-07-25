using Terraria;

namespace AAMod.Items.DevTools
{
    public class AAUndowner : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("AA Undowner");
            Tooltip.SetDefault(@"Undowns all AA bosses.
Non-Consumable");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
        }

        public override bool UseItem(Player player)
        {
            AAWorld.downedAkuma = false;
            AAWorld.downedAllAncients = false;
            AAWorld.downedAshe = false;
            AAWorld.downedBrood = false;
            AAWorld.downedDB = false;
            AAWorld.downedNC = false;
            AAWorld.downedDjinn = false;
            AAWorld.downedEquinox = false;
            AAWorld.downedFungus = false;
            AAWorld.downedGrips = false;
            AAWorld.downedGripsS = false;
            AAWorld.downedHaruka = false;
            AAWorld.downedHydra = false;
            AAWorld.downedIZ = false;
            AAWorld.downedKraken = false;
            AAWorld.downedMonarch = false;
            AAWorld.downedRajah = false;
            AAWorld.downedSag = false;
            AAWorld.downedSAncient = false;
            AAWorld.downedSerpent = false;
            AAWorld.downedShen = false;
            AAWorld.downedSisters = false;
            AAWorld.downedSoC = false;
            AAWorld.downedYamata = false;
            AAWorld.downedZero = false;
            AAWorld.downedRajahsRevenge = false;
            return true;
        }
    }
}
