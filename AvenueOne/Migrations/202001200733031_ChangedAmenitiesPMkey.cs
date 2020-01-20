namespace AvenueOne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedAmenitiesPMkey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RoomTypeAmenities", "Amenities_Name", "dbo.Amenities");
            DropIndex("dbo.RoomTypeAmenities", new[] { "Amenities_Name" });
            RenameColumn(table: "dbo.RoomTypeAmenities", name: "Amenities_Name", newName: "Amenities_Id");
            DropPrimaryKey("dbo.Amenities");
            DropPrimaryKey("dbo.RoomTypeAmenities");
            AddColumn("dbo.Amenities", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.RoomTypeAmenities", "Amenities_Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Amenities", "Id");
            AddPrimaryKey("dbo.RoomTypeAmenities", new[] { "RoomType_Id", "Amenities_Id" });
            CreateIndex("dbo.RoomTypeAmenities", "Amenities_Id");
            AddForeignKey("dbo.RoomTypeAmenities", "Amenities_Id", "dbo.Amenities", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomTypeAmenities", "Amenities_Id", "dbo.Amenities");
            DropIndex("dbo.RoomTypeAmenities", new[] { "Amenities_Id" });
            DropPrimaryKey("dbo.RoomTypeAmenities");
            DropPrimaryKey("dbo.Amenities");
            AlterColumn("dbo.RoomTypeAmenities", "Amenities_Id", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Amenities", "Id");
            AddPrimaryKey("dbo.RoomTypeAmenities", new[] { "RoomType_Id", "Amenities_Name" });
            AddPrimaryKey("dbo.Amenities", "Name");
            RenameColumn(table: "dbo.RoomTypeAmenities", name: "Amenities_Id", newName: "Amenities_Name");
            CreateIndex("dbo.RoomTypeAmenities", "Amenities_Name");
            AddForeignKey("dbo.RoomTypeAmenities", "Amenities_Name", "dbo.Amenities", "Name", cascadeDelete: true);
        }
    }
}
