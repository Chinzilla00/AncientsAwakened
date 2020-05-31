using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Rajah
{
    public class RajahBag : BaseAAItem
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
        }

        public override int BossBagNPC => mod.NPCType("Rajah");

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
            if (Main.rand.Next(10) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PMLDevArmor();
            }
            player.QuickSpawnItem(Terraria.ModLoader.ModContent.ItemType<RajahPelt>(), Main.rand.Next(15, 31));
            player.QuickSpawnItem(mod.ItemType("RajahPelt"), Main.rand.Next(20, 25));
            player.QuickSpawnItem(mod.ItemType("RajahSash"));
            string[] lootTable = { "BaneOfTheBunny", "Bunzooka", "Punisher", "RabbitcopterEars", "RoyalScepter" };
            int loot = Main.rand.Next(lootTable.Length);
            if (Main.rand.Next(6) == 1 && ModLoader.GetMod("ThoriumMod") != null)
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