using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using System.IO;

namespace AAMod.NPCs.Bosses.Sagittarius
{
    [AutoloadBossHead]
    public class Sag : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sagittarius");
            Main.npcFrameCount[npc.type] = 12;
		}

		public override void SetDefaults()
        {
            npc.lifeMax = 6000;
            npc.boss = true;
            npc.defense = 300;
            npc.damage = 35;
            npc.width = 124;
            npc.height = 186;
            npc.aiStyle = -1;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Sag");
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.noTileCollide = true;
            bossBag = mod.ItemType("SagBag");
        }

        public float[] internalAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
            }
        }

        public override void AI()
        {
            if (npc.target == -1)
            {
                npc.TargetClosest(true);
            }

            Player player = Main.player[npc.target];

            if (!DeathCheck())
                return;

            #region Direction & Alpha

            if (npc.alpha > 0)
            {
                npc.alpha -= 10;
            }
            if (npc.alpha <= 0)
            {
                npc.alpha = 0;
            }

            if (player.Center.X > npc.Center.X)
            {
                npc.direction = -1;
            }
            else
            {
                npc.direction = 1;
            }

            #endregion
            


            npc.rotation = 0;
            npc.noTileCollide = true;
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.velocity.X != 0)
            {
                if (npc.frameCounter++ > 8)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += frameHeight;
                    if (npc.frame.Y > frameHeight * 3)
                    {
                        npc.frame.Y = 0;
                    }
                }
            }
        }

        public bool DeathCheck()
        {
            AAPlayer modPlayer = Main.player[npc.target].GetModPlayer<AAPlayer>();
            if (Main.player[npc.target].dead || Vector2.Distance(Main.player[npc.target].Center, npc.Center) > 5000 || !modPlayer.ZoneVoid)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead || Vector2.Distance(Main.player[npc.target].Center, npc.Center) > 5000 || !modPlayer.ZoneVoid)
                {
                    npc.velocity *= .7f;
                    npc.alpha += 5;
                    if (npc.alpha >= 255)
                    {
                        npc.active = false;
                    }
                    if (!Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) <= 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) >= 6000f)
                    {
                        npc.TargetClosest(true);
                    }
                    return false;
                }
            }
            return true;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity * 0.2f, mod.GetGoreSlot("Gores/SagBodyGore"), 1f);
                Gore.NewGore(npc.position, npc.velocity * 0.2f, mod.GetGoreSlot("Gores/SagHeadGore"), 1f);
                Gore.NewGore(npc.position, npc.velocity * 0.2f, mod.GetGoreSlot("Gores/SagLegGore"), 1f);
                Gore.NewGore(npc.position, npc.velocity * 0.2f, mod.GetGoreSlot("Gores/SagLegGore"), 1f);
                Gore.NewGore(npc.position, npc.velocity * 0.2f, mod.GetGoreSlot("Gores/SagNeckGore"), 1f);
                int dust1 = ModContent.DustType<Dusts.VoidDust>();
                int dust2 = ModContent.DustType<Dusts.VoidDust>();
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

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 12, npc.frame, dColor, true);
            BaseDrawing.DrawTexture(sb, mod.GetTexture("Glowmasks/Sagittarius_Glow"), 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 12, npc.frame, ColorUtils.COLOR_GLOWPULSE, true);
            return false;
        }

        public override void NPCLoot()
        {
            AAWorld.downedSag = true;
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SagTrophy"));
            }
            if (!Main.expertMode)
            {
                if (Main.rand.Next(7) == 0)
                {
                    npc.DropLoot(mod.ItemType("SagMask"));
                }
                string[] lootTable = { "SagCore", "NeutronStaff", "Legg" };
                int loot = Main.rand.Next(lootTable.Length);
                npc.DropLoot(mod.ItemType(lootTable[loot]));
                Item.NewItem(npc.Center, ModContent.ItemType<Items.Materials.Doomite>(), Main.rand.Next(30, 40));
            }
            else
            {
                npc.DropBossBags();
            }
        }
        
    }
}
