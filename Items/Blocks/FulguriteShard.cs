using Terraria;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class FulguriteShard : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 22;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = 4;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("FulguriteOre");
            item.value = 10000;
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


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fulgurite Shard");
            Tooltip.SetDefault("The fury of a thousand bolts of lightning run through this shard");
        }
    }
}
