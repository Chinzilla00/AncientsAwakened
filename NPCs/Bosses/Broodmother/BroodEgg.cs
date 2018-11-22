using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Broodmother
{
    [AutoloadBossHead]
    public class BroodEgg : ModNPC
    {
        private Player player;
        private float speed;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Egg");

        }
        public override void SetDefaults()
        {
            npc.width = 34;
            npc.height = 34;
            npc.aiStyle = 0;
            npc.damage = 0;
            npc.defense = 30;
            npc.lavaImmune = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.lifeMax = 50;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 0f;
            npc.knockBackResist = 2f;
            npc.npcSlots = 0f;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            {
                SpriteEffects spriteEffects = SpriteEffects.None;
                if (npc.spriteDirection == 1)
                {
                    spriteEffects = SpriteEffects.FlipHorizontally;
                }
                spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/Broodmother/BroodEgg_Glow"), new Vector2(npc.Center.X - Main.screenPosition.X, npc.Center.Y - Main.screenPosition.Y),
                npc.frame, Color.White, npc.rotation,
                new Vector2(npc.width * 0.5f, npc.height * 0.5f), 1f, spriteEffects, 0f);
            }
        }


        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);
        }
        
        public override void AI()
        {
            if (npc.velocity.Y == 0f)
            {
                npc.velocity.X = npc.velocity.X * 0.9f;
                npc.rotation += npc.velocity.X * 0.02f;
            }
            else
            {
                npc.velocity.X = npc.velocity.X * 0.99f;
                npc.rotation += npc.velocity.X * 0.04f;
            }
            int num1326 = 900;
            if (Main.expertMode)
            {
                num1326 = 600;
            }
            if (npc.justHit)
            {
                npc.ai[0] -= (float)Main.rand.Next(10, 21);
                if (!Main.expertMode)
                {
                    npc.ai[0] -= (float)Main.rand.Next(10, 21);
                }
            }
            npc.ai[0] += 1f;
            if (npc.ai[0] >= num1326)
            {
                npc.Transform(mod.NPCType("Broodmini"));
            }
            if (Main.netMode != 1 && npc.velocity.Y == 0f && (double)Math.Abs(npc.velocity.X) < 0.2 && (double)npc.ai[0] >= (double)num1326 * 0.75)
            {
                float num1327 = npc.ai[0] - ((float)num1326 * 0.75f);
                num1327 /= (float)num1326 * 0.25f;
                if ((float)Main.rand.Next(-10, 120) < num1327 * 100f)
                {
                    npc.velocity.Y = npc.velocity.Y - (Main.rand.Next(20, 40) * 0.025f);
                    npc.velocity.X = npc.velocity.X + (Main.rand.Next(-20, 20) * 0.025f);
                    npc.velocity *= 1f + (num1327 * 2f);
                    npc.netUpdate = true;
                    return;
                }
            }
        }
    }
}