using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class DestinedToDie : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Destined To Die");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.wingTime = 0;
            player.velocity.Y += 10;
            player.GetModPlayer<AAPlayer>(mod).DestinedToDie = true;
        }
    }
}
