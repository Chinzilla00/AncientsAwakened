using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Yamata        //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class Toxibomb : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 200;                      
            item.magic = true;  
            item.width = 32;     
            item.height = 28;    
            item.useTime = 26; 
            item.useAnimation = 26; 
            item.useStyle = 5;        
            item.noMelee = true;   
            item.knockBack = 1; 
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.mana = 9;
            item.UseSound = SoundID.Item20; 
            item.autoReuse = true; 
            item.shoot = mod.ProjectileType("Toxibomb");  
            item.shootSpeed = 20f;
            item.rare = 10;
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Abyssal Bomb");
			Tooltip.SetDefault("Fires off explosive spirit bombs");
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Yamata;;
                }
            }
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
            recipe.AddIngredient(null, "EventideAbyssium", 5);
            recipe.AddIngredient(null, "DreadScale", 5);
            recipe.AddIngredient(null, "BogBomb", 1);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
