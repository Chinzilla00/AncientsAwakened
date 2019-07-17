using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Serpent
{
    public class IceCrystal : ModNPC
    {
        public override void SetDefaults()
        {
            npc.life = 200;
            npc.defense = 10;
            npc.width = 30;
            npc.height = 30;
            npc.aiStyle = -1;
            npc.alpha = 255;
            npc.value = 0;
        }

		public override void SetStaticDefaults()
		{
		    DisplayName.SetDefault("Ice Crystal");
		}

        public override void AI()
        {
            if (Main.netMode != 1)
            {
                npc.ai[2]++;
            }
            if (npc.alpha > 0)
            {
                npc.alpha -= 3;
            }
            else
            {
                npc.alpha = 0;
            }
            float num398 = 700f;
            bool flag11 = false;
            for (int num399 = 0; num399 < Main.maxPlayers; num399++)
            {
                float num400 = Main.player[num399].position.X + Main.player[num399].width / 2;
                float num401 = Main.player[num399].position.Y + Main.player[num399].height / 2;
                float num402 = Math.Abs(npc.position.X + (npc.width / 2) - num400) + Math.Abs(npc.position.Y + (npc.height / 2) - num401);
                if (num402 < num398 && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[num399].position, Main.player[num399].width, Main.player[num399].height))
                {
                    num398 = num402;
                    flag11 = true;
                }
            }
            if (flag11)
            {
                BaseMod.BaseAI.ShootPeriodic(npc, npc.position, npc.width, npc.height, mod.ProjectileType<IceSpike>(), ref npc.ai[0], 180, npc.damage / 2, 9, true);
                return;
            }
        }

        public override void NPCLoot()
        {
            Main.PlaySound(2, npc.position, 50);
            int pieCut = 20;
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(npc.Center.X - 1, npc.Center.Y - 1), 2, 2, mod.DustType<Dusts.SnowDust>(), 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default, new Vector2(6f, 0f), (m / pieCut) * 6.28f);
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(npc.Center.X - 1, npc.Center.Y - 1), 2, 2, mod.DustType<Dusts.SnowDust>(), 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default, new Vector2(9f, 0f), (m /pieCut) * 6.28f);
                Main.dust[dustID].noLight = false;
            }
        }
    }
}
