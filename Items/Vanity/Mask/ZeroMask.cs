using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Mask
{
    [AutoloadEquip(EquipType.Head)]
	public class ZeroMask : BaseAAItem
	{
        public static int type;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Zero Mask");
		}

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 26;
            item.rare = 2;
            item.vanity = true;
        }
    }
}