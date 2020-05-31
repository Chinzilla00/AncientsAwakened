using AAMod.Globals;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class Irradiated : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Irradiated");
			Description.SetDefault("Your health is burning away");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

        public override void Update(NPC npc, ref int buffIndex)
        {
            Player player = Main.player[npc.target];
            bool NearPlayerX = (npc.Center.X > player.Center.X - 48) || (npc.Center.X < player.Center.X + 48);
            bool NearPlayerY = (npc.Center.X > player.Center.Y - 48) || (npc.Center.X < player.Center.Y + 48);
            if (NearPlayerX || NearPlayerY)
            {
                npc.GetGlobalNPC<AAModGlobalNPC>().irradiated = true;
            }
        }
    }
}
