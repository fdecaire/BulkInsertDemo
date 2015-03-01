using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Helpers;

namespace BulkInsertDemo
{
	/*
CREATE TABLE [dbo].[Store](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Address] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[Zip] [varchar](50) NULL,
 CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
	 */
	class Program
	{
		static void Main(string[] args)
		{
			var createStoreRecords = new CreateStoreRecords();
			createStoreRecords.InsertRecords();
		}
	}

	public class CreateStoreRecords
	{
		private DataTable StoreData;

		private void SetupDataTable()
		{
			StoreData = new DataTable("Store");

			StoreData.AddDataColumn("Name", "System.String");
			StoreData.AddDataColumn("Address", "System.String");
			StoreData.AddDataColumn("City", "System.String");
			StoreData.AddDataColumn("State", "System.String");
			StoreData.AddDataColumn("Zip", "System.String");
		}

		public void InsertRecords()
		{
			SetupDataTable();

			using (var db = new ADODatabaseContext("Server=FRANK-PC\\FRANK;Initial Catalog=sampledata;Integrated Security=True"))
			{
				DataRow StoreList = StoreData.NewRow();
				StoreList["Name"] = "Toys R Us";
				StoreList["Address"] = "123 Main St";
				StoreList["City"] = "Chicago";
				StoreList["State"] = "IL";
				StoreList["Zip"] = "12345";
				StoreData.Rows.Add(StoreList);

				StoreList = StoreData.NewRow();
				StoreList["Name"] = "Target";
				StoreList["Address"] = "5th Ave";
				StoreList["City"] = "New York";
				StoreList["State"] = "NY";
				StoreList["Zip"] = "33333";
				StoreData.Rows.Add(StoreList);

				db.BulkInsert(StoreData);
			}
		}
	}
}
