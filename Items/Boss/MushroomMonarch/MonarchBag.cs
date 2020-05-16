using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.MushroomMonarch
{
    public class MonarchBag : BaseAAItem
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
            item.height = 36;
            item.rare = ItemRarityID.Purple;
            item.expert = true; item.expertOnly = true;
        }
        public override int BossBagNPC => mod.NPCType("MushroomMonarch");

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(Terraria.ModLoader.ModContent.ItemType<Vanity.Mask.MonarchMask>());
            }
            if (Main.rand.Next(10) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PHMDevArmor();
            }
            player.QuickSpawnItem(mod.ItemType("Mushium"), Main.rand.Next(30, 40));
            player.QuickSpawnItem(mod.ItemType("HeartyTruffle"));
        }
    }
}