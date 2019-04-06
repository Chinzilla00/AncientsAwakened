using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using BaseMod;
using Terraria.Graphics.Shaders;

namespace AAMod.NPCs.Enemies.Void
{
    public class Vortex : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vortex");
        }

        public override void SetDefaults()
        {
            npc.npcSlots = 100;
            npc.width = 84;
            npc.height = 84;
            npc.aiStyle = -1;
            npc.damage = 40;
            npc.defense = 40;
            npc.lifeMax = 1000;
            npc.value = Item.buyPrice(0, 0, 50, 0);
            npc.HitSound = new LegacySoundStyle(3, 4, Terraria.Audio.SoundType.Sound);
            npc.DeathSound = new LegacySoundStyle(4, 14, Terraria.Audio.SoundType.Sound);
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.netAlways = true;
        }

        public float Rotation = 0;

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidEnergy"), Main.rand.Next(4));
        }

        public override void AI()
        {
            Player player = Main.player[npc.target];
            AAPlayer modPlayer = Main.player[npc.target].GetModPlayer<AAPlayer>();
            BaseAI.AIElemental(npc, ref npc.ai, null, 1, false, false, 800f, 600f, 180, 3f);

            if (npc.velocity.X > 0)
            {
                Rotation += .03f;
            }
            else if (npc.velocity.X < 0)
            {
                Rotation -= .03f;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture2D13 = Main.npcTexture[npc.type];
            Texture2D BladeTex = mod.GetTexture("NPCs/Enemies/Void/VortexBlades");
            Texture2D GlowTex = mod.GetTexture("Glowmasks/Vortex_Glow");
            Texture2D BladeGlowTex = mod.GetTexture("Glowmasks/VortexBlades_Glow");

            BaseDrawing.DrawTexture(spriteBatch, BladeTex, 0, npc.position, npc.width, npc.height, npc.scale, Rotation, 0, 1, new Rectangle(0, 0, BladeTex.Width, BladeTex.Height), drawColor, true);
            BaseDrawing.DrawTexture(spriteBatch, texture2D13, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, 0, 1, new Rectangle(0, 0, texture2D13.Width, texture2D13.Height), drawColor, true);
            BaseDrawing.DrawTexture(spriteBatch, BladeGlowTex, 0, npc.position, npc.width, npc.height, npc.scale, Rotation, 0, 1, new Rectangle(0, 0, BladeTex.Width, BladeTex.Height), AAColor.ZeroShield, true);
            BaseDrawing.DrawTexture(spriteBatch, GlowTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, 0, 1, new Rectangle(0, 0, texture2D13.Width, texture2D13.Height), AAColor.ZeroShield, true);
            return false;
        }
    }
}