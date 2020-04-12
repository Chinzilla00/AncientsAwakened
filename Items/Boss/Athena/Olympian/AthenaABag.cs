using Terraria;

namespace AAMod.Items.Boss.Athena.Olympian
{
    public class AthenaABag : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.expert = true; item.expertOnly = true;
            item.rare = 10;
        }

        public override int BossBagNPC => mod.NPCType("AthenaA");

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("AthenaMask"));
            }
            if (Main.rand.Next(20) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PMLDevArmor();
            }
            player.QuickSpawnItem(mod.ItemType("GoddessHarp"));
            player.QuickSpawnItem(mod.ItemType("GoddessFeather"), Main.rand.Next(20, 25));
            player.QuickSpawnItem(mod.ItemType("SkyCrystal"), Main.rand.Next(30, 50));
            string[] lootTable = { "HurricaneStone", "Olympia", "Windfury", "GaleForce" };
            int loot = Main.rand.Next(lootTable.Length);
            player.QuickSpawnItem(mod.ItemType(lootTable[loot]));
            player.QuickSpawnItem(mod.ItemType("StarChart"));
        }
    }
}