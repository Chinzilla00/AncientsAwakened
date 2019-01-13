using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class DarkmatterPitchet : ModItem
    {
        
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Darkmatter Pitchet");
        }


        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 54;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = 11;
		    item.pick = 235;
            item.axe = 100;
            item.tileBoost += 4;

            item.damage = 60;
            item.knockBack = 4;

            item.useStyle = 1;
            item.useTime = 5;
            item.useAnimation = 19;

            item.melee = true;
            item.useTurn = true;
            item.autoReuse = true;

            
            item.UseSound = SoundID.Item1;
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
            recipe.AddIngredient(null, "DarkMatter", 20);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}