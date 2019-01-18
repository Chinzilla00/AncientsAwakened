using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using BaseMod;
using AAMod.NPCs.Bosses.Yamata.Awakened;

namespace AAMod.NPCs.Bosses.SoC
{
    [AutoloadBossHead]
    public class SoC : ModNPC
	{
        public bool HeadsSpawned = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of Cthulhu");
        }

        public override void SetDefaults()
        {
            npc.npcSlots = 100;
            npc.width = 54;
            npc.height = 54;
            npc.aiStyle = -1;
            npc.damage = 40;
            npc.defense = 10;
            npc.lifeMax = 3000000;
            npc.value = Item.buyPrice(0, 2, 0, 0);
            npc.DeathSound = new LegacySoundStyle(2, 88, Terraria.Audio.SoundType.Sound);
            npc.knockBackResist = 0f;
            npc.boss = true;
            music = MusicID.LunarBoss;
            npc.noGravity = false;
            npc.netAlways = true;
            npc.dontTakeDamage = true;
            for (int m = 0; m < npc.buffImmune.Length; m++) npc.buffImmune[m] = true;
        }

        public bool LeaveLine = false;

        public override void AI()
        {
            Player player = Main.player[npc.target];
            AAPlayer modPlayer = Main.player[npc.target].GetModPlayer<AAPlayer>();
            modPlayer.Leave = false;
            npc.rotation = npc.velocity.X / 15f;
            if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead)
                {
                    npc.ai[1] = 1f;
                }
            }
            if (npc.ai[1] == 0f)
            {
                Vector2 vector45 = new Vector2(npc.position.X + ((float)npc.width * 0.5f), npc.position.Y + ((float)npc.height * 0.5f));
                float num444 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector45.X;
                float num445 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector45.Y;
                float num446 = (float)Math.Sqrt((double)((num444 * num444) + (num445 * num445)));
                float num447 = 10f;
                num447 += num446 / 100f;
                if (num447 < 8f)
                {
                    num447 = 8f;
                }
                if (num447 > 32f)
                {
                    num447 = 32f;
                }
                num446 = num447 / num446;
                npc.velocity.X = num444 * num446;
                npc.velocity.Y = num445 * num446;
                return;
            }
            if (npc.ai[1] == 1f)
            {
                if (LeaveLine == false)
                {
                    LeaveLine = true;

                    BaseUtility.Chat("...leave and do not return, mortal...", Color.DarkCyan);
                }
                npc.velocity.X *= 0.6f;
                npc.alpha += 10;
            }
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (damage > 0)
            {
                Main.NewText("YOU CANNOT CHEAT DEATH", Color.Cyan);
                damage = 0;
            }
            return false;
        }

        public float Rotation = 0;

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D WheelTex = mod.GetTexture("NPCs/Bosses/SoC/SoC_Wheel");
            Rotation = npc.velocity.X > 0 ? 0.01f : -0.01f;
            spriteBatch.Draw(WheelTex, npc.Center, null, drawColor, Rotation, npc.Center, 1.3f, SpriteEffects.None, 1f);
            return true;
        }
    }
}