using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using BaseMod;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using AAMod.NPCs.Enemies.Sky;

namespace AAMod.NPCs.Bosses.Athena
{
	public class OwlRune : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.alpha = 255;
            npc.dontTakeDamage = true;
            npc.lifeMax = 1;
            npc.aiStyle = 0;
            npc.damage = Main.expertMode ? 50 : 84;
            npc.defense = Main.expertMode ? 1 : 1;
            npc.knockBackResist = 0.2f;
            npc.width = 152;
            npc.height = 84;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.lavaImmune = true;
            npc.noTileCollide = true;
            npc.noGravity = true;
        }

        public override void AI()
        {
            if (npc.ai[1] == 0)
            {
                npc.alpha -= 5;
                npc.scale += .019f;
                if (Main.netMode != 1)
                {
                    npc.ai[0]++;
                    if (npc.ai[0] >= 51)
                    {
                        npc.alpha = 0;
                        npc.scale = 1;
                        npc.ai[0] = 0;
                        npc.ai[1] = 1;
                        npc.netUpdate = true;
                    }
                }
            }
            else if (npc.ai[1] == 1)
            {
                if (npc.alpha <= 0)
                {
                    npc.alpha = 0;
                }
                if (npc.scale > 1)
                {
                    npc.scale = 1;
                }
                if (Main.netMode != 1)
                {
                    npc.ai[0]++;
                    if (npc.ai[0] >= 300)
                    {
                        npc.ai[0] = 0;
                        npc.ai[1] = 2;
                        npc.netUpdate = true;
                    }
                }
                npc.TargetClosest();
                Player player = Main.player[npc.target];
                BaseAI.ShootPeriodic(npc, player.position, npc.width, npc.height, mod.ProjectileType<SeraphFeather>(), ref npc.ai[2], 15, npc.damage / 4, 10, false);
            }
            else
            {
                if (Main.netMode != 1)
                {
                    npc.ai[0]++;
                    if (npc.ai[0] >= 51)
                    {
                        npc.active = false;
                        npc.netUpdate = true;
                    }
                }
                npc.alpha += 5;
                npc.scale -= .019f;
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color drawColor)
        {
            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 7, npc.frame, npc.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            return false;
        }
    }
}