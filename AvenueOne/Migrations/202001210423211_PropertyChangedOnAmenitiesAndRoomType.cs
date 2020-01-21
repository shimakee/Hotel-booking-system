namespace AvenueOne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropertyChangedOnAmenitiesAndRoomType : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Amenities", "Name", unique: true, name: "Name");
            CreateIndex("dbo.RoomTypes", "Name", unique: true, name: "Name");
        }
        
        public override void Down()
        {
            DropIndex("dbo.RoomTypes", "Name");
            DropIndex("dbo.Amenities", "Name");
        }
    }
}
