using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace AAMod.NPCs.Bosses.Anubis.Forsaken
{
    public class CurseCircle : ModNPC
    {
        public override void SetDefaults()
        {
            npc.alpha = 255;
            npc.dontTakeDamage = true;
            npc.lifeMax = 1;
            npc.aiStyle = -1;
            npc.damage = Main.expertMode ? 50 : 84;
            npc.defense = Main.expertMode ? 1 : 1;
            npc.knockBackResist = 0.2f;
            npc.width = 82;
            npc.height = 82;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.lavaImmune = true;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.scale = .001f;
            npc.friendly = false;
        }

        public override void AI()
        {
            if (npc.ai[1] == 0)
            {
                if (npc.alpha > 50)
                {
                    npc.alpha -= 5;
                }
                if (npc.scale < 1)
                {
                    npc.scale += .02f;
                }
                npc.rotation += .1f;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    npc.ai[0]++;
                    if (npc.ai[0] >= 120)
                    {
                        npc.ai[0] = 0;
                        npc.ai[1] = 1;

                        int Type = Main.rand.Next(3);

                        switch (Type)
                        {
                            case 0:
                                Type = ModContent.NPCType<CursedLocust>();
                                break;
                            case 1:
                                Type = ModContent.NPCType<CursedScarab>();
                                break;
                            case 2:
                                Type = ModContent.NPCType<Naddaha>();
                                break;
                        }

                        int m = NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, Type);
                        Main.npc[m].Center = npc.Center;

                        npc.netUpdate = true;
                    }
                }
            }
            else
            {
                if (npc.alpha < 255)
                {
                    npc.alpha += 5;
                }
                else
                {
                    npc.active = false;
                }
                if (npc.scale < 1)
                {
                    npc.scale -= .02f;
                }
                npc.rotation -= .1f;
            }
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch sb, Color drawColor)
        {
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseDrawing.DrawAura(sb, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, auraPercent, 1.4f, npc.scale, npc.rotation, npc.direction, 1, default, 0, 0, ColorUtils.COLOR_GLOWPULSE);
            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 1, npc.frame, npc.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            return false;
        }
    }
}