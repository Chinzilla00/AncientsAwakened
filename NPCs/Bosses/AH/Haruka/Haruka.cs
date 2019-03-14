using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.NPCs.Bosses.AH.Haruka
{
    [AutoloadBossHead]
    public class Haruka : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Haruka Yamata");
        }

        public override void SetDefaults()
        {
            Main.npcFrameCount[npc.type] = 4;
            npc.width = 84;
            npc.height = 72;
            npc.friendly = false;
            npc.damage = 80;
            npc.defense = 50;
            npc.lifeMax = 120000;
            npc.HitSound = SoundID.NPCHit1;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.knockBackResist = 0f;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.lavaImmune = true;
            npc.boss = true;
            npc.netAlways = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/AH");
            npc.noGravity = true;
            npc.noTileCollide = true;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, mod.DustType<Dusts.AcidDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
            AAWorld.downedHaruka = true;
        }
        public override void NPCLoot()
        {
            int Ashe = NPC.CountNPCS(mod.NPCType("Ashe"));
            if (Ashe == 0)
            {
               
            }
            if (!Main.expertMode)
            {
                npc.DropLoot(mod.ItemType("EventideAbyssium"), 5, 10);
                string[] lootTable = { "Masamune" };
                int loot = Main.rand.Next(lootTable.Length);
                npc.DropLoot(mod.ItemType(lootTable[loot]));
                Main.NewText("Rgh..! Ow...", new Color(45, 46, 70));
            }
            npc.value = 0f;
            npc.boss = false;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 0.6f);  //boss damage increase in expermode
        }

        public int attackMode = 0;

        public int CurrentFrame = 0;

        public void ResetFrame()
        {
            npc.frame.Y = 0;
        }


        public override void FindFrame(int frameHeight)
        {
            if (attackMode == 0)
            {
                Main.npcFrameCount[npc.type] = 4;
            }
            if (attackMode == 1)
            {
                Main.npcFrameCount[npc.type] = 4;
            }
            if (attackMode == 2)
            {
                Main.npcFrameCount[npc.type] = 8;
            }
            if (attackMode == 3)
            {
                Main.npcFrameCount[npc.type] = 17;
            }
            if (attackMode == 4)
            {
                Main.npcFrameCount[npc.type] = 7;
            }
        }

        public override void AI()
        {
            npc.active = false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Texture2D jump = mod.GetTexture("NPCs/Bosses/AH/Haruka/HarukaJump");
            Texture2D walk = mod.GetTexture("NPCs/Bosses/AH/Haruka/HarukaWalk");
            Texture2D slash = mod.GetTexture("NPCs/Bosses/AH/Haruka/HarukaSlash");
            Texture2D spin = mod.GetTexture("NPCs/Bosses/AH/Haruka/HarukaSpin");
            Texture2D CurrentTex = texture;
            int frameCount = 4;
            if (attackMode == 0)
            {
                CurrentTex = texture;
                frameCount = 4;
            }
            if (attackMode == 1)
            {
                CurrentTex = jump;
                frameCount = 4;
            }
            if (attackMode == 2)
            {
                CurrentTex = walk;
                frameCount = 8;
            }
            if (attackMode == 3)
            {
                CurrentTex = slash;
                frameCount = 17;
            }
            if (attackMode == 4)
            {
                CurrentTex = spin;
                frameCount = 7;
            }

            int num214 = CurrentTex.Height / frameCount;

            int y6 = num214 * CurrentFrame;

            Vector2 drawCenter = new Vector2(npc.Center.X, npc.Center.Y) - Main.screenPosition;

            var effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            Rectangle? source = new Rectangle?(new Rectangle(0, y6, CurrentTex.Width, num214));

            Vector2 Origin = new Vector2(CurrentTex.Width / 2f, num214 / 2f);


            Main.spriteBatch.Draw(CurrentTex, drawCenter, source, drawColor, npc.rotation, Origin, npc.scale, effects, 0f);
            
            return false;
        }
        
        private void DespawnHandler()
        {
            Player player = Main.player[npc.target];
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead)
                {
                    npc.velocity = new Vector2(0f, -10f);
                    if (npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                    return;
                }
            }
        }
    }
}