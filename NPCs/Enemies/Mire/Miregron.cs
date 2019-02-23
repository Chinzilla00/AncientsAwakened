using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Mire
{
    public class Miregron : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pigron");
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[170];
		}

		public override void SetDefaults()
        {
            npc.width = 44;
            npc.height = 36;
            npc.aiStyle = -1;
            npc.damage = 80;
            npc.defense = 12;
            npc.lifeMax = 210;
            npc.HitSound = SoundID.NPCHit27;
            npc.DeathSound = SoundID.NPCDeath30;
            npc.knockBackResist = 0.5f;
            npc.value = 2000f;
            animationType = NPCID.PigronCorruption;
            npc.buffImmune[31] = false;
        }


        public override void AI()
        {
            if (Main.rand.Next(1000) == 0)
            {
                Main.PlaySound(29, (int)npc.position.X, (int)npc.position.Y, 9, 1f, 0f);
            }
            npc.noGravity = true;
            if (!npc.noTileCollide)
            {
                if (npc.collideX)
                {
                    npc.velocity.X = npc.oldVelocity.X * -0.5f;
                    if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 2f)
                    {
                        npc.velocity.X = 2f;
                    }
                    if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -2f)
                    {
                        npc.velocity.X = -2f;
                    }
                }
                if (npc.collideY)
                {
                    npc.velocity.Y = npc.oldVelocity.Y * -0.5f;
                    if (npc.velocity.Y > 0f && npc.velocity.Y < 1f)
                    {
                        npc.velocity.Y = 1f;
                    }
                    if (npc.velocity.Y < 0f && npc.velocity.Y > -1f)
                    {
                        npc.velocity.Y = -1f;
                    }
                }
            }
            npc.TargetClosest(true);
            if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
            {
                if (npc.ai[1] > 0f && !Collision.SolidCollision(npc.position, npc.width, npc.height))
                {
                    npc.ai[1] = 0f;
                    npc.ai[0] = 0f;
                    npc.netUpdate = true;
                }
            }
            else if (npc.ai[1] == 0f)
            {
                npc.ai[0] += 1f;
            }
            if (npc.ai[0] >= 300f)
            {
                npc.ai[1] = 1f;
                npc.ai[0] = 0f;
                npc.netUpdate = true;
            }
            if (npc.ai[1] == 0f)
            {
                npc.alpha = 0;
                npc.noTileCollide = false;
            }
            else
            {
                npc.wet = false;
                npc.alpha = 200;
                npc.noTileCollide = true;
            }
            npc.rotation = npc.velocity.Y * 0.1f * (float)npc.direction;
            npc.TargetClosest(true);
            if (npc.direction == -1 && npc.velocity.X > -4f && npc.position.X > Main.player[npc.target].position.X + (float)Main.player[npc.target].width)
            {
                npc.velocity.X = npc.velocity.X - 0.08f;
                if (npc.velocity.X > 4f)
                {
                    npc.velocity.X = npc.velocity.X - 0.04f;
                }
                else if (npc.velocity.X > 0f)
                {
                    npc.velocity.X = npc.velocity.X - 0.2f;
                }
                if (npc.velocity.X < -4f)
                {
                    npc.velocity.X = -4f;
                }
            }
            else if (npc.direction == 1 && npc.velocity.X < 4f && npc.position.X + (float)npc.width < Main.player[npc.target].position.X)
            {
                npc.velocity.X = npc.velocity.X + 0.08f;
                if (npc.velocity.X < -4f)
                {
                    npc.velocity.X = npc.velocity.X + 0.04f;
                }
                else if (npc.velocity.X < 0f)
                {
                    npc.velocity.X = npc.velocity.X + 0.2f;
                }
                if (npc.velocity.X > 4f)
                {
                    npc.velocity.X = 4f;
                }
            }
            if (npc.directionY == -1 && (double)npc.velocity.Y > -2.5 && npc.position.Y > Main.player[npc.target].position.Y + (float)Main.player[npc.target].height)
            {
                npc.velocity.Y = npc.velocity.Y - 0.1f;
                if ((double)npc.velocity.Y > 2.5)
                {
                    npc.velocity.Y = npc.velocity.Y - 0.05f;
                }
                else if (npc.velocity.Y > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y - 0.15f;
                }
                if ((double)npc.velocity.Y < -2.5)
                {
                    npc.velocity.Y = -2.5f;
                }
            }
            else if (npc.directionY == 1 && (double)npc.velocity.Y < 2.5 && npc.position.Y + (float)npc.height < Main.player[npc.target].position.Y)
            {
                npc.velocity.Y = npc.velocity.Y + 0.1f;
                if ((double)npc.velocity.Y < -2.5)
                {
                    npc.velocity.Y = npc.velocity.Y + 0.05f;
                }
                else if (npc.velocity.Y < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y + 0.15f;
                }
                if ((double)npc.velocity.Y > 2.5)
                {
                    npc.velocity.Y = 2.5f;
                }
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.player;
            if (spawnInfo.player.GetModPlayer<AAPlayer>(mod).ZoneMire && Main.hardMode && !spawnInfo.playerSafe)
            {
                return SpawnCondition.UndergroundMimic.Chance;
            }
            return 0f;
        }

        public override void HitEffect(int hitDirection, double damage)
		{
            if (npc.life > 0)
            {
                int num589 = 0;
                while (num589 < damage / (double)npc.lifeMax * 50.0)
                {
                    int num590 = Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.AcidDust>(), 0f, 0f, 0, default(Color), 1.5f);
                    Main.dust[num590].velocity *= 1.5f;
                    Main.dust[num590].noGravity = true;
                    num589++;
                }
                return;
            }
            for (int num591 = 0; num591 < 10; num591++)
            {
                int num592 = Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.AcidDust>(), 0f, 0f, 0, default(Color), 1.5f);
                Main.dust[num592].velocity *= 2f;
                Main.dust[num592].noGravity = true;
            }
            for (int num593 = 0; num593 < 4; num593++)
            {
                int num594 = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y + (float)(npc.height / 2) - 10f), new Vector2((float)hitDirection, 0f), 99, npc.scale);
                Main.gore[num594].velocity *= 0.3f;
            }
        }

		public override void NPCLoot()
		{
            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3532, 1, false, 0, false, false);
            }
        }
	}
}