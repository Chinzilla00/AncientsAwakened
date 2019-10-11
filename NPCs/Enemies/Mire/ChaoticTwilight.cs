using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System;

namespace AAMod.NPCs.Enemies.Mire
{
    public class ChaoticTwilight : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chaotic Twilight");
            Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults()
        {
            npc.width = 74;
            npc.height = 76;
            npc.damage = 90;
			npc.defense = 10;
			npc.lifeMax = 200;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath6;
            npc.value = 24000f;
            npc.knockBackResist = .30f;
            npc.aiStyle = -1;
            npc.noTileCollide = true;
            npc.noGravity = true;
        }

        public override void AI()
        {
            npc.noGravity = true;
            npc.noTileCollide = true;

            Lighting.AddLight((int)((npc.position.X + npc.width / 2) / 16f), (int)((npc.position.Y + npc.height / 2) / 16f), 0f, 0f, 0.3f);

            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
            }
            if (npc.ai[0] == 0f)
            {
                float num312 = 9f;
                Vector2 vector32 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float num313 = Main.player[npc.target].position.X + Main.player[npc.target].width / 2 - vector32.X;
                float num314 = Main.player[npc.target].position.Y + Main.player[npc.target].height / 2 - vector32.Y;
                float num315 = (float)Math.Sqrt(num313 * num313 + num314 * num314);
                num315 = num312 / num315;
                num313 *= num315;
                num314 *= num315;
                npc.velocity.X = num313;
                npc.velocity.Y = num314;
                npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 0.785f;
                npc.ai[0] = 1f;
                npc.ai[1] = 0f;
                npc.netUpdate = true;
                return;
            }
            if (npc.ai[0] == 1f)
            {
                if (npc.justHit)
                {
                    npc.ai[0] = 2f;
                    npc.ai[1] = 0f;
                }
                npc.velocity *= 0.99f;
                npc.ai[1] += 1f;
                if (npc.ai[1] >= 100f)
                {
                    npc.netUpdate = true;
                    npc.ai[0] = 2f;
                    npc.ai[1] = 0f;
                    npc.velocity.X = 0f;
                    npc.velocity.Y = 0f;
                    return;
                }
            }
            else
            {
                if (npc.justHit)
                {
                    npc.ai[0] = 2f;
                    npc.ai[1] = 0f;
                }
                npc.velocity *= 0.96f;
                npc.ai[1] += 1f;
                float num316 = npc.ai[1] / 120f;
                num316 = 0.1f + num316 * 0.4f;
                npc.rotation += num316 * npc.direction;
                if (npc.ai[1] >= 120f)
                {
                    npc.netUpdate = true;
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                    return;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter < 3)
            {
                npc.frame.Y = 0 * frameHeight;
            }
            else if (npc.frameCounter < 6)
            {
                npc.frame.Y = 1 * frameHeight;
            }
            else if (npc.frameCounter < 9)
            {
                npc.frame.Y = 2 * frameHeight;
            }
            else if (npc.frameCounter < 12)
            {
                npc.frame.Y = 3 * frameHeight;
            }
            else
            {
                npc.frameCounter = 0;
            }
        }
        

		public override void HitEffect(int hitDirection, double damage)
		{

            int dust1 = Terraria.ModLoader.ModContent.DustType<Dusts.MireBubbleDust>();
            if (npc.life <= 0)
			{
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0);
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0);
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0);
            }
		}

		public override void NPCLoot()
		{
            
                if (Main.rand.NextFloat() < 0.1f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AbyssalTwilight"));
                }
        }
	}
}