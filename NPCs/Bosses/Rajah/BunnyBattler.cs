using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.NPCs.Bosses.Rajah
{
    public class BunnyBattler : ModNPC
    {
        public override string Texture => "AAMod/NPCs/Bosses/Rajah/BunnyBattler";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rabbid Rabbit");
            Main.npcFrameCount[npc.type] = 6;
        }

        public override void SetDefaults()
        {
            npc.width = 48;
            npc.height = 40;
            npc.aiStyle = -1;
            npc.damage = 90;
            npc.defense = 30;
            npc.lifeMax = 300;
            npc.knockBackResist = 0f;
            npc.npcSlots = 0f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
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

        public override void AI()
        {
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            if (npc.velocity.Y != 0)
            {
                if (npc.velocity.X < 0)
                {
                    npc.spriteDirection = -1;
                }
                else if (npc.velocity.X > 0)
                {
                    npc.spriteDirection = 1;
                }
            }
            else
            {
                if (player.position.X < npc.position.X)
                {
                    npc.spriteDirection = -1;
                }
                else if (player.position.X > npc.position.X)
                {
                    npc.spriteDirection = 1;
                }
            }
            BaseAI.AISlime(npc, ref npc.ai, false, 25, 6f, -8f, 6f, -10f);
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.velocity.Y < 0)
            {
                npc.frame.Y = frameHeight * 4;
            }
            else if (npc.velocity.Y > 0)
            {
                npc.frame.Y = frameHeight * 5;
            }
            else if (npc.ai[0] < -15f)
            {
                npc.frame.Y = 0;
            }
            else if (npc.ai[0] > -15f)
            {
                npc.frame.Y = frameHeight;
            }
            else if (npc.ai[0] > -10f)
            {
                npc.frame.Y = frameHeight * 2;
            }
            else if (npc.ai[0] > -5f)
            {
                npc.frame.Y = frameHeight * 3;
            }
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override void PostAI()
        {
            if (NPC.AnyNPCs(mod.NPCType<Rajah>()) ||
                NPC.AnyNPCs(mod.NPCType<Rajah2>()) ||
                NPC.AnyNPCs(mod.NPCType<Rajah3>()) ||
                NPC.AnyNPCs(mod.NPCType<Rajah4>()) ||
                NPC.AnyNPCs(mod.NPCType<Rajah5>()) ||
                NPC.AnyNPCs(mod.NPCType<Rajah6>()) ||
                NPC.AnyNPCs(mod.NPCType<Rajah7>()) ||
                NPC.AnyNPCs(mod.NPCType<Rajah8>()) ||
                NPC.AnyNPCs(mod.NPCType<Rajah9>()) ||
                NPC.AnyNPCs(mod.NPCType<SupremeRajah>()))
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
    public class BunnyBattler1 : BunnyBattler
    {
        public override string Texture => "AAMod/NPCs/Bosses/Rajah/BunnyBattler";
        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 100;
            npc.defense = 40;
            npc.lifeMax = 400;
        }
    }
    public class BunnyBattler2 : BunnyBattler
    {
        public override string Texture => "AAMod/NPCs/Bosses/Rajah/BunnyBattler";
        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 130;
            npc.defense = 50;
            npc.lifeMax = 600;
        }
    }
    public class BunnyBattler3 : BunnyBattler
    {
        public override string Texture => "AAMod/NPCs/Bosses/Rajah/BunnyBattler";
        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 140;
            npc.defense = 60;
            npc.lifeMax = 900;
        }
    }
    public class BunnyBattler4 : BunnyBattler
    {
        public override string Texture => "AAMod/NPCs/Bosses/Rajah/BunnyBattler";
        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 150;
            npc.defense = 70;
            npc.lifeMax = 1200;
        }
        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            damage /= 2;
            return true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (NPC.AnyNPCs(mod.NPCType<SupremeRajah>()))
            {
                BaseDrawing.DrawAfterimage(spriteBatch, Main.npcTexture[npc.type], 0, npc, 1f, 1f, 10, true, 0f, 0f, Main.DiscoColor);
            }
            return false;
        }
    }
}