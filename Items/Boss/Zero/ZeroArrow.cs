using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class ZeroArrow : ModItem
	{
        
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Singularity Arrow");
            Tooltip.SetDefault(@"The only thing faster than light is the void that devours it
Non-consumable");
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

        public override void SetDefaults()
		{
			item.damage = 28;
			item.ranged = true;
			item.width = 14;
			item.height = 40;
			item.consumable = false;             //You need to set the item consumable so that the ammo would automatically consumed
			item.knockBack = 7f;
			item.value = Item.buyPrice(1, 0, 0, 0);
            item.rare = 6;
			item.shoot = mod.ProjectileType("Neutralizer");         //The speed of the projectile
			item.ammo = AmmoID.Arrow;              //The ammo class this ammo belongs to.
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
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MoonlordArrow, 999);
            recipe.AddIngredient(null, "ApocalyptitePlate", 1);
            recipe.AddIngredient(null, "UnstableSingularity", 1);
            recipe.AddTile(null, "BinaryReassembler");
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}
