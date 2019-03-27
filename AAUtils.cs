using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;


namespace AAMod
{
    public class GenericUtils
    {
        //COLORS
        public static Color COLOR_GLOWPULSE //a pulsing white glow
        {
            get
            {
                return new Color(255, 255, 255) * ((float)Main.mouseTextColor / 255f);
            }
        }

        public static void MoveToPoint(Entity entity, Vector2 point, float moveSpeed)
        {
            float velMultiplier = 1f;
            Vector2 dist = point - entity.Center;
            float length = (dist == Vector2.Zero ? 0f : dist.Length());
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            entity.velocity = (length == 0f ? Vector2.Zero : Vector2.Normalize(dist));
            entity.velocity *= moveSpeed;
            entity.velocity *= velMultiplier;
        }
    }

    public static class ItemUtilities
    {
        public static void DropLoot(this Entity ent, int type, int stack = 1)
        {
            Item.NewItem((int)ent.position.X, (int)ent.position.Y, ent.width, ent.height, type, stack);
        }

        public static void DropLoot(this Entity ent, int type, float chance)
        {
            if (Main.rand.NextDouble() < chance)
                Item.NewItem((int)ent.position.X, (int)ent.position.Y, ent.width, ent.height, type);
        }

        public static void DropLoot(this Entity ent, int type, int min, int max)
        {
            Item.NewItem((int)ent.position.X, (int)ent.position.Y, ent.width, ent.height, type, Main.rand.Next(min, max));
        }
    }
    public class AAUtils : ModPlayer
    {
        public static void DrawNPCGlowMask(SpriteBatch spriteBatch, NPC npc, Texture2D texture)
        {
            var effects = npc.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), npc.frame,
                             Color.White, npc.rotation, npc.frame.Size() / 2, npc.scale, effects, 0);
        }

        public static void DrawItemGlowMask(Texture2D texture, PlayerDrawInfo info)
        {
            Item item = info.drawPlayer.HeldItem;
            if (info.shadow != 0f || info.drawPlayer.frozen || ((info.drawPlayer.itemAnimation <= 0 || item.useStyle == 0) && (item.holdStyle <= 0 || info.drawPlayer.pulley))/*||item.type<=0*/|| info.drawPlayer.dead || item.noUseGraphic || (info.drawPlayer.wet && item.noWet))
            {
                return;
            }
            Vector2 offset = new Vector2();
            float rotOffset = 0;
            Vector2 origin = new Vector2();
            if (item.useStyle == 5)
            {
                if (Item.staff[item.type])
                {
                    rotOffset = 0.785f * info.drawPlayer.direction;
                    if (info.drawPlayer.gravDir == -1f) { rotOffset -= 1.57f * info.drawPlayer.direction; }
                    origin = new Vector2(texture.Width * 0.5f * (1 - info.drawPlayer.direction), (info.drawPlayer.gravDir == -1f) ? 0 : texture.Height);
                    int num86 = -(int)origin.X;
                    ItemLoader.HoldoutOrigin(info.drawPlayer, ref origin);
                    offset = new Vector2(origin.X + num86, 0);
                }
                else
                {
                    offset = new Vector2(10, texture.Height / 2);
                    ItemLoader.HoldoutOffset(info.drawPlayer.gravDir, item.type, ref offset);
                    origin = new Vector2(-offset.X, texture.Height / 2);
                    if (info.drawPlayer.direction == -1)
                    {
                        origin.X = texture.Width + offset.X;
                    }
                    offset = new Vector2(texture.Width / 2, offset.Y);
                }
            }
            else
            {
                origin = new Vector2(texture.Width * 0.5f * (1 - info.drawPlayer.direction), (info.drawPlayer.gravDir == -1f) ? 0 : texture.Height);
            }
            Main.playerDrawData.Add
            (
                new DrawData
                (
                    texture,
                    info.itemLocation - Main.screenPosition + offset,
                    texture.Bounds,
                    new Color(250, 250, 250, item.alpha),
                    info.drawPlayer.itemRotation + rotOffset,
                    origin,
                    item.scale, info.spriteEffects, 0
                )
            );
        }

        public static void DrawItemGlowMaskWorld(SpriteBatch spriteBatch, Item item, Texture2D texture, float rotation, float scale)
        {
            Main.spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + (item.width / 2),
                    item.position.Y - Main.screenPosition.Y + item.height - (texture.Height / 2) + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                new Color(250, 250, 250, item.alpha), rotation,
                new Vector2(texture.Width / 2, texture.Height / 2),
                scale, SpriteEffects.None, 0f
            );
        }

    }
}

//ADD TO AAUTILS AT THE TOP
