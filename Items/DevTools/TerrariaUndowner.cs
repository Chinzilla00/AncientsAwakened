using Terraria;
using Terraria.ID;

namespace AAMod.Items.DevTools
{
    public class TerrariaUndowner : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terraria Undowner");
            Tooltip.SetDefault(@"Undowns all Vanilla bosses.
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
            AAWorld.downedRajahsRevenge = false;
            return true;
        }
    }
}
