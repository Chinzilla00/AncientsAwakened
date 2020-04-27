using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.NPCs.Bosses.FeudalFungus
{
    public class FungusWake : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Very Large Glowing Mushroom...?");
            Main.npcFrameCount[npc.type] = 5;
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
            npc.dontTakeDamage = true;
            npc.value = 0;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Fungus");
        }

        int frame = 0;

        public override void AI()
        {
            npc.velocity.Y += .1f;
            npc.ai[0]++;

            if (npc.ai[0] == 30)
            {
                frame += 1;
            }
            if (npc.ai[0] == 60)
            {
                frame += 1;
            }
            if (npc.ai[0] == 120)
            {
                frame += 1;
            }
            if (npc.ai[0] == 160)
            {
                frame += 1;
            }

            npc.frame.Y = 80 * frame;

            if (npc.ai[0] == 160 && Main.netMode != 1)
            {
                NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, ModContent.NPCType<FeudalFungus>());
                npc.active = false;
                npc.netUpdate = true;
            }
        }


        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f;
        }

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/FungusWake_Glow");
            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, 0, 5, npc.frame, npc.GetAlpha(dColor), true);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, 0, 5, npc.frame, AAColor.Glow, true);
            return false;
        }
    }
}
