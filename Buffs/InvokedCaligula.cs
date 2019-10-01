using Terraria.ModLoader;
using Terraria;
using AAMod.Items.Dev.Invoker;

namespace AAMod.Buffs
{
	public class InvokedCaligula : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Invoked Caligula");
			Description.SetDefault("The first invoked monster is yourself.");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<InvokerPlayer>(mod).InvokedCaligula = true;
        }
	}
}