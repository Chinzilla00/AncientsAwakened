using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class LockedOn : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Locked On");
            Description.SetDefault("Infinity Zero has you locked on");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 60;
            player.GetModPlayer<AAPlayer>(mod).LockedOn = true;
        }
    }
}
