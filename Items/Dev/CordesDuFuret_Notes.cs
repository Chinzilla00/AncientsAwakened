using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    //Ferret's dev weapon (note form)	
    public class CordesDuFuret_Notes : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cordes Du Furet");
			Tooltip.SetDefault("Right click in inventory to change between firing notes and smashing heads\n'YA ne delayu DRUGOY NULEVOY POVTOR.'");
		}
		
		public override void SetDefaults()
		{
			item.damage = 290;
			item.width = 64;
			item.height = 64;
			item.useTime = 5;
			item.useAnimation = 5;
			item.useStyle = 5;
			item.knockBack = 7;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 9;
			item.autoReuse = true;		
			item.magic = true;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/FerretNote");
			item.shoot = mod.ProjectileType("FerretNote");
            item.shootSpeed = 12f;
		}

        public override bool CanRightClick()
        {
            return true;
        }	

        public override void RightClick(Player player)
        {
			byte pre = item.prefix;
            item.TurnToAir();
			int itemID = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("CordesDuFuret_Axe"), 1, false, pre, false, false);
			if (Main.netMode == 1)
			{
				NetMessage.SendData(21, -1, -1, null, itemID, 1f, 0f, 0f, 0, 0, 0);
			}			
        }

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-6, -2);
		}
	}
}
