namespace AvenueOne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRoomTypeAndAmenities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Amenities",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 50),
                        Details = c.String(),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.RoomTypes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 50),
                        Details = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoomTypeAmenities",
                c => new
                    {
                        RoomType_Id = c.String(nullable: false, maxLength: 128),
                        Amenities_Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.RoomType_Id, t.Amenities_Name })
                .ForeignKey("dbo.RoomTypes", t => t.RoomType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Amenities", t => t.Amenities_Name, cascadeDelete: true)
                .Index(t => t.RoomType_Id)
                .Index(t => t.Amenities_Name);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomTypeAmenities", "Amenities_Name", "dbo.Amenities");
            DropForeignKey("dbo.RoomTypeAmenities", "RoomType_Id", "dbo.RoomTypes");
            DropIndex("dbo.RoomTypeAmenities", new[] { "Amenities_Name" });
            DropIndex("dbo.RoomTypeAmenities", new[] { "RoomType_Id" });
            DropTable("dbo.RoomTypeAmenities");
            DropTable("dbo.RoomTypes");
            DropTable("dbo.Amenities");
        }
    }
}
