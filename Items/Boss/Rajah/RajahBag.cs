using Terraria;

namespace AAMod.Items.Boss.Rajah.Supreme
{
    public class RajahCache : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Cache");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.expert = true;
        }

        public override int BossBagNPC => mod.NPCType("SupremeRajah");

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("RajahMask"));
            }
            if (Main.rand.Next(20) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
                modPlayer.PMLDevArmor();
            }
            player.QuickSpawnItem(mod.ItemType<RajahPelt>(), Main.rand.Next(15, 31));
            player.QuickSpawnItem(mod.ItemType("RajahPelt"), Main.rand.Next(20, 25));
            player.QuickSpawnItem(mod.ItemType("RajahSash"));
            string[] lootTable = { "BaneOfTheBunny", "Bunzooka", "Punisher", "RabbitcopterEars", "RoyalScepter" };
            int loot = Main.rand.Next(lootTable.Length);
            if (Main.rand.Next(6) == 1 && AAMod.thoriumLoaded)
            {
                player.QuickSpawnItem(mod.ItemType("CarrotFarmer"));
            }
            else
            {
                player.QuickSpawnItem(mod.ItemType(lootTable[loot]));
            }
        }
    }
}