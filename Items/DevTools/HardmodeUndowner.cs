using Terraria;
using Terraria.ID;

namespace AAMod.Items.DevTools
{
    public class HardmodeUndowner : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hardmode Undowner");
            Tooltip.SetDefault(@"Undowns all post-WOF bosses.
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
            NPC.downedAncientCultist = false;
            NPC.downedChristmasIceQueen = false;
            NPC.downedChristmasSantank = false;
            NPC.downedChristmasTree = false;
            NPC.downedClown = false;
            NPC.downedFishron = false;
            NPC.downedFrost = false;
            NPC.downedGolemBoss = false;
            NPC.downedHalloweenKing = false;
            NPC.downedHalloweenTree = false;
            NPC.downedMartians = false;
            NPC.downedMechBoss1 = false;
            NPC.downedMechBoss2 = false;
            NPC.downedMechBoss3 = false;
            NPC.downedMechBossAny = false;
            NPC.downedMoonlord = false;
            NPC.downedPirates = false;
            NPC.downedPlantBoss = false;
            NPC.downedTowerNebula = false;
            NPC.downedTowerSolar = false;
            NPC.downedTowerStardust = false;
            NPC.downedTowerVortex = false;
            AAWorld.downedAkuma = false;
            AAWorld.downedAllAncients = false;
            AAWorld.downedAshe = false;
            AAWorld.downedDB = false;
            AAWorld.downedNC = false;
            AAWorld.downedEquinox = false;
            AAWorld.downedHaruka = false;
            AAWorld.downedRajah = false;
            AAWorld.downedSAncient = false;
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
