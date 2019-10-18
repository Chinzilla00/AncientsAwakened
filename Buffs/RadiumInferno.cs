using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class RadiumInferno : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Radium Inferno");
            Description.SetDefault("Rapidly depleting life");
            Main.debuff[Type] = true;
            longerExpertDebuff = false;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }
            npc.lifeRegen -= 200;
            npc.lifeRegenExpectedLossPerSecond = 100;
            Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType("RadiumDust"));
        }
    }
    
}
