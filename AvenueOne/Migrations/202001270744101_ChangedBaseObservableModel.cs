namespace AvenueOne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedBaseObservableModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Amenities", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.Amenities", "DateModified", c => c.DateTime());
            AddColumn("dbo.RoomTypes", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.RoomTypes", "DateModified", c => c.DateTime());
            AddColumn("dbo.Rooms", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.Rooms", "DateModified", c => c.DateTime());
            AddColumn("dbo.Customers", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.Customers", "DateModified", c => c.DateTime());
            AddColumn("dbo.People", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.People", "DateModified", c => c.DateTime());
            AddColumn("dbo.Users", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "DateModified", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "DateModified");
            DropColumn("dbo.Users", "DateAdded");
            DropColumn("dbo.People", "DateModified");
            DropColumn("dbo.People", "DateAdded");
            DropColumn("dbo.Customers", "DateModified");
            DropColumn("dbo.Customers", "DateAdded");
            DropColumn("dbo.Rooms", "DateModified");
            DropColumn("dbo.Rooms", "DateAdded");
            DropColumn("dbo.RoomTypes", "DateModified");
            DropColumn("dbo.RoomTypes", "DateAdded");
            DropColumn("dbo.Amenities", "DateModified");
            DropColumn("dbo.Amenities", "DateAdded");
        }
    }
}
