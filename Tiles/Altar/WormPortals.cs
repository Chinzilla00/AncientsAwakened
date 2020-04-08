using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles.Altar
{
    public class DBPortal : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bright Star");
        }
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 100;
            npc.friendly = false;
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.timeLeft = 10;
            npc.alpha = 255;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public override bool PreDraw(SpriteBatch spritebatch, Color lightColor)
        {
            Texture2D DBPortal = mod.GetTexture("Tiles/Altar/DBPortal");
            Texture2D DBPortalBack = mod.GetTexture("Tiles/Altar/DBPortalBack");
            Texture2D DBEyes = mod.GetTexture("Tiles/Altar/DBPortalEyes");

            int diff = Main.LocalPlayer.miscCounter % 50;
            float diffFloat = diff / 50f;
            float auraPercent = BaseUtility.MultiLerp(diffFloat, 0f, 1f, 0f);

            BaseDrawing.DrawTexture(spritebatch, DBPortalBack, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, 0, 1, npc.frame, npc.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            BaseDrawing.DrawTexture(spritebatch, DBPortal, 0, npc.position, npc.width, npc.height, npc.scale, -npc.rotation, 0, 1, npc.frame, npc.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            BaseDrawing.DrawAura(spritebatch, DBEyes, 0, npc.position, npc.width, npc.height, auraPercent, 2f, npc.scale, 0f, 0, 1, default, 0, 0, npc.GetAlpha(Color.White));

            return false;
        }

        public override void AI()
        {
            npc.TargetClosest();
            Player player = Main.player[npc.target];
            MoveToPoint(player.Center - new Vector2(200, 300f));

            if (Vector2.Distance(npc.Center, player.Center) > 2000)
            {
                npc.alpha = 255;
                npc.Center = player.Center - new Vector2(200, 300f);
            }

            npc.rotation += .1f;

            if (NPC.AnyNPCs(ModContent.NPCType<WormSpawn>()))
            {
                npc.timeLeft = 60;
                if (npc.alpha > 30)
                {
                    npc.alpha -= 3;
                }
                else
                {
                    npc.alpha = 30;
                }
            }
            else
            {
                if (npc.alpha < 255)
                {
                    npc.alpha -= 5;
                }
            }
        }

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 14f;
            if (moveSpeed == 0f || npc.Center == point) return; //don't move if you have no move speed
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            if (length < 200f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 100f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 50f)
            {
                moveSpeed *= 0.5f;
            }
            npc.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            npc.velocity *= moveSpeed;
            npc.velocity *= velMultiplier;
        }

    }

    public class NCPortal : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Void");
        }
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 100;
            npc.friendly = false;
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.timeLeft = 10;
            npc.alpha = 255;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public override bool PreDraw(SpriteBatch spritebatch, Color lightColor)
        {
            Texture2D NCPortal = mod.GetTexture("Tiles/Altar/NCPortalEyes");
            Texture2D NCPortalBack = mod.GetTexture("Tiles/Altar/NCPortalEyes");
            Texture2D NCEyes = mod.GetTexture("Tiles/Altar/NCPortalEyes");

            int diff = Main.LocalPlayer.miscCounter % 50;
            float diffFloat = diff / 50f;
            float auraPercent = BaseUtility.MultiLerp(diffFloat, 0f, 1f, 0f);

            BaseDrawing.DrawTexture(spritebatch, NCPortalBack, 0, npc.position, npc.width, npc.height, npc.scale, -npc.rotation, 0, 1, npc.frame, npc.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            BaseDrawing.DrawTexture(spritebatch, NCPortal, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, 0, 1, npc.frame, npc.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            BaseDrawing.DrawAura(spritebatch, NCEyes, 0, npc.position, npc.width, npc.height, auraPercent, 2f, npc.scale, 0f, 0, 1, default, 0, 0, npc.GetAlpha(Color.White));

            return false;
        }

        public override void AI()
        {
            npc.TargetClosest();
            Player player = Main.player[npc.target];
            MoveToPoint(player.Center - new Vector2(-200, 300f));

            if (Vector2.Distance(npc.Center, player.Center) > 2000)
            {
                npc.alpha = 255;
                npc.Center = player.Center - new Vector2(-200, 300f);
            }

            npc.rotation += .1f;

            if (NPC.AnyNPCs(ModContent.NPCType<WormSpawn>()))
            {
                npc.timeLeft = 60;
                if (npc.alpha > 30)
                {
                    npc.alpha -= 3;
                }
                else
                {
                    npc.alpha = 30;
                }
            }
            else
            {
                if (npc.alpha < 255)
                {
                    npc.alpha -= 5;
                }
            }
        }

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 14f;
            if (moveSpeed == 0f || npc.Center == point) return; //don't move if you have no move speed
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            if (length < 200f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 100f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 50f)
            {
                moveSpeed *= 0.5f;
            }
            npc.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            npc.velocity *= moveSpeed;
            npc.velocity *= velMultiplier;
        }

    }
}