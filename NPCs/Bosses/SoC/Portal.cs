using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.SoC
{
    public class Portal : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rift Portal");
        }
        public override void SetDefaults()
        {
            npc.width = 120;
            npc.height = 120;
            npc.alpha = 255;
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.timeLeft = 10;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }


        public bool Spawned = false;

        public override void AI()
        {
            npc.scale = 1f - npc.alpha / 255f;
            npc.rotation += .05f;
            npc.velocity.X = npc.ai[0];
            npc.velocity.Y = npc.ai[1];

            if (npc.alpha <= 0 && !Spawned)
            {
                SummonEnemy();
                Spawned = true;
            }
            if (!Spawned)
            {
                npc.alpha -= 3;
            }
            if (Spawned)
            {
                npc.alpha += 3;
                if (npc.alpha >= 255)
                {
                    npc.active = false;
                }
            }
        }

        public override void NPCLoot()
        {
            for (int num468 = 0; num468 < 3; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width, 1, mod.DustType<Dusts.AkumaADust>(), -npc.velocity.X * 0.2f,
                    -npc.velocity.Y * 0.2f, 100, default(Color), 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width, npc.height, mod.DustType<Dusts.AkumaADust>(), -npc.velocity.X * 0.2f,
                    -npc.velocity.Y * 0.2f, 100, default(Color));
                Main.dust[num469].velocity *= 2f;
            }
        }

        public void SummonEnemy()
        {
            int Enemy = Main.rand.Next(3);

            switch (Enemy)
            {
                case 0:
                    Enemy = mod.NPCType("DeityDragon");
                    break;
                case 1:
                    Enemy = mod.NPCType("EoA");
                    break;
                default:
                    Enemy = mod.NPCType("RiftVision");
                    break;
            }
            if (Main.netMode != 1)
            {
                int npcID = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, Enemy);
                Main.npc[npcID].Center = npc.Center;
                Main.npc[npcID].netUpdate = true;
            }

            npc.active = false;
        }
    }
}