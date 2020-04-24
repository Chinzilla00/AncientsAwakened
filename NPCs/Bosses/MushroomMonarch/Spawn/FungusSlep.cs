using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.NPCs.Bosses.MushroomMonarch.Spawn
{
    public class FungusSlep : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Very Large Glowing Mushroom");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 200;
            npc.defense = 0;
            npc.damage = 0;
            npc.width = 74;
            npc.height = 70;
            npc.aiStyle = -1;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0f;
            npc.noTileCollide = false;
            npc.noGravity = false;
            npc.value = 0;
            npc.rarity = 1;
        }

        public override bool PreAI()
        {
            npc.velocity.Y += .1f;
            return false;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.playerSafe || NPC.AnyNPCs(mod.NPCType("FungusSlep")) || NPC.AnyNPCs(mod.NPCType("FungusWake")) || NPC.AnyNPCs(mod.NPCType("FeudalFungus")) && !spawnInfo.player.ZoneGlowshroom)
            {
                return 0f;
            }
            if (spawnInfo.player.InZone("Surface"))
            {
                return SpawnCondition.OverworldMushroom.Chance * 0.001f;
            }
            if (spawnInfo.player.InZone("Underground"))
            {
                return SpawnCondition.UndergroundMushroom.Chance * 0.001f;
            }
            return 0f;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, ModContent.DustType<Dusts.MushDust>(), hitDirection, -1f, 0, default, 1f);
            }
            if (npc.life <= 0)
            {
                if (Main.netMode != 1 && (NPC.CountNPCS(mod.NPCType("FungusWake")) + NPC.CountNPCS(mod.NPCType("FeudalFungus"))) < 1)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("FungusWake"));
                }
            }
        }

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/FungusSlep_Glow");
            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, 0, 1, npc.frame, npc.GetAlpha(dColor), true);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, 0, 1, npc.frame, AAColor.Glow, true);
            return false;
        }
    }
}
