using Terraria;
using Terraria.ModLoader;
using BaseMod;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System;

namespace AAMod.NPCs.Bosses.Truffle
{
    public class TruffleProbe : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Truffle Probe");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 14;
            npc.height = 14;
            npc.value = BaseUtility.CalcValue(0, 0, 0, 0);
            npc.npcSlots = 0;
            npc.aiStyle = -1;
            npc.lifeMax = 500;
            npc.defense = 0;
            npc.damage = 20;
            npc.HitSound = new LegacySoundStyle(3, 4, Terraria.Audio.SoundType.Sound);
            npc.DeathSound = new LegacySoundStyle(4, 14, Terraria.Audio.SoundType.Sound);
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.noTileCollide = true;
        }

        public int body = -1;
        public float rotValue = -1f;

        public override void AI()
        {
            if (!NPC.AnyNPCs(mod.NPCType<TechnoTruffle>()))
            {
                npc.life = 0;
            }

            npc.noGravity = true;

            if (npc.alpha > 0)
            {
                npc.dontTakeDamage = true;
                npc.chaseable = false;
            }
            else
            {
                npc.dontTakeDamage = false;
                npc.chaseable = true;
            }

            if (body == -1)
            {
                int npcID = BaseAI.GetNPC(npc.Center, mod.NPCType<TechnoTruffle>(), 400f, null);
                if (npcID >= 0) body = npcID;
            }
            if (body == -1) return;
            NPC truffle = Main.npc[body];
            if (truffle == null || truffle.life <= 0 || !truffle.active || truffle.type != mod.NPCType<TechnoTruffle>()) { BaseAI.KillNPCWithLoot(npc); return; }

            Player player = Main.player[truffle.target];

            for (int m = npc.oldPos.Length - 1; m > 0; m--)
            {
                npc.oldPos[m] = npc.oldPos[m - 1];
            }
            npc.oldPos[0] = npc.position;

            int probeNumber = ((TechnoTruffle)truffle.modNPC).ProbeCount;
            if (rotValue == -1f) rotValue = npc.ai[0] % probeNumber * ((float)Math.PI * 2f / probeNumber);
            rotValue += 0.04f;
            while (rotValue > (float)Math.PI * 2f) rotValue -= (float)Math.PI * 2f;

            npc.Center = BaseUtility.RotateVector(player.Center, player.Center + new Vector2(260, 0f), rotValue);
        }

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/TruffleProbe_Glow1");
            Texture2D glowTex1 = mod.GetTexture("Glowmasks/TruffleProbe_Glow2");
            Color color = BaseUtility.MultiLerpColor(Main.player[Main.myPlayer].miscCounter % 100 / 100f, BaseDrawing.GetLightColor(npc.position), BaseDrawing.GetLightColor(npc.position), Color.Violet, BaseDrawing.GetLightColor(npc.position), Color.Violet, BaseDrawing.GetLightColor(npc.position));

            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc, color);
            BaseDrawing.DrawTexture(spritebatch, glowTex1, 0, npc, Color.White);
            return false;
        }
    }
}


