using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Terrarium.PostPlant
{
    public class Minion3 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Terra Sphere");
		}

		public override void SetDefaults()
		{
            npc.width = 26;
            npc.height = 26;
            npc.lifeMax =  350;
            npc.defense = 20;
            npc.damage = 10;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.4f;
            npc.alpha = 255;
            npc.noGravity = true;
            npc.noTileCollide = true;
        }


        public float[] shootAI = new float[4];

        public override void AI()
        {
            Player player = Main.player[npc.target];

            BaseAI.AISkull(npc, ref npc.ai, false, 4, 300, .011f, .020f);
            BaseAI.ShootPeriodic(npc, player.position, player.width, player.height, mod.ProjType("SummonBlast"), ref shootAI[0], 120, (int)(npc.damage * (Main.expertMode ? 0.25f : 0.5f)), 3f, true, new Vector2(20f, 15f));

            if (npc.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("SummonDust"), 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
            }
            npc.alpha -= 12;
            if (npc.alpha < 0)
            {
                npc.alpha = 0;
            }
        }

        public void FireMagic(NPC npc, Vector2 velocity)
        {
            Player player = Main.player[npc.target];
            BaseAI.ShootPeriodic(npc, player.position, player.width, player.height, mod.ProjType("SummonBlast"), ref shootAI[0], 5, (int)(npc.damage * (Main.expertMode ? 0.25f : 0.5f)), 24f, true, new Vector2(20f, 15f));
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {

                npc.position.X = npc.position.X + npc.width / 2;
                npc.position.Y = npc.position.Y + npc.height / 2;
                npc.width = 44;
                npc.height = 78;
                npc.position.X = npc.position.X - npc.width / 2;
                npc.position.Y = npc.position.Y - npc.height / 2;
                int dust1 = mod.DustType<Dusts.SummonDust>();
                int dust2 = mod.DustType<Dusts.SummonDust>();
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust2, 0f, 0f, 0);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType<Buffs.Terrablaze>(), 300);
        }
    }
}
