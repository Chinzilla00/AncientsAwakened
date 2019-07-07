using Terraria.ID;
using BaseMod;

namespace AAMod.Items.Melee
{
    public class SunLance : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sun Halberd");
            BaseMod.BaseUtility.AddTooltips(item, new string[] { "Strikes foes in an arc, then stabs in the direction of the cursor"});			
		}
		
        public override void SetDefaults()
        {
            item.width = 35;
            item.height = 35;
            item.maxStack = 1;
            item.rare = 5;
            item.value = BaseMod.BaseUtility.CalcValue(0, 15, 0, 0);

            item.useStyle = 5;
            item.useAnimation = 50;
            item.useTime = 50;
            item.UseSound = SoundID.Item1;
            item.damage = 100;
            item.knockBack = 6;
            item.melee = true;
            item.autoReuse = true;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.shoot = mod.ProjType("SunLance");
            item.shootSpeed = 4;			
        }
    }
}