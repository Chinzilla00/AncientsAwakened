using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Champion.Drone
{
    public class DroneCool : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("R.A.B.I.T. Unit Reload Protocol");
            Description.SetDefault("RELOADING. DAMAGE COMPENSATION PROVIDED.");
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.rangedCrit += 15;
            player.rangedDamage += .15f;
        }
    }
}