using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Terrarium.PreHM
{
    public class PuritySphere : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Purity Sphere");
		}

		public override void SetDefaults()
		{
            npc.width = 26;
            npc.height = 26;
            npc.lifeMax =  60;
            npc.defense = 5;
            npc.damage = 10;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.4f;
            npc.alpha = 255;
            npc.noGravity = true;
            npc.noTileCollide = false;
        }

        public override Color? GetAlpha(Color drawColor)
        {
            return Color.White;
        }

        public float[] shootAI = new float[4];

        public override void AI()
        {
            Player player = Main.player[npc.target];
            BaseAI.AISkull(npc, ref npc.ai, true, 4, 300, .011f, .020f);
            BaseAI.ShootPeriodic(npc, player.position, player.width, player.height, mod.ProjType("TerraShot"), ref shootAI[0], 120, (int)(npc.damage * (Main.expertMode ? 0.25f : 0.5f)), 3f, true, new Vector2(20f, 15f));
            if (npc.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 107, 0f, 0f, 100, default(Color), .8f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
            }
            npc.alpha -= 12;
            if (npc.alpha < 0)
            {
                npc.alpha = 0;
            }

            if (!Collision.CanHit(npc.position, npc.width, npc.height, player.position, player.width, player.height))
            {
                npc.noTileCollide = true;
            }
            else
            {
                npc.noTileCollide = false;
            }
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(4) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType<Items.Materials.TerraShard>());
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 107, 0f, 0f, 0, default(Color), 1f);
                }
            }
        }
    }
}
