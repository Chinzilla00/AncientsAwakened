using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Boss.Zero
{
    public class ZeroTerratool_Hammer : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.melee = true;
            item.width = 60;
            item.height = 54;
			item.useStyle = 1;
            item.useTime = 5;
            item.useAnimation = 20;
            item.tileBoost += 20;
            item.knockBack = 3;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 9; AARarity = 13;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.damage = 100;
            item.hammer = 260;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terratool");
            Tooltip.SetDefault("Right Click to change tool types");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            byte pre = item.prefix;
            item.TurnToAir();
            int itemID = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("ZeroTerratool"), 1, false, pre, false, false);
            if (Main.netMode == 1)
            {
                NetMessage.SendData(21, -1, -1, null, itemID, 1f, 0f, 0f, 0, 0, 0);
            }
        }
    }
}
