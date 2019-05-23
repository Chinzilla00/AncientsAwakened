using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.DevTools
{
    public class NoodleSword : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("[DEV] Noodle Sword");
            Tooltip.SetDefault(@"Top 10 op weapons in video games");
        }

        public override void SetDefaults()
        {
            item.damage = 10000;     
            item.melee = true;    
            item.width = 64;            
            item.height = 70;         
            item.useTime = 17;   
            item.useAnimation = 17;     
            item.useStyle = 1;       
            item.knockBack = 4;   
            item.value = 0;        
            item.rare = 11;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;   
            item.useTurn = true;
            item.expert = true;
			item.shoot = mod.ProjectileType("Noodle");
			item.shootSpeed = 9f;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Main.itemTexture[item.type];
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
    }
}
