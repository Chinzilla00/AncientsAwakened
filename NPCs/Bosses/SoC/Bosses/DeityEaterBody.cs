using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Bosses.SoC.Bosses
{
    public class DeityEaterBody : DeityEater
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crom Cruach");
        }
        
        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.dontCountMe = true;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }


        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (AAWorld.Anticheat == true)
            {
                if (damage > npc.lifeMax / 8)
                {
                    damage = 0;
                }

                return false;
            }

            return true;
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[npc.target];
            if (player.vortexStealthActive && projectile.ranged)
            {
                damage /= 2;
                crit = false;
            }
            if (projectile.penetrate == -1 && !projectile.minion)
            {
                projectile.damage *= (int).2;
            }
            else if (projectile.penetrate >= 1)
            {
                projectile.damage *= (int).2;
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color drawColor)
        {
            Texture2D currentTex = Main.npcTexture[npc.type];
            Texture2D GlowTex = mod.GetTexture("Glowmasks/DeityEaterBody_Glow");

            BaseDrawing.DrawTexture(sb, currentTex, 0, npc, drawColor);

            //draw glow/glow afterimage
            BaseDrawing.DrawTexture(sb, GlowTex, 0, npc, AAColor.Cthulhu2);
            BaseDrawing.DrawAfterimage(sb, GlowTex, 0, npc, 0.8f, 1f, 6, false, 0f, 0f, AAColor.Cthulhu2);

            return false;
        }
    }
}
