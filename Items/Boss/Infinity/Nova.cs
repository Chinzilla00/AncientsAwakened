using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Infinity
{
    public class Nova : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Nova");
            Tooltip.SetDefault("Fires an explosive energy blast that causes an expanding explosion");
        }

        public override void SetDefaults()
        {
            item.useStyle = 5;
            item.useAnimation = 18;
            item.useTime = 18;
            item.shootSpeed = 10f;
            item.knockBack = 0f;
            item.width = 48;
            item.height = 54;
            item.damage = 390;
            item.UseSound = SoundID.Item20;
            item.shoot = mod.ProjectileType("NovaBurst");
            item.mana = 20;
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.noMelee = true;
            item.magic = true;
            item.noUseGraphic = false;
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
            recipe.AddIngredient(null, "VoidStar", 1);
            recipe.AddIngredient(null, "Infinitium", 12);
            recipe.AddTile(null, "BinaryReassembler");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
