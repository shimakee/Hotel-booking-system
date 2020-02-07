namespace AvenueOne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedRoomModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "Details", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "Details");
        }
    }
}
