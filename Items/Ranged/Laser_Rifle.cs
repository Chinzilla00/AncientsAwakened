using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class Laser_Rifle : ModItem
	{
		public static short customGlowMask = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Laser Carbine");
			Tooltip.SetDefault("Uses energy cells as ammo");
			if (Main.netMode != 2)
			{
				Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					glowMasks[i] = Main.glowMaskTexture[i];
				}
				glowMasks[glowMasks.Length - 1] = mod.GetTexture("Items/Ranged/" + this.GetType().Name + "_Glowmask");
				customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
		}

		public override void SetDefaults()
		{
			item.damage = 53;
			item.ranged = true;
			item.width = 46;
			item.height = 22;
			item.useAnimation = 14;
			item.useTime = 14;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 2;
			item.value = Item.sellPrice(0, 4, 72, 0);
			item.rare = 5;
			item.UseSound = SoundID.Item12;
			item.autoReuse = true;
			item.shoot = 10;
			item.shootSpeed = 22f;
			item.useAmmo = mod.ItemType("Energy_Cell");
			item.glowMask = customGlowMask;
			item.crit = 5;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-4, 2);
		}
	}
}
