using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Infinity
{
    public class TotalDestruction : ModItem
    {
        
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Total Destruction");
            Tooltip.SetDefault("Destroys everything in front of you with a destructive laser");
        }

        public override void SetDefaults()
        {
            
            item.useStyle = 5;
            item.useAnimation = 7;
            item.useTime = 7;
            item.mana = 10;
            item.shootSpeed = 16f;
            item.knockBack = 0f;
            item.width = 122;
            item.reuseDelay = 5;
            item.height = 32;
            item.damage = 250;
            item.UseSound = SoundID.Item13;
            item.channel = true;
            item.shoot = mod.ProjectileType("TotalDestruction");
            item.value = Item.sellPrice(1, 0, 0, 0);
            item.noMelee = true;
            item.magic = true;
            item.autoReuse = true;
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
                    line2.overrideColor = AAColor.IZ;
                }
            }
        }
        
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "DoomRay");
			recipe.AddIngredient(null, "Infinitium", 12);
	        recipe.AddTile(null, "BinaryReassembler");
	        recipe.SetResult(this);
	        recipe.AddRecipe();
		}
	}
}
