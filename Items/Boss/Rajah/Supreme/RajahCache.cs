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
            item.expert = true; item.expertOnly = true;
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
            if (Main.rand.Next(10) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.SADevArmor();
            }
            player.QuickSpawnItem(Terraria.ModLoader.ModContent.ItemType<ChampionPlate>(), Main.rand.Next(15, 31));
            player.QuickSpawnItem(mod.ItemType("RajahCape"));
            string[] lootTable = { "Excalihare", "FluffyFury", "RabbitsWrath", "BaneOfTheBunnyEX", "CottonCaneEX", "PunisherEX", "RoyalScepterEX", "BunzookaEX"};
            int loot = Main.rand.Next(lootTable.Length);
            player.QuickSpawnItem(mod.ItemType(lootTable[loot]));
        }
    }
}