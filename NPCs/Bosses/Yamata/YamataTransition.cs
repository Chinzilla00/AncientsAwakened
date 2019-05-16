using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Yamata
{
    public class YamataTransition : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirit of Wrath");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 100;
            npc.friendly = false;
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.timeLeft = 10;
            npc.scale *= 1.3f;
            npc.alpha = 255;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public int RVal = 125;
        public int BVal = 255;


        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            BaseDrawing.DrawTexture(spriteBatch, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 24, npc.frame, npc.GetAlpha(new Color(RVal, 0, BVal)), true);
            return false;
        }

        public override void AI()
        {

            Player player = Main.player[npc.target];
            MoveToPoint(player.Center - new Vector2(0, 300f));

            if (Vector2.Distance(npc.Center, player.Center) > 2000)
            {
                npc.alpha = 255;
                npc.Center = player.Center - new Vector2(0, 300f);
            }


            npc.ai[0]++;
            npc.frameCounter++;
            if (npc.frameCounter >= 7)
            {
                npc.frameCounter = 0;
                npc.frame.Y += Main.npcTexture[npc.type].Height / 4 ;
            }

            if (npc.frame.Y > (Main.npcTexture[npc.type].Height / 4) * 3)
            {
                npc.frame.Y = 0 ;
            }

            if (npc.ai[0] > 375)
            {
                npc.alpha -= 5;
                if (npc.alpha < 0)
                {
                    npc.alpha = 0;
                }
            }

            if (npc.ai[0] == 375)    
            {
                Main.NewText("NYEHEHEHEHEHEHEHEH~!", new Color(45, 46, 70));
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Yamata2");
            }
            if (npc.ai[0] == 650)
            {
                Main.NewText("You thought I was DONE..?!", new Color(45, 46, 70));
            }
            if (npc.ai[0] == 900)
            {
                Main.NewText("HAH! AS IF!", new Color(45, 46, 70));
            }

            if (npc.ai[0] >= 900)
            {
                RVal += 5;
                BVal -= 5;
                if (RVal <= 90)
                {
                    BVal = 90;
                }
                if (RVal >= 255)
                {
                    RVal = 255;
                }
            }

            if (npc.ai[0] == 1100)
            {
                Main.NewText("The abyss hungers...", new Color(146, 30, 68));
            }
            if (npc.ai[0] >= 1455 && !NPC.AnyNPCs(mod.NPCType("YamataA")))
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("YamataA"));
            }

            if (NPC.AnyNPCs(mod.NPCType("YamataA")))
            {
                Main.NewText("Yamata has been Awakened!", Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
                Main.NewText("AND IT'S GOT 7 HEADS! NYEHEHEHEHEHEHEHEHEHEHEHEH!!!", new Color(146, 30, 68));
                npc.netUpdate = true;
                npc.active = false;
            }
        }

        public void MoveToPoint(Vector2 point, bool goUpFirst = false)
        {
            float moveSpeed = 14f;
            if (moveSpeed == 0f || npc.Center == point) return; //don't move if you have no move speed
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
            float length = (dist == Vector2.Zero ? 0f : dist.Length());
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            if (length < 200f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 100f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 50f)
            {
                moveSpeed *= 0.5f;
            }
            npc.velocity = (length == 0f ? Vector2.Zero : Vector2.Normalize(dist));
            npc.velocity *= moveSpeed;
            npc.velocity *= velMultiplier;
        }

        public void SpawnBoss(Vector2 center, string name, string displayName)
        {
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)center.X, (int)center.Y, bossType, 0, 0, 0, 0, 0, npc.target);
                Main.npc[npcID].Center = center - new Vector2(MathHelper.Lerp(-100f, 100f, (float)Main.rand.NextDouble()), 0f);
                Main.npc[npcID].netUpdate2 = true; Main.npc[npcID].netUpdate = true;			
            }
        }
        public override bool CheckActive()
        {
            if (!NPC.AnyNPCs(mod.NPCType("AkumaA")))
            {
                return false;
            }
            return true;
        }
    }
}