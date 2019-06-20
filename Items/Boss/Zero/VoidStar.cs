using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class VoidStar : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Void Star");
            Tooltip.SetDefault("Fires a dark, spinning vortex that homes in on enemies");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.useStyle = 5;
            item.useAnimation = 15;
            item.useTime = 15;
            item.shootSpeed = 10f;
            item.knockBack = 0f;
            item.width = 30;
            item.height = 26;
            item.damage = 290;
            item.UseSound = SoundID.Item20;
            item.shoot = mod.ProjectileType("VoidStarPF");
            item.mana = 18;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.noMelee = true;
            item.magic = true;
            item.noUseGraphic = true;
            item.rare = 9; AARarity = 13;
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
            recipe.AddIngredient(ItemID.NebulaArcanum);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
