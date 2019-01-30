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
    [AutoloadBossHead]
    public class DeityEater : SoC
	{
        public bool HeadsSpawned = false;


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crom Cruach");
        }


        public override void SetDefaults()
        {
            npc.width = 38;
            npc.height = 38;
            npc.aiStyle = -1;
            npc.netAlways = true;
            npc.damage = 90;
            npc.defense = 100;
            npc.lifeMax = 150000;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0f;
            npc.behindTiles = true;
            npc.scale = 1f;
            npc.buffImmune[20] = true;
            npc.buffImmune[24] = true;
            npc.buffImmune[39] = true;
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/SoC");
            for (int m = 0; m < npc.buffImmune.Length; m++) npc.buffImmune[m] = true;
            npc.alpha = 255;
        }
        
        public override bool PreNPCLoot()
        {
            return false;
        }

        public override void AI()
        {
            if (npc.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("CthulhuDust"), 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
            }
            npc.alpha -= 12;
            if (npc.alpha < 0)
            {
                npc.alpha = 0;
            }

            BaseAI.AIWorm(npc, new int[] { mod.NPCType("DeityEater"), mod.NPCType("DeityEaterBody"), mod.NPCType("DeityEaterTail") }, 30, 4f, 17f, 0f, true, false, true, false, false, false);
            bool isBody = npc.type == mod.NPCType("DeityEaterBody");
            if (isBody)
            {
                if (npc.localAI[0] == 0)
                {
                    npc.localAI[0] = 1;
                    npc.localAI[1] = Main.rand.Next(4);
                }
                npc.frame.Y = (int)npc.localAI[1] * npc.frame.Height;
            }
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (AAWorld.Anticheat == true)
            {
                if (damage > npc.lifeMax / 8)
                {
                    Main.NewText("YOU CANNOT CHEAT DEATH", Color.DarkCyan);
                    damage = 0;
                }

                return false;
            }

            return true;
        }

        public override void HitEffect(int hitDirection, double dmg)
        {
            if (npc.life > 0)
            {
                GoHere = npc.Center;
                ComeBack = true;
                int num121 = 0;
                while ((double)num121 < dmg / (double)npc.lifeMax * 3.0)
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), (float)hitDirection, -1f, 0, Color.Transparent, 0.75f);
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        Dust dust39 = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), 0f, 0f, 0, default(Color), 1f)];
                        dust39.noGravity = true;
                    }
                    for (int num122 = 0; num122 < npc.oldPos.Length; num122++)
                    {
                        if (Main.rand.Next(4) == 0)
                        {
                            if (npc.oldPos[num122] == Vector2.Zero)
                            {
                                break;
                            }
                            if (Main.rand.Next(3) == 0)
                            {
                                Dust.NewDust(npc.oldPos[num122], npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), (float)hitDirection, -1f, 0, Color.Transparent, 0.75f);
                            }
                            if (Main.rand.Next(2) == 0)
                            {
                                Dust dust40 = Main.dust[Dust.NewDust(npc.oldPos[num122], npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), 0f, 0f, 0, default(Color), 1f)];
                                dust40.noGravity = true;
                            }
                        }
                    }
                    num121++;
                }
            }
            else
            {
                for (int num123 = 0; num123 < 5; num123++)
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), (float)hitDirection, -1f, 0, default(Color), 1f);
                    }
                    Dust dust41 = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), 0f, 0f, 0, default(Color), 1f)];
                    dust41.noGravity = true;
                    dust41.velocity *= 3f;
                }
                for (int num124 = 0; num124 < npc.oldPos.Length; num124++)
                {
                    if (Main.rand.Next(4) == 0)
                    {
                        if (npc.oldPos[num124] == Vector2.Zero)
                        {
                            break;
                        }
                        for (int num125 = 0; num125 < 2; num125++)
                        {
                            if (Main.rand.Next(3) == 0)
                            {
                                Dust.NewDust(npc.oldPos[num124], npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), (float)hitDirection, -1f, 0, default(Color), 1f);
                            }
                            Dust dust42 = Main.dust[Dust.NewDust(npc.oldPos[num124], npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), 0f, 0f, 0, default(Color), 1f)];
                            dust42.noGravity = true;
                            dust42.velocity *= 3f;
                        }
                    }
                }
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color drawColor)
        {
            Texture2D currentTex = Main.npcTexture[npc.type];
            Texture2D GlowTex = mod.GetTexture("Glowmasks/DeityEater_Glow");

            BaseDrawing.DrawTexture(sb, currentTex, 0, npc, drawColor);

            //draw glow/glow afterimage
            BaseDrawing.DrawTexture(sb, GlowTex, 0, npc, AAColor.Cthulhu2);
            BaseDrawing.DrawAfterimage(sb, GlowTex, 0, npc, 0.8f, 1f, 6, false, 0f, 0f, AAColor.Cthulhu2);

            return false;
        }
    }
}
