using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using AAMod.Dusts;

namespace AAMod.NPCs.Enemies.Void
{
    public class Scout : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Scout");
        }

        public override void SetDefaults()
        {
            npc.width = 38;
            npc.height = 38;
            npc.value = 0;
            npc.npcSlots = 1;
            npc.aiStyle = -1;
            npc.lifeMax = 1200;
            npc.defense = 120;
            npc.damage = 80;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.knockBackResist = 0.3f;
            npc.noGravity = true;
            npc.noTileCollide = true;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            bool isDead = npc.life <= 0;
            for (int m = 0; m < (isDead ? 25 : 5); m++)
            {
                int dustType = mod.DustType<VoidDust>();
                Dust.NewDust(npc.position, npc.width, npc.height, dustType, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 100, Color.White, (isDead ? 2f : 1.1f));
            }
        }

        float shootAI = 0;
        public override void AI()
        {
            BaseAI.AISkull(npc, ref npc.ai, false, 6f, 350f, 0.6f, 0.15f);
            Player player = Main.player[npc.target];
            bool playerActive = player != null && player.active && !player.dead;
            Vector2 vector2 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
            if (shootAI < 60)
            {
                BaseAI.LookAt(player.Center, npc, 3, 0, .1f, false);
            }
            if (Main.netMode != 1 && playerActive)
            {
                shootAI++;
                if (shootAI >= 90)
                {
                    shootAI = 0;
                    int projType = mod.ProjType("NeutralizerP");

                    if (Collision.CanHit(npc.position, npc.width, npc.height, player.position, player.width, player.height))
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, projType, (int)(npc.damage * 0.25f), 3f, Main.myPlayer, npc.whoAmI);

                    }
                }
            }
        }


        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidEnergy"), Main.rand.Next(4));
        }

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/Scout_Glow");
            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc, AAColor.ZeroShield);
            return false;
        }
    }
}