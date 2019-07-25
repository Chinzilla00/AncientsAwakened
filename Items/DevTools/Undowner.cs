using Terraria;

namespace AAMod.Items.DevTools
{
    public class Undowner : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Undowner");
            Tooltip.SetDefault(@"Undowns all bosses.
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
            NPC.downedAncientCultist = false;
            NPC.downedBoss1 = false;
            NPC.downedBoss2 = false;
            NPC.downedBoss3 = false;
            NPC.downedChristmasIceQueen = false;
            NPC.downedChristmasSantank = false;
            NPC.downedChristmasTree = false;
            NPC.downedClown = false;
            NPC.downedFishron = false;
            NPC.downedFrost = false;
            NPC.downedGoblins = false;
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
            NPC.downedQueenBee = false;
            NPC.downedSlimeKing = false;
            NPC.downedTowerNebula = false;
            NPC.downedTowerSolar = false;
            NPC.downedTowerStardust = false;
            NPC.downedTowerVortex = false;
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
            AAWorld.downedToad = false;
            AAWorld.downedYamata = false;
            AAWorld.downedZero = false;
            AAWorld.downedRajahsRevenge = false;
            return true;
        }
    }
}
