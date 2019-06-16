using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Mire
{
    public class Newt : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Newt");
            Main.npcFrameCount[npc.type] = 15;
        }

        public override void SetDefaults()
        {
            npc.width = 112;
            npc.height = 30;
            npc.damage = 10;
            npc.defense = 10;
            npc.damage = 28;
            npc.defense = 6;
            npc.lifeMax = 60;
            npc.knockBackResist = 0.55f;
            npc.value = 100f;
            npc.aiStyle = 3;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            aiType = NPCID.Crawdad;
        }

        private bool tongueAttack;
        private int tongueFrame;
        private int tongueCounter;
        private int tongueTimer;

        public override void AI()
        {
            Player player = Main.player[npc.target]; // makes it so you can reference the player the npc is targetting
            if (tongueAttack == false)
            {
                npc.frameCounter++;
                if (npc.frameCounter >= 10)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += 30;
                    if (npc.frame.Y > 420)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = 0;
                    }
                }
            }
            else
            {
                npc.frameCounter = 0;
                npc.frame.Y = 0;
            }
            if (!tongueAttack)
            {
                if (npc.velocity.X < 0) // so it faces the player
                {
                    npc.direction = 1;
                }
                else if (npc.velocity.X > 0)
                {
                    npc.direction = -1;
                }
            }
            else
            {
                if (player.position.X < npc.position.X)
                {
                    npc.direction = 1;
                }
                else
                {
                    npc.direction = -1;
                }
            }
            if (tongueAttack == true)
            {
                if (tongueFrame < 3)
                {
                    tongueCounter++;
                }
                if (tongueCounter > 5)
                {
                    tongueFrame++;
                    tongueCounter = 0;
                }
                if (tongueFrame >= 3)
                {
                    tongueFrame = 0;
                }
            }
            float distance = npc.Distance(Main.player[npc.target].Center);
            if (distance >= 100) // distance until it does the tongue attack
            {
                if (Main.rand.Next(30) == 0) // so it wont do it repeatedly when the player is near. increase to lower the chance of it doing it
                {
                    if (tongueAttack == false)
                    {
                        tongueAttack = true;
                    }
                }
            }
            if (tongueAttack == true)
            {
                tongueTimer++;
                npc.aiStyle = 0;
                npc.velocity.X = 0;
                if (tongueTimer == 35)
                {
                    if (npc.direction == -1)
                    {
                        Projectile.NewProjectile((new Vector2(npc.position.X + 56f, npc.Center.Y)), new Vector2(6 + Main.rand.Next(0, 6), -4 + Main.rand.Next(-4, 0)), mod.ProjectileType("AcidProj"), 15, 3);
                    }
                    else
                    {
                        Projectile.NewProjectile((new Vector2(npc.Center.X - 56f, npc.Center.Y)), new Vector2(-6 + Main.rand.Next(-6, 0), -4 + Main.rand.Next(-4, 0)), mod.ProjectileType("AcidProj"), 15, 3);
                    }
                }
                if (tongueTimer >= 100)
                {
                    tongueAttack = false;
                    tongueTimer = 0;
                    tongueCounter = 0;
                    tongueFrame = 0;
                }
            }
            if (tongueAttack == false) // so it changes back to aiStyle 3 after the attacks are done
            {
                npc.aiStyle = 3;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/NewtGoreTail"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/NewtGoreBody"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/NewtGoreLeg"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/NewtGoreLeg"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/NewtGoreLeg"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/NewtGoreLeg"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/NewtGoreHead"), 1f);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Texture2D tongueAni = mod.GetTexture("NPCs/Enemies/Mire/Newt_Shoot");
            var effects = npc.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            if (tongueAttack == false) // i think this is important for it to not do its usual walking cycle while its also doing those attacks
            {
                spriteBatch.Draw(texture, npc.Center - Main.screenPosition, npc.frame, drawColor, npc.rotation, npc.frame.Size() / 2, npc.scale, effects, 0f);
            }
            if (tongueAttack == true)
            {
                Vector2 drawCenter = new Vector2(npc.Center.X, npc.Center.Y);
                int num214 = tongueAni.Height / 4;
                int y6 = num214 * tongueFrame;
                Main.spriteBatch.Draw(tongueAni, drawCenter - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, y6, tongueAni.Width, num214)), drawColor, npc.rotation, new Vector2((float)tongueAni.Width / 2f, (float)num214 / 2f), npc.scale, effects, 0f);
            }
            return false;
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MirePod"));
        }
    }
}