using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod;

namespace AAMod.NPCs.Critters
{
    public class RoyalRabbit : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Royal Rabbit");
            Main.npcFrameCount[npc.type] = 7;
        }
        public override void SetDefaults()
        {
            npc.width = 28;
            npc.height = 24;
            npc.defense = 0;
            npc.lifeMax = 100;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            npc.npcSlots = 0f;
            npc.aiStyle = 7;
            aiType = NPCID.Bunny;  //npc behavior
            animationType = NPCID.Bunny;
            npc.dontTakeDamageFromHostiles = false;
            banner = npc.type;
            bannerItem = ItemID.BunnyBanner;
            npc.catchItem = (short)mod.ItemType("RoyalRabbit");
        }

        public override void NPCLoot()
        {
            Player player = Main.player[Player.FindClosest(npc.Center, npc.width, npc.height)];
            int bunnyKills = NPC.killCount[Item.NPCtoBanner(NPCID.Bunny)];
            if (bunnyKills % 100 == 0 && bunnyKills < 1000)
            {
                Main.NewText("Those who slaughter the innocent must be PUNISHED!", 107, 137, 179);
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Rajah"), npc.Center);
                AAModGlobalNPC.SpawnRajah(player, mod.NPCType<Bosses.Rajah.Rajah>(), true, new Vector2(npc.Center.X, npc.Center.Y - 2000), "Rajah Rabbit");
            }
            if (bunnyKills % 100 == 0 && bunnyKills >= 1000)
            {
                Main.NewText("YOU HAVE COMMITTED AN UNFORGIVABLE SIN! I SHALL WIPE YOU FROM THIS MORTAL REALM! PREPARE FOR TRUE PAIN AND PUNISHMENT, " + player.name.ToUpper() + "!!!", 107, 137, 179);
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Rajah"), npc.Center);
                AAModGlobalNPC.SpawnRajah(player, mod.NPCType<Bosses.Rajah.Rajah>(), true, new Vector2(npc.Center.X, npc.Center.Y - 2000), "Rajah Rabbit");
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, 77, 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RoyalRabbit1"), 1f);
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldDayGrassCritter.Chance * ((Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type == TileID.Grass && NPC.downedGolemBoss) ? .01f : 0f);
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return false;
        }
    }
}