using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System;

namespace AAMod.Items.Dev
{
    public class UmbreonSPEX : ModItem
	{
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Umbra");
			Tooltip.SetDefault(@"A dark sword from a dark creature
Blade of Night EX");
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
            item.expert = true;
		}


        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
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
