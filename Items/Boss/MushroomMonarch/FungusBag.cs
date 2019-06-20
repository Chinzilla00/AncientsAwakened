using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.MushroomMonarch
{
    public class FungusBag : BaseAAItem
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
            item.rare = 11;
            item.expert = true;
            bossBagNPC = mod.NPCType("FeudalFungus");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(mod.ItemType<Items.Vanity.Mask.FungusMask>());
            }
            if (Main.rand.NextFloat(20) == 1)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
                modPlayer.PHMDevArmor();
            }
            player.QuickSpawnItem(mod.ItemType("GlowingMushium"), Main.rand.Next(30, 40));
            player.QuickSpawnItem(mod.ItemType("MagicTruffle"));
        }
    }
}