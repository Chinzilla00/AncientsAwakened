using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Inferno
{
    public class TheStrideDeathAnimation : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flamebrute");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void SetDefaults()
        {
            npc.dontTakeDamage = true;
            npc.lifeMax = 1;
            npc.width = 62;
            npc.height = 88;
            npc.friendly = false;
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
            npc.noGravity = false;
            npc.aiStyle = -1;
            npc.timeLeft = 48;

            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public override void AI()
        {
            if (npc.ai[0] > 0)
            {
                TheStrideGore();
                npc.life = 0;
            }
            base.AI();
        }
        public override void FindFrame(int frameHeight)
        {
            Player player = Main.player[npc.target];

            if (npc.frameCounter++ > 7)
            {
                npc.frameCounter = 0;
                npc.frame.Y = npc.frame.Y + frameHeight;
            }
            if (npc.frame.Y >= frameHeight * 6)
            {
                npc.ai[0]++;
                npc.frame.Y = 0;
                return;
            }
        }
        public void TheStrideGore()
        {
          for(int i = 0; i<20; i++)
          {
            int num = Dust.NewDust(npc.position, npc.width, npc.height, ModContent.DustType<Dusts.RealityDust>(), Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(0, -10), 6, new Color(255, 0, 0, 255), 1f);
            Main.dust[num].noGravity = false;
            Main.dust[num].velocity *= 2.5f;
            Main.dust[num].noLight = true;
          }
            Projectile.NewProjectile(npc.Center, new Vector2 (Main.rand.NextFloat(-20, 20), Main.rand.NextFloat(0, -40)), mod.ProjectileType("FlamebruteProjectileGore5"), npc.damage/2, 4f);
            Projectile.NewProjectile(npc.Center, new Vector2(Main.rand.NextFloat(-20, 20), Main.rand.NextFloat(0, -40)), mod.ProjectileType("FlamebruteProjectileGore4"), npc.damage / 2, 4f);
            Projectile.NewProjectile(npc.Center, new Vector2(Main.rand.NextFloat(-20, 20), Main.rand.NextFloat(0, -40)), mod.ProjectileType("FlamebruteProjectileGore3"), npc.damage / 2, 4f);            
        }


    }
}
