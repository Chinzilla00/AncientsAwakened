using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class HydraToxin : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Hydra Toxin");
			Description.SetDefault("Slower movement per Tile");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.GetModPlayer<AAPlayer>(mod).hydraToxin = true;
        }

		public override void Update(NPC npc, ref int buffIndex)
		{
            npc.GetGlobalNPC<AAModGlobalNPC>(mod).Hydratoxin = true;
        }
	}
}
