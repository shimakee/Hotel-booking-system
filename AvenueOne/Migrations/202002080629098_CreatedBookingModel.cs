namespace AvenueOne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedBookingModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DateCheckin = c.DateTime(nullable: false),
                        DateCheckout = c.DateTime(nullable: false),
                        AmountTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Occupants = c.Int(nullable: false),
                        Status = c.Byte(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                        Room_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.Room_Id, cascadeDelete: true)
                .Index(t => t.Room_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "Room_Id", "dbo.Rooms");
            DropIndex("dbo.Bookings", new[] { "Room_Id" });
            DropTable("dbo.Bookings");
        }
    }
}
