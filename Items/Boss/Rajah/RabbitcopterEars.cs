using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Boss.Rajah
{
    [AutoloadEquip(EquipType.Wings)]
	public class RabbitcopterEars : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Rabbitcopter Ears");
            Tooltip.SetDefault(@"Allows flight and slow fall
'Yeah that's not how rabbit ears work but whatever, it works.'");
        }

		public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.accessory = true;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = ItemRarityID.Yellow;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 180;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.75f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 2.5f;
            constantAscend = 0.125f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 8f;
            acceleration *= 2f;
        }
        public override bool WingUpdate(Player player, bool inUse)
        {
            bool isFlying = false;
            if (player.controlJump && player.wingTime > 0f && !player.jumpAgainCloud && player.jump == 0 && player.velocity.Y != 0f)
            {
                isFlying = true;
            }
            if (player.controlJump && player.controlDown && player.wingTime > 0f)
            {
                isFlying = true;
            }
            if (isFlying || player.jump > 0)
            {
                player.wingFrameCounter++;
                if (player.wingFrameCounter >= 6)
                {
                    player.wingFrameCounter = 0;
                }
                player.wingFrame = 1 + player.wingFrameCounter / 2;
            }
            else if (player.velocity.Y != 0f)
            {
                if (player.controlJump)
                {
                    player.wingFrameCounter++;
                    if (player.wingFrameCounter >= 6)
                    {
                        player.wingFrameCounter = 0;
                    }
                    player.wingFrame = 1 + player.wingFrameCounter / 2;
                }
                else if (player.wingTime == 0f)
                {
                    player.wingFrame = 0;
                }
                else
                {
                    player.wingFrame = 0;
                }
            }
            else
            {
                player.wingFrame = 0;
            }
            return true;
        }
    }
}