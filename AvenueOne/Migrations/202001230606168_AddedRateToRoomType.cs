namespace AvenueOne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRateToRoomType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoomTypes", "Rate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.RoomTypes", "RateType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoomTypes", "RateType");
            DropColumn("dbo.RoomTypes", "Rate");
        }
    }
}
