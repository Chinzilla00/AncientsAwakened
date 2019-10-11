using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class CursedHellfire : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Cursed Hellfire");
			Description.SetDefault("Your flesh and blood are burning away");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<AAPlayer>().CursedHellfire = true;
		}

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<AAModGlobalNPC>(mod).CursedHellfire = true;
        }
    }
}
