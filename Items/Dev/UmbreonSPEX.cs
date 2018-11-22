using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AAMod.Items.Dev
{
    public class UmbreonSPEX : ModItem
	{
		public static short customGlowMask = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Umbra");
			Tooltip.SetDefault(@"A dark sword from a dark creature
Blade of Night EX");
			if (Main.netMode != 2)
			{
				Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					glowMasks[i] = Main.glowMaskTexture[i];
				}
				glowMasks[glowMasks.Length - 1] = mod.GetTexture("Items/Dev/UmbreonSPEX_Glow");
				customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
		}
		
		public override void SetDefaults()
		{
			item.damage = 425;
			item.melee = true;
			item.width = 100;
			item.height = 100;
			item.useTime = 22;
			item.useAnimation = 22;
			item.useStyle = 1;
			item.knockBack = 7;
			item.value = Item.buyPrice(1, 1, 50, 0);
			item.rare = 2;
			item.UseSound = SoundID.Item71;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("UmbreonSPProjectile");
			item.shootSpeed = 18f;
			item.glowMask = customGlowMask;
		}
		
		public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
		    float spread = 20f * 0.0174f;
		    float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
		    double deltaAngle = spread / 6f;
		    double offsetAngle;
		    for (int i = 0; i < 3; i++)
		    {
		    	offsetAngle = startAngle + (deltaAngle * i);
		    	Terraria.Projectile.NewProjectile(position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), item.shoot, damage, knockBack, item.owner);
		    }
		    return false;
		}
		
		public override void AddRecipes()
        {
		    ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "UmbreonSP", 1);
            recipe.AddIngredient(null, "EXSoul", 1);
            recipe.AddTile(TileID.LunarCraftingStation); // (null, "ModTileID");
		    recipe.SetResult(this);
		    recipe.AddRecipe();
        }
	}
}
