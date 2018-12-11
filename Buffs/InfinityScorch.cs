using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class InfinityScorch : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Infinity Scorch");
			Description.SetDefault("Your health is burning away");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<AAPlayer>(mod).InfinityScorch = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<AAModGlobalNPC>(mod).InfinityScorch = true;
        }
    }
}
