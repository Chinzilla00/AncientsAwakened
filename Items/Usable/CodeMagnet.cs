using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Usable
{
    public class CodeMagnet : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Binary Code Magnet");
			Tooltip.SetDefault(@"Pulls items to you by moving its code closer to you
Right click the item to turn it off");
		}

        public override void SetDefaults()
        {
            item.width = item.height = 16;
            item.rare = ItemRarityID.LightRed;
            item.maxStack = 1;
			item.value = 8000;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(ModContent.ItemType<CodeMagnetOff>());
        }
    }
}
