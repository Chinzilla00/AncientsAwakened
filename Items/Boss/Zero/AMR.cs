using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class AMR : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Anti-matter Rifle");
            Tooltip.SetDefault(@"Fires a laser that bounces between enemies
Hold to charge the rifle
the longer the rifle is charged, the more it penetrates");
        }

        public override void SetDefaults()
        {
            item.noUseGraphic = true;
            item.damage = 600;
            item.noMelee = true;
            item.ranged = true;
            item.width = 74;
            item.height = 24;
            item.useTime = 65;
            item.useAnimation = 65; 
            item.useStyle = 5; 
            item.shoot = mod.ProjectileType("AMR");
            item.channel = true;
            item.knockBack = 12;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 9;
            item.shootSpeed = 8f;
            item.crit += 5;
            item.rare = 9; AARarity = 13;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity13;
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
            recipe.AddIngredient(null, "ApocalyptitePlate", 5);
            recipe.AddIngredient(null, "UnstableSingularity", 5);
            recipe.AddIngredient(ItemID.SniperRifle);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}