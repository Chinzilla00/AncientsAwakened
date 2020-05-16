
using Terraria;
using Terraria.ID;


namespace AAMod.Items.Boss.Sagittarius
{
    public class SagCore : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Sagittarius Core");
            BaseUtility.AddTooltips(item, new string[] { "Activates probes that orbit you and defend you from surrounding enemies" });			
		}		

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 34;
            item.maxStack = 1;
            item.rare = ItemRarityID.LightRed;
            item.value = BaseUtility.CalcValue(0, 0, 60, 0);
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 35;
            item.useTime = 35;
            item.UseSound = SoundID.Item8;
            item.autoReuse = true;
            item.noMelee = true;
            item.summon = true;
            item.shoot = mod.ProjType("OrbiterMinion");
            item.shootSpeed = 5;
            item.damage = 50;
            item.mana = 10;
        }
		
		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(mod.BuffType("SagOrbiter"), 2, true);
			}
		}
    }
}