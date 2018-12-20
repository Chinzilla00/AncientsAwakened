using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class ZeroStar : ModItem
    {
        
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Zero Star");
            Tooltip.SetDefault("A spinning blade of doom");
        }

		public override void SetDefaults()
		{
			item.damage = 170;
			item.width = 46;
			item.height = 46;
			item.useTime = 12;
			item.useAnimation = 12;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = Item.buyPrice(1, 0, 0, 0);
            item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.thrown = true;
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

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Zero;
                }
            }
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "ApocalyptitePlate", 5);
                recipe.AddIngredient(null, "UnstableSingularity", 5);
                recipe.AddIngredient(ItemID.LightDisc, 5);
                recipe.AddTile(null, "ACS");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
	}
}