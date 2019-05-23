using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Terrarium.PreHM
{
    public class PuritySquid : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Purity Squid");
			Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults()
		{
            npc.lifeMax =  60;
            npc.defense = 20;
            npc.damage = 10;
            npc.width = 26;
            npc.height = 20;
            npc.aiStyle = -1;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.4f;
            npc.alpha = 255;
            npc.noTileCollide = false;
            npc.noGravity = true;
        }
        
        public override Color? GetAlpha(Color drawColor)
        {
            return Color.White;
        }

        public override void PostAI()
        {
            Player player = Main.player[Main.myPlayer];
            if (!player.GetModPlayer<AAPlayer>(mod).Terrarium)
            {
                npc.life = 0;
            }
        }

        public override bool PreNPCLoot()
        {
            Player player = Main.player[Main.myPlayer];
            if (!player.GetModPlayer<AAPlayer>(mod).Terrarium)
            {
                return false;
            }
            return true;
        }

        public override void AI()
        {
            if (npc.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 107, 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
            }
            BaseAI.AIElemental(npc, ref npc.ai, null, 120, false, true, 800, 400, 180, 2);

            if (npc.ai[0] == 2f)
            {
                npc.alpha += 12;
                if (npc.alpha > 255)
                {
                    npc.alpha = 255;
                }
            }
            else
            {
                npc.alpha -= 12;
                if (npc.alpha < 0)
                {
                    npc.alpha = 0;
                }
            }

            npc.rotation = npc.velocity.X / 15f;

            npc.frameCounter++;
            if (npc.frameCounter >= 10)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 36;
                if (npc.frame.Y > (36 * 3))
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = 0;
                }
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
