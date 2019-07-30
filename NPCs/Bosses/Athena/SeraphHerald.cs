using AAMod.NPCs.Enemies.Sky;
using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;

namespace AAMod.NPCs.Bosses.Athena
{
	public class SeraphHerald : Seraph
	{
        public override string Texture => "AAMod/NPCs/Bosses/Athena/SeraphA";

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
            npc.damage = 0;
            npc.value = 0;
        }

        int pos;
        public override bool PreAI()
        {
            npc.TargetClosest();
            Player player = Main.player[npc.target];

            if (player.Center.X > npc.Center.X)
            {
                if (pos == -250)
                {
                    pos = 250;
                }

                npc.direction = 1;
            }
            else
            {
                if (pos == 250)
                {
                    pos = -250;
                }

                npc.direction = -1;
            }

            Vector2 wantedVelocity = player.Center - new Vector2(pos, 200);
            MoveToPoint(wantedVelocity);

            if (Main.netMode != 2)
            {
                npc.frameCounter++;
                if (npc.frameCounter >= 6)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += Main.npcTexture[npc.type].Height / 4;
                }
                if (npc.frame.Y > Main.npcTexture[npc.type].Height / 4 * 3)
                {
                    npc.frame.Y = 0;
                }
            }
            if (Main.netMode != 1)
            {
                if (Vector2.Distance(player.Center, npc.Center) > 2200)
                {
                    npc.position = new Vector2(pos, 200);
                    for (int i = 0; i < 5; i++)
                    {
                        Dust d = Main.dust[Dust.NewDust(npc.position, npc.height, npc.width, mod.DustType<Feather>(), Main.rand.Next(-1, 2), 1, 0)];
                        d.position = npc.Center;
                    }
                }
                npc.ai[0]++;

                if (npc.ai[0] == 1)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("HEY! EARTHWALKER! YEAH YOU, YA STUPID APE!", Color.Silver);
                    npc.netUpdate = true;
                }
                else
                if (npc.ai[0] == 120)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("Queen Athena requests your presence at the acropolis again immediately!", Color.Silver);
                }
                else
                if (npc.ai[0] == 240)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("She demands a rematch, and this time, she won't let you tear her down so easily!", Color.Silver);
                }
                else
                if (npc.ai[0] >= 360)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("I would say break a leg, but we can do that ourselves when you show up!", Color.Silver);
                }
                else
                if (npc.ai[0] >= 480)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("See ya twerp!", Color.Silver);
                    npc.active = false;
                    npc.netUpdate = true;
                }
            }
            return false;
        }

        public void MoveToPoint(Vector2 point, bool goUpFirst = false)
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