using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework.Graphics;

namespace AAMod.NPCs.Enemies.Mire
{ 
    public class HarukaShadow : ModNPC
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("...");
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.defense = 1;
            npc.knockBackResist = 0f;
            npc.noGravity = false;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.lifeMax = 1;
            npc.damage = 0;
            npc.value = 0;
            npc.alpha = 50;
            npc.width = 38;
            npc.height = 58;
            npc.rarity = 1;
        }

        public override void AI()
        {
            if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
            {
                npc.ai[0] = 1;
            }
            if (npc.ai[0] == 1)
            {
                npc.dontTakeDamage = true;
                if (npc.ai[1] < 255)
                {
                    npc.alpha += 4;
                    if (Main.netMode != 1)
                    {
                        npc.ai[1] += 4;
                    }
                }
                else
                {
                    if (Main.netMode != 1)
                    {
                        npc.active = false;
                        npc.netUpdate = true;
                    }
                }
            }
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            knockback = 0;
            damage = 0;
            crit = false;
            if (npc.ai[0] != 1)
            {
                npc.ai[0] = 1;
                CombatText.NewText(npc.Hitbox, new Color(72, 78, 117), Lang.BossChat("HarukaShadow"));
            }
            return false;
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.ai[0] == 0)
            {
                npc.frame.Y = frameHeight;
            }
            else
            {
                npc.frame.Y = frameHeight * 2;
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            Texture2D tex = Main.npcTexture[npc.type];
            Texture2D tex2 = mod.GetTexture("NPCs/Bosses/Hydra/HarukaShade_Glow");
            BaseDrawing.DrawTexture(sb, tex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 3, npc.frame, npc.GetAlpha(dColor));
            if (npc.ai[0] == 0)
            {
                Lighting.AddLight(npc.Center, Color.MediumVioletRed.R / 180, Color.MediumVioletRed.G / 180, Color.MediumVioletRed.B / 180);
                BaseDrawing.DrawTexture(sb, tex2, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 3, npc.frame, Color.White);
            }
            return false;
        }
    }
}