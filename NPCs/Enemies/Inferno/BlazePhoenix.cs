using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BaseMod;

namespace AAMod.NPCs.Enemies.Inferno
{
    public class BlazePhoenix : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blaze Phoenix");
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.ShadowFlameApparition);
            aiType = NPCID.ShadowFlameApparition;
            npc.aiStyle = NPCID.ShadowFlameApparition;
            npc.npcSlots = 1;
            npc.value = BaseUtility.CalcValue(0, 0, 0, 0);
            npc.aiStyle = 86;
            npc.lifeMax = 9000;
            npc.defense = 30;
            npc.noGravity = true;
            npc.damage = 80;
            npc.alpha = 255;
        }
        public override void AI()
        {
            if (npc.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("BroodmotherDust"), 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
            }
            npc.alpha -= 12;
            if (npc.alpha < 0)
            {
                npc.alpha = 0;
            }
            npc.frameCounter++;
            if (npc.frameCounter < 5)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 76;
                if (npc.frame.Y > 76 * 8)
                {
                    npc.frame.Y = 0;
                }
            }
            
        }

        public Color GetGlowAlpha()
        {
            return new Color(200, 0, 50) * ((float)Main.mouseTextColor / 255f);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.GetModPlayer<AAPlayer>(mod).ZoneInferno && Main.dayTime && Main.hardMode ? .8f : 0f;
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Dragonfire"));
        }

        public static Texture2D glowTex = null;
        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            if (glowTex == null)
            {
                glowTex = mod.GetTexture("NPCs/Bosses/Enemies/Inferno/BlazePhoenix");
            }
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc, GetGlowAlpha());
            BaseDrawing.DrawAfterimage(spritebatch, glowTex, 0, npc, 0.8f, 1f, 4, false, 0f, 0f, Color.White);
            return false;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("DragonFire"), 600);
        }
    }
}
