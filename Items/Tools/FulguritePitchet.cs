using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class FulguritePitchet : ModItem
    {
        
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fulgurite Pitchet");
        }


        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 48;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = 4;
		    item.pick = 200;
            item.axe = 110;
            item.tileBoost += 1;

            item.damage = 60;
            item.knockBack = 4;

            item.useStyle = 1;
            item.useTime = 6;
            item.useAnimation = 22;

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
            recipe.AddIngredient(null, "FulguriteBar", 18);
            recipe.AddIngredient(ItemID.SoulofFright, 1);
            recipe.AddIngredient(ItemID.SoulofMight, 1);
            recipe.AddIngredient(ItemID.SoulofSight, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}