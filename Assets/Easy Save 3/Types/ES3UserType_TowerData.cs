using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("floorCount", "guns", "floorLevels", "id", "FloorCount", "Guns", "FloorLevels")]
	public class ES3UserType_TowerData : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_TowerData() : base(typeof(TowerData)){ Instance = this; priority = 1; }


		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (TowerData)obj;
			
			writer.WriteProperty("floorCount", instance.floorCount, ES3Type_int.Instance);
			writer.WriteProperty("guns", instance.guns, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.List<Data_and_Scriptable.GunSo.GunSo>)));
			writer.WriteProperty("floorLevels", instance.floorLevels, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.List<System.Int32>)));
			writer.WriteProperty("id", instance.id, ES3Type_string.Instance);
			writer.WriteProperty("FloorCount", instance.FloorCount, ES3Type_int.Instance);
			writer.WriteProperty("Guns", instance.Guns, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.List<Data_and_Scriptable.GunSo.GunSo>)));
			writer.WriteProperty("FloorLevels", instance.FloorLevels, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.List<System.Int32>)));
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (TowerData)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "floorCount":
						instance.floorCount = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "guns":
						instance.guns = reader.Read<System.Collections.Generic.List<Data_and_Scriptable.GunSo.GunSo>>();
						break;
					case "floorLevels":
						instance.floorLevels = reader.Read<System.Collections.Generic.List<System.Int32>>();
						break;
					case "id":
						instance.id = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					case "FloorCount":
						instance.FloorCount = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "Guns":
						instance.Guns = reader.Read<System.Collections.Generic.List<Data_and_Scriptable.GunSo.GunSo>>();
						break;
					case "FloorLevels":
						instance.FloorLevels = reader.Read<System.Collections.Generic.List<System.Int32>>();
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new TowerData();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}


	public class ES3UserType_TowerDataArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_TowerDataArray() : base(typeof(TowerData[]), ES3UserType_TowerData.Instance)
		{
			Instance = this;
		}
	}
}