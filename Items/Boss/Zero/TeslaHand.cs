using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class TeslaHand : ModItem
    {
        
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Broken Zero Weapon");
            Tooltip.SetDefault("Just swing it around and it'll shock whatever's in front of you");
        }

        public override void SetDefaults()
        {
            
            item.width = 36;
            item.height = 42;
            item.damage = 240;
            item.noMelee = true;
            item.noUseGraphic = false;
            item.channel = true;
            item.autoReuse = true;
            item.ranged = true;
            item.useAnimation = 9;
            item.useTime = 9;
            item.useStyle = 1;
            item.knockBack = 2f;
            item.UseSound = SoundID.Item116;
            item.value = Item.sellPrice(1, 0, 0, 0);
            item.shootSpeed = 20f;
            item.shoot = mod.ProjectileType("Teslashock");
            item.rare = 10;
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

        // How can I make the shots appear out of the muzzle exactly?
        // Also, when I do this, how do I prevent shooting through tiles?
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			return true;
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "ApocalyptitePlate", 5);
			recipe.AddIngredient(null, "UnstableSingularity", 5);
	        recipe.AddTile(null, "ACS");
	        recipe.SetResult(this);
	        recipe.AddRecipe();
		}
	}
}
