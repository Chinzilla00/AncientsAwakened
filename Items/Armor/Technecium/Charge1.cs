using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Technecium
{
    public class Charge1 : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Technecium Charge");
            Description.SetDefault("You are slowly gaining an electrical charge...");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            int chargeCount = player.ownedProjectileCounts[mod.ProjectileType<TechneciumCharge>()];
            if (chargeCount < 2)
            {
                Main.buffTexture[Type] = mod.GetTexture("Items/Armor/Technecium/Charge1");
            }
            else if (chargeCount == 2)
            {
                Main.buffTexture[Type] = mod.GetTexture("Items/Armor/Technecium/Charge2");
            }
            else if (chargeCount == 3)
            {
                Main.buffTexture[Type] = mod.GetTexture("Items/Armor/Technecium/Charge3");
            }
            else if (chargeCount == 4)
            {
                Main.buffTexture[Type] = mod.GetTexture("Items/Armor/Technecium/Charge4");
            }
            if (chargeCount == 0)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}