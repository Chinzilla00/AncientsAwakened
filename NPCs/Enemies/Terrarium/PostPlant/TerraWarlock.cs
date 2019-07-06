using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Enemies.Terrarium.PostPlant
{
    public class TerraWarlock : ModNPC
    {
		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if((Main.netMode == 2 || Main.dedServ))
			{
				writer.Write(internalAI[0]);
				writer.Write(internalAI[1]);
			}
		}

        public override void PostAI()
        {
            Player player = Main.player[Main.myPlayer];
            if (!player.GetModPlayer<AAPlayer>(mod).Terrarium)
            {
                npc.life = 0;
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Warlock");
            Main.npcFrameCount[npc.type] = 15;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 600;
            npc.defense = 40;
            npc.damage = 120;
            npc.width = 38;
            npc.height = 60;
            npc.aiStyle = -1;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.4f;

        }
      
		public static int AISTATE_WALK = 0, AISTATE_SUMMON = 1;
		public float[] internalAI = new float[2];
        public int SummonThis = 0;
		
        public override void AI()
        {
            
            Player player = Main.player[npc.target]; // makes it so you can reference the player the npc is targetting
            npc.frameCounter++;
            if (internalAI[1] != AISTATE_SUMMON) //walk or charge
            {
				if (npc.frameCounter >= 10)
				{
					npc.frameCounter = 0;
					npc.frame.Y += 60;
					if (npc.frame.Y > (60 * 7))
					{
						npc.frameCounter = 0;
						npc.frame.Y = 0;
					}
				}
                if(npc.velocity.Y != 0)
                {
                    npc.frame.Y = 0;
                }
            }
            else //jump
            {
                if (npc.frameCounter >= 10)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += 60;
                    if (npc.frame.Y > (60 * 14))
                    {

                        Vector2 spawnAt = npc.Center + new Vector2(0f, npc.height / 2f);
                        if (Main.expertMode)
                        {
                            SummonThis = Main.rand.Next(4);

                            switch (SummonThis)
                            {
                                case 0:
                                    SummonThis = mod.NPCType("Minion1");
                                    break;
                                case 1:
                                    SummonThis = mod.NPCType("Minion2");
                                    break;
                                case 2:
                                    SummonThis = mod.NPCType("Minion3");
                                    break;
                                default:
                                    SummonThis = mod.NPCType("Minion4");
                                    break;
                            }
                            NPC.NewNPC((int)spawnAt.X - 10, (int)spawnAt.Y - 10, SummonThis);
                        }
                        internalAI[1] = AISTATE_WALK;
                    }
                    if (npc.frame.Y > (60 * 14) || npc.frame.Y < (60 * 8))
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = (60 * 8);
                    }
                }
            }
            if (player.Center.X > npc.Center.X) // so it faces the player
            {
                npc.spriteDirection = -1;
            }else
            {
                npc.spriteDirection = 1;
            }
			if(Main.netMode != 1)
			{
				internalAI[0]++;
				if (internalAI[0] >= 240)
				{
					internalAI[0] = 0;
                    if (internalAI[1] == AISTATE_SUMMON)
                    {
                        internalAI[1] = AISTATE_WALK;
                    }
                    if (internalAI[1] == AISTATE_WALK)
                    {
                        internalAI[1] = AISTATE_SUMMON;
                    }
					npc.ai = new float[4];
					npc.netUpdate = true;
				}
			}
			if(internalAI[1] == AISTATE_WALK) //fighter
			{
                BaseAI.AIZombie(npc, ref npc.ai, false, false, 0, 0.07f, 3f, 3, 4, 60, true, 10, 60, true, null, false);				
			}
            else
			{
                npc.velocity.X = 0;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/TerraWarlockGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/TerraWarlockGore2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/TerraWarlockGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/TerraWarlockGore4"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/TerraWarlockGore5"), 1f);
                npc.position.X = npc.position.X + npc.width / 2;
                npc.position.Y = npc.position.Y + npc.height / 2;
                npc.width = 44;
                npc.height = 78;
                npc.position.X = npc.position.X - npc.width / 2;
                npc.position.Y = npc.position.Y - npc.height / 2;
                int dust1 = mod.DustType<Dusts.SummonDust>();
                int dust2 = mod.DustType<Dusts.SummonDust>();
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust2, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;
            }
        }


        public override void NPCLoot()
        {
            if (Main.rand.Next(40) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType<Items.Materials.TerraCrystal>());
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType<Buffs.Terrablaze>(), 300);
        }
    }
}


