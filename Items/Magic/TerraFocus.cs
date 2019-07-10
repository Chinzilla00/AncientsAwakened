using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Magic
{
    public class TerraFocus : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Focus");
            Tooltip.SetDefault(@"Fires shots of terra magic at your foes");
        }

        public override void SetDefaults()
        {
            item.useStyle = 5;
            item.useAnimation = 12;
            item.useTime = 4;
            item.reuseDelay = item.useAnimation + 6;
            item.shootSpeed = 14f;
            item.knockBack = 6f;
            item.width = 16;
            item.height = 16;
            item.damage = 50;
            item.UseSound = SoundID.Item9;
            item.crit = 20;
            item.shoot = 660;
            item.mana = 14;
            item.rare = 4;
            item.value = 300000;
            item.noMelee = true;
            item.magic = true;
            item.autoReuse = true;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Glowmasks/" + GetType().Name);
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