using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class Glitched : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Glitched");
			Description.SetDefault("Your head is like 10 feet in front of you");
			Main.persistentBuff[Type] = true;
			Main.meleeBuff[Type] = true;
			canBeCleared = false;

		}


        public override void Update(Player player, ref int index)
        {
            base.Update(player, ref index);
            player.manaCost *= 0;
            player.magicDamage += .2f;
            player.minionDamage += .2f;
        }
    }
}
