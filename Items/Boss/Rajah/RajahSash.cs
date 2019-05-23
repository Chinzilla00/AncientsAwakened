using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using System.Collections.Generic;

namespace AAMod.Items.Boss.Rajah
{
    public class RajahSash : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rajah Rabbit's Sash of Vengeance");
            Tooltip.SetDefault(@"Every 10% of health lost gives you 8% extra attack power to your highest damage type boost
Increased Jump Height and Speed
Grants Autojump
Immunity to fall damage");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.value = Item.sellPrice(0, 12, 0, 0);
            item.rare = 9;
            item.accessory = true;
            item.expertOnly = true;
            item.expert = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[item.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            Color damageColor = Color.Firebrick;
            string DamageType = "";
            if (modPlayer.MeleeHighest(player))
            {
                DamageType = "Melee";
                damageColor = Color.Firebrick;
            }
            else if (modPlayer.RangedHighest(player))
            {
                DamageType = "Ranged";
                damageColor = Color.SeaGreen;
            }
            else if (modPlayer.MagicHighest(player))
            {
                DamageType = "Magic";
                damageColor = Color.Violet;
            }
            else if (modPlayer.SummonHighest(player))
            {
                DamageType = "Summoning";
                damageColor = Color.Cyan;
            }
            else if (modPlayer.ThrownHighest(player))
            {
                DamageType = "Throwing";
                damageColor = Color.DarkOrange;
            }

            string DamageAmmount = (100 * DamageBoost(player)) + "%";

            new TooltipLine(mod, "Damage Type", "Current Damage Boost: +" + DamageAmmount + DamageType + "Damage")
            {
                overrideColor = damageColor
            };
        }

        public override void UpdateEquip(Player player)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            player.autoJump = true;
            Player.jumpHeight = 40;
            player.jumpSpeedBoost += 3.6f;
            player.noFallDmg = true;
            if (modPlayer.MeleeHighest(player))
            {
                player.meleeDamage += DamageBoost(player); 
            }
            else if (modPlayer.RangedHighest(player))
            {
                player.rangedDamage += DamageBoost(player);
            }
            else if (modPlayer.MagicHighest(player))
            {
                player.magicDamage += DamageBoost(player);
            }
            else if (modPlayer.SummonHighest(player))
            {
                player.minionDamage += DamageBoost(player);
            }
            else if (modPlayer.ThrownHighest(player))
            {
                player.thrownDamage += DamageBoost(player);
            }
        }

        public float DamageBoost(Player player)
        {
            if (player.statLife <= player.statLifeMax * .1f)
            {
                return .72f;
            }
            if (player.statLife <= player.statLifeMax * .2f)
            {
                return .64f;
            }
            if (player.statLife <= player.statLifeMax * .3f)
            {
                return .56f;
            }
            if (player.statLife <= player.statLifeMax * .4f)
            {
                return .48f;
            }
            if (player.statLife <= player.statLifeMax * .5f)
            {
                return .4f;
            }
            if (player.statLife <= player.statLifeMax * .6f)
            {
                return .32f;
            }
            if (player.statLife <= player.statLifeMax * .7f)
            {
                return .24f;
            }
            if (player.statLife <= player.statLifeMax * .8f)
            {
                return .16f;
            }
            if (player.statLife <= player.statLifeMax * .9f)
            {
                return .8f;
            }
            return 0f;
        }
    }
}