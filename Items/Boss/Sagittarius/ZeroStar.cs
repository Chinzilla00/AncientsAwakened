using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using System;

namespace AAMod.Items.Boss.Sagittarius
{
    public class ZeroStar : BaseAAItem
    {
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Zero Star");
            Tooltip.SetDefault("A spinning blade of doom");
        }
		public override void SetDefaults()
		{
	        item.damage = 25;
	        item.width = 46;
	        item.height = 46;
	        item.useTime = 23;
	        item.useAnimation = 30;
	        item.useStyle = 1;
	        item.knockBack = 6;
	        item.value = Item.sellPrice(0, 30, 0, 0);
            item.UseSound = SoundID.Item1;
	        item.autoReuse = true;
            item.melee = true;
            item.shoot = mod.ProjectileType("ZeroStarP");
            item.shootSpeed = 20f;
            item.noMelee = true;
            item.noUseGraphic = true;
            
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
        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "Doomite", 25);
                recipe.AddIngredient(null, "DoomiteScrap", 15);
                recipe.AddTile(null, "ACS");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}