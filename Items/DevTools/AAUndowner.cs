using Terraria;
using Terraria.ID;

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
            item.rare = ItemRarityID.Green;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = ItemUseStyleID.HoldingUp;
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
            AAWorld.downedHaruka = false;
            AAWorld.downedHydra = false;
            AAWorld.downedMonarch = false;
            AAWorld.downedRajah = false;
            AAWorld.downedSag = false;
            AAWorld.downedSAncient = false;
            AAWorld.downedSerpent = false;
            AAWorld.downedShen = false;
            AAWorld.downedSisters = false;
            AAWorld.downedYamata = false;
            AAWorld.downedZero = false;
            AAWorld.downedRajahsRevenge = false;
            AAWorld.downedAthena = false;
            AAWorld.downedAnubis = false;
            AAWorld.downedGreed = false;
            AAWorld.downedAthenaA = false;
            AAWorld.downedAnubisA = false;
            AAWorld.downedGreedA = false;
            return true;
        }
    }
}
