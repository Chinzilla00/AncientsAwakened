using System;
using System.IO;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Achievements;
using BaseMod;

namespace AAMod
{
	public class AANet
	{
		public static bool DEBUG = true; 	
		
		public static void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
			if(DEBUG) ErrorLogger.Log((Main.netMode == 2 ? "--SERVER-- " : "--CLIENT-- " ) + "SYNC PLAYER CALLED! NEWPLAYER: " + newPlayer + ". TOWHO: " + toWho + ". FROMWHO:" + fromWho);			
			if(Main.netMode == 2 && (toWho > -1 || fromWho > -1))
            {
                PlayerConnected(toWho == -1 ? fromWho : toWho);
            }
        }

        public static void PlayerConnected(int playerID)
        {
            if (DEBUG) ErrorLogger.Log("--SERVER-- PLAYER JOINED!");
        }

        public static void SendNetMessage(int msg,  params object[] param)
		{
			SendNetMessageClient(msg, -1, param);
		}
		
		public static void SendNetMessageClient(int msg, int client, params object[] param)
		{
			try
			{
				if (Main.netMode == 0) { return; }
		
				BaseMod.BaseNet.WriteToPacket(AAMod.instance.GetPacket(), (byte)msg, param).Send(client);
			}catch(Exception e)
			{ 
				ErrorLogger.Log((Main.netMode == 2 ? "--SERVER-- " : "--CLIENT-- " ) + "ERROR SENDING MSG: " + msg.ToString() + ": " + e.Message); ErrorLogger.Log(e.StackTrace); ErrorLogger.Log("-------"); 
				string param2 = "";
				for(int m = 0; m < param.Length; m++)
				{
					param2 += param[m];
				}
				ErrorLogger.Log("PARAMS: " + param2); 
				ErrorLogger.Log("-------"); 
			}
		}

        public static void HandlePacket(BinaryReader bb, int whoAmI)
        {
			byte msg = bb.ReadByte();		
			if(DEBUG) ErrorLogger.Log((Main.netMode == 2 ? "--SERVER-- " : "--CLIENT-- " ) + "HANDING MESSAGE: " + msg);				
			try
			{
            if (msg == SyncShock) //Electric Shock master and stats setting
			{
				int projID = (int)bb.ReadShort();
				int masterType = (int)bb.ReadByte();
				int masterID = (int)bb.ReadShort();
				int maxTargets = (int)bb.ReadByte();
				float minRange = bb.ReadFloat();
				float maxRange = bb.ReadFloat();
				if (Main.projectile[projID] != null && Main.projectile[projID].active && Main.projectile[projID].modProjectile is AAProjectile)
				{
                    ((AAProjectile)Main.projectile[projID].modProjectile).SetMaster(masterType, masterID, maxTargets, minRange, maxRange, false);
				}
				if (Main.netMode == 2) SendNetMessage(SyncShock, (short)projID, (byte)masterType, (short)masterID, (byte)maxTargets, minRange, maxRange);
			}
			}catch(Exception e){ ErrorLogger.Log((Main.netMode == 2 ? "--SERVER-- " : "--CLIENT-- " ) + "ERROR HANDLING MSG: " + msg.ToString() + ": " + e.Message); ErrorLogger.Log(e.StackTrace); ErrorLogger.Log("-------"); }
		}	
		public const byte SyncShock = 4;
	}
}