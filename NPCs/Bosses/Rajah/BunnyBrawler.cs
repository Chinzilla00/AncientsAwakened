using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.IO;
using BaseMod;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.NPCs.Bosses.Rajah
{
    public class BunnyBrawler : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bunny Brawler");
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            npc.width = 76;
            npc.height = 76;
            npc.aiStyle = -1;
            npc.damage = 120;
            npc.defense = 60;
            npc.lifeMax = 400;
            npc.knockBackResist = 0f;
            npc.npcSlots = 0f;
            npc.HitSound = SoundID.NPCHit14;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.aiStyle = 41;
            aiType = NPCID.Derpling;
            animationType = NPCID.Derpling;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            bool isDead = npc.life <= 0;
            if (isDead)          //this make so when the npc has 0 life(dead) he will spawn this
            {

            }
            for (int m = 0; m < (isDead ? 35 : 6); m++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 100, default, isDead ? 2f : 1.5f);
            }
        }
        public bool SetLife = false;
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(SetLife);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                SetLife = reader.ReadBool(); //Set Lifex
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.velocity.Y == 0)
            {
                npc.frame.Y = 0;
            }
            else if (npc.velocity.Y < 0)
            {
                npc.frame.Y = frameHeight;
            }
            else if(npc.velocity.Y > 0)
            {
                npc.frame.Y = frameHeight * 2;
            }
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override void PostAI()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<Rajah>()) ||
                   NPC.AnyNPCs(ModContent.NPCType<Rajah2>()) ||
                   NPC.AnyNPCs(ModContent.NPCType<Rajah3>()) ||
                   NPC.AnyNPCs(ModContent.NPCType<Rajah4>()) ||
                   NPC.AnyNPCs(ModContent.NPCType<Rajah5>()) ||
                   NPC.AnyNPCs(ModContent.NPCType<Rajah6>()) ||
                   NPC.AnyNPCs(ModContent.NPCType<Rajah7>()) ||
                   NPC.AnyNPCs(ModContent.NPCType<Rajah8>()) ||
                   NPC.AnyNPCs(ModContent.NPCType<Rajah9>()) ||
                   NPC.AnyNPCs(ModContent.NPCType<SupremeRajah>()))
            {
                if (npc.alpha > 0)
                {
                    npc.alpha -= 5;
                }
                else
                {
                    npc.alpha = 0;
                }
            }
            else
            {
                npc.dontTakeDamage = true;
                if (npc.alpha < 255)
                {
                    npc.alpha += 5;
                }
                else
                {
                    npc.active = false;
                }
            }
        }
    }
    public class BunnyBrawler1 : BunnyBrawler
    {
        public override string Texture => "AAMod/NPCs/Bosses/Rajah/BunnyBrawler";
        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 120;
            npc.defense = 70;
            npc.lifeMax = 600;
        }
    }
    public class BunnyBrawler2 : BunnyBrawler
    {
        public override string Texture => "AAMod/NPCs/Bosses/Rajah/BunnyBrawler";
        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 140;
            npc.defense = 70;
            npc.lifeMax = 800;
        }
    }
    public class BunnyBrawler3 : BunnyBrawler
    {
        public override string Texture => "AAMod/NPCs/Bosses/Rajah/BunnyBrawler";
        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 155;
            npc.defense = 90;
            npc.lifeMax = 1200;
        }
    }
    public class BunnyBrawler4 : BunnyBrawler
    {
        public override string Texture => "AAMod/NPCs/Bosses/Rajah/BunnyBrawler";
        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 170;
            npc.defense = 100;
            npc.lifeMax = 1600;
        }
        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            damage /= 2;
            return true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (NPC.AnyNPCs(ModContent.NPCType<SupremeRajah>()))
            {
                BaseDrawing.DrawAfterimage(spriteBatch, Main.npcTexture[npc.type], 0, npc, 1f, 1f, 10, true, 0f, 0f, Main.DiscoColor);
            }
            return false;
        }
    }
}