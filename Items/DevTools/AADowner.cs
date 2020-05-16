using Terraria;
using Terraria.ID;

namespace AAMod.Items.DevTools
{
    public class AADowner : BaseAAItem
    {
        public override string Texture => "AAMod/Items/DevTools/AAUndowner";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("AA Downer");
            Tooltip.SetDefault(@"Downs all AA bosses.
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
            AAWorld.downedAkuma = true;
            AAWorld.downedAllAncients = true;
            AAWorld.downedAshe = true;
            AAWorld.downedBrood = true;
            AAWorld.downedDB = true;
            AAWorld.downedNC = true;
            AAWorld.downedDjinn = true;
            AAWorld.downedEquinox = true;
            AAWorld.downedFungus = true;
            AAWorld.downedGrips = true;
            AAWorld.downedHaruka = true;
            AAWorld.downedHydra = true;
            AAWorld.downedMonarch = true;
            AAWorld.downedRajah = true;
            AAWorld.downedSag = true;
            AAWorld.downedSAncient = true;
            AAWorld.downedSerpent = true;
            AAWorld.downedShen = true;
            AAWorld.downedSisters = true;
            AAWorld.downedYamata = true;
            AAWorld.downedZero = true;
            AAWorld.downedRajahsRevenge = true;
            AAWorld.downedAthena = true;
            AAWorld.downedAnubis = true;
            AAWorld.downedGreed = true;
            return true;
        }
    }
}
