using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
    public class StormJavelin : ModItem
    {

        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Storm Javelin Javelin");
            Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {

            item.damage = 70;           //this is the item damage
            item.melee = true;             //this make the item do throwing damage
            item.noMelee = true;
            item.width = 30;
            item.height = 30;
            item.useTime = 18;       //this is how fast you use the item
            item.useAnimation = 18;   //this is how fast the animation when the item is used
            item.useStyle = 1;      
            item.knockBack = 6;
            item.value = 1;
            item.rare = 8;
            item.reuseDelay = 0;    //this is the item delay
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;       //this make the item auto reuse
            item.shoot = mod.ProjectileType("StormJavelin");  //javelin projectile
            item.shootSpeed = 15f;     //projectile speed
            item.useTurn = true;
            item.maxStack = 999;       //this is the max stack of this item
            item.consumable = true;  //this make the item consumable when used
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



        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FulguriteBar");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }
    }
}
